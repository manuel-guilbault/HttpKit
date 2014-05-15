using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace HttpKit.Parsing
{
    public static class TokenizerExtensions
    {
        private const char CarriageReturn = (char)13;
        private const char LineFeed = (char)10;
        private const char Space = (char)32;
        private const char HorizontalTab = (char)9;
        private const char DoubleQuote = (char)34;
        private static readonly string CrLf = ((char)13).ToString() + (char)10;

        private static readonly string TokenSeparators = @"()<>@,;:\""/[]?={} " + HorizontalTab;

        private static bool IsControl(char c)
        {
            return (c >= 0 && c <= 31) || c == 127;
        }

        private static bool IsChar(char c)
        {
            return c >= 0 && c <= 127;
        }

        private static bool IsUpperAlpha(char c)
        {
            return c >= 'A' && c <= 'Z';
        }

        private static bool IsLowerAlpha(char c)
        {
            return c >= 'a' && c <= 'z';
        }

        private static bool IsDigit(char c)
        {
            return c >= '0' && c <= '9';
        }

        private static bool IsText(char c)
        {
            return !IsControl(c);
        }

        private static bool IsHexadecimal(char c)
        {
            return IsDigit(c) || "abcdefABCDEF".Contains(c);
        }

        private static bool IsTokenSeparator(char c)
        {
            return TokenSeparators.Contains(c);
        }

        public static ParsingException CreateException(this Tokenizer tokenizer, string message, int offset = 0)
        {
            return new ParsingException(message, tokenizer.Value, tokenizer.Position + offset);
        }

        public static bool IsAtEnd(this Tokenizer tokenizer, int offset = 0)
        {
            if (offset < 0) throw new ArgumentException("offset must be equal to or greater than zero", "offset");

            return tokenizer.Position + offset >= tokenizer.Value.Length;
        }

        public static char PeekChar(this Tokenizer tokenizer, int offset = 0)
        {
            if (offset < 0) throw new ArgumentException("offset must be equal to or greater than zero", "offset");
            if (tokenizer.IsAtEnd(offset)) throw tokenizer.CreateException("End of string", offset);

            return tokenizer.Value[tokenizer.Position + offset];
        }

        public static string Peek(this Tokenizer tokenizer, int offset, int length)
        {
            if (offset < 0) throw new ArgumentException("offset must be equal to or greater than zero", "offset");

            length = Math.Min(tokenizer.Value.Length - (tokenizer.Position + offset), length);
            return tokenizer.Value.Substring(tokenizer.Position + offset, length);
        }

        public static string Peek(this Tokenizer tokenizer, int length)
        {
            return tokenizer.Peek(0, length);
        }

        public static string PeekWhile(this Tokenizer tokenizer, Func<char, bool> predicate)
        {
            int length = 0;
            while (!tokenizer.IsAtEnd(length) && predicate(tokenizer.PeekChar(length)))
            {
                ++length;
            }
            return tokenizer.Peek(length);
        }

        public static bool IsNext(this Tokenizer tokenizer, string value, StringComparison comparisonType = StringComparison.InvariantCulture)
        {
            if (value == null) throw new ArgumentNullException("value");

            return string.Equals(tokenizer.Peek(value.Length), value, comparisonType);
        }

        public static string Read(this Tokenizer tokenizer, int length)
        {
            var result = tokenizer.Peek(length);
            tokenizer.Move(length);
            return result;
        }

        public static void Read(this Tokenizer tokenizer, string token, StringComparison comparisonType = StringComparison.InvariantCulture)
        {
            if (token == null) throw new ArgumentNullException("value");

            if (tokenizer.IsNext(token, comparisonType))
            {
                tokenizer.Move(token.Length);
            }
            else
            {
                throw tokenizer.CreateException(string.Format("Not matching expected token ({0})", token));
            }
        }

        public static string ReadUntil(this Tokenizer tokenizer, string token, StringComparison comparisonType = StringComparison.InvariantCulture)
        {
            int offset = 0;
            while (!tokenizer.IsAtEnd(offset) && !string.Equals(tokenizer.Peek(offset, token.Length), token, comparisonType))
            {
                ++offset;
            }

            if (tokenizer.IsAtEnd(offset))
            {
                return "";
            }

            return tokenizer.Read(offset);
        }

        public static string ReadWhile(this Tokenizer tokenizer, params char[] characters)
        {
            if (characters == null) throw new ArgumentNullException("characters");

            return tokenizer.ReadWhile(@char => characters.Contains(@char));
        }

        public static string ReadWhile(this Tokenizer tokenizer, Func<char, bool> predicate)
        {
            int offset = 0;
            while (!tokenizer.IsAtEnd(offset) && predicate(tokenizer.PeekChar(offset)))
            {
                ++offset;
            }
            return offset == 0 ? "" : tokenizer.Read(offset);
        }

        public static void SkipWhiteSpaces(this Tokenizer tokenizer)
        {
            while (!tokenizer.IsAtEnd() && tokenizer.IsNext(" "))
            {
                tokenizer.Move();
            }
        }

        public static long ReadLong(this Tokenizer tokenizer)
        {
            var numberPosition = tokenizer.Position;
            var value = tokenizer.ReadWhile(char.IsDigit);
            if (value == "") throw tokenizer.CreateException("Digit expected");

            try
            {
                return long.Parse(value);
            }
            catch (OverflowException)
            {
				throw tokenizer.CreateException("Number overflow", -numberPosition);
            }
        }

        public static long? TryReadLong(this Tokenizer tokenizer)
        {
            var numberPosition = tokenizer.Position;
            var value = tokenizer.ReadWhile(char.IsDigit);
            if (value == "") return null;

            try
            {
                return long.Parse(value);
            }
            catch (OverflowException)
            {
				throw tokenizer.CreateException("Number overflow", -numberPosition);
            }
        }

        public static DateTime? TryReadDateTime(this Tokenizer tokenizer)
        {
            return DateTimeParser.TryParse(tokenizer.ToString());
        }

        public static string PeekToken(this Tokenizer tokenizer)
        {
            return tokenizer.PeekWhile(c => !IsControl(c) && !IsTokenSeparator(c));
        }

        public static string ReadToken(this Tokenizer tokenizer)
        {
            var token = tokenizer.ReadWhile(c => !IsControl(c) && !IsTokenSeparator(c));
            if (token == "") throw tokenizer.CreateException("Token expected");

            return token;
        }

        public static bool IsNextToken(this Tokenizer tokenizer, string token, StringComparison comparisonType = StringComparison.InvariantCulture)
        {
            if (token == null) throw new ArgumentNullException("token");

            return string.Equals(tokenizer.PeekToken(), token, comparisonType);
        }
    }
}

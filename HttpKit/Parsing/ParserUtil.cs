using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace HttpKit.Parsing
{
	public static class ParserUtil
	{
		private static readonly string separators = @"()<>@,;:\""/[]?={} " + HorizontalTab;

		public const char CarriageReturn = (char)13;
		public const char LineFeed = (char)10;
		public const char Space = (char)32;
		public const char HorizontalTab = (char)9;
		public const char DoubleQuote = (char)34;
		public static readonly string CrLf = ((char)13).ToString() + (char)10;

		public static bool IsChar(char c)
		{
			return c >= 0 && c <= 127;
		}

		public static bool IsUpperAlpha(char c)
		{
			return c >= 'A' && c <= 'Z';
		}

		public static bool IsLowerAlpha(char c)
		{
			return c >= 'a' && c <= 'z';
		}

		public static bool IsDigit(char c)
		{
			return c >= '0' && c <= '9';
		}

		public static bool IsControl(char c)
		{
			return (c >= 0 && c <= 31) || c == 127;
		}

		public static bool IsText(char c)
		{
			return !IsControl(c);
		}

		public static bool IsHexadecimal(char c)
		{
			return IsDigit(c) || "abcdefABCDEF".Contains(c);
		}

		public static bool IsSeparator(char c)
		{
			return separators.Contains(c);
		}

        public static string PeekToken(this Tokenizer tokenizer)
        {
            return tokenizer.PeekWhile(c => !IsControl(c) && !IsSeparator(c));
        }

		public static string ReadToken(this Tokenizer tokenizer)
		{
			var token = tokenizer.ReadWhile(c => !IsControl(c) && !IsSeparator(c));
			if (token == "") throw tokenizer.CreateException("Token expected");

			return token;
		}

        public static bool IsNextToken(this Tokenizer tokenizer, string token, StringComparison comparisonType = StringComparison.InvariantCulture)
        {
            if (token == null) throw new ArgumentNullException("token");

            return string.Equals(tokenizer.PeekToken(), token, comparisonType);
        }

        public static DateTime? TryParseDateTime(string value)
        {
            DateTime dateTime;
            if (DateTime.TryParseExact(value, "r", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal, out dateTime))
            {
                return dateTime;
            }
            else
            {
                return null;
            }
        }
	}
}

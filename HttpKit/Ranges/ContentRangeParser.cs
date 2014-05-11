using HttpKit.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HttpKit.Ranges
{
    public class ContentRangeParser : IHeaderParser<IContentRange>
    {
        private const string UNIT_RANGE_SEPARATOR = " ";
        private const string RANGE_LENGTH_SEPARATOR = "/";
        private const string RANGE_BOUNDS_SEPARATOR = "-";
        private const string UNKNOWN_RANGE = "*";
        private const string UNKNOWN_LENGTH = "*";

        public IContentRange Parse(Tokenizer tokenizer)
        {
            if (tokenizer == null) throw new ArgumentNullException("tokenizer");

            tokenizer.SkipWhiteSpaces();
            var unit = ParseUnit(tokenizer);

            tokenizer.Read(UNIT_RANGE_SEPARATOR);

            tokenizer.SkipWhiteSpaces();
            var range = ParseSubRange(tokenizer);

            tokenizer.Read(RANGE_LENGTH_SEPARATOR);

            var instanceLength = ParseInstanceLength(tokenizer);

            return new ContentRange(unit, range, instanceLength);
        }

        protected IRangeUnit ParseUnit(Tokenizer tokenizer)
        {
            var name = tokenizer.ReadToken();
            return new RangeUnit(name);
        }

        protected IContentSubRange ParseSubRange(Tokenizer tokenizer)
        {
            if (tokenizer.IsNext(UNKNOWN_RANGE))
            {
                tokenizer.Read(UNKNOWN_RANGE);
                return ContentSubRange.Unknown;
            }

            var start = tokenizer.ReadLong();
            tokenizer.Read(RANGE_BOUNDS_SEPARATOR);
            var end = tokenizer.ReadLong();

            return new ContentSubRange(start, end);
        }

        protected IInstanceLength ParseInstanceLength(Tokenizer tokenizer)
        {
            if (tokenizer.IsNext(UNKNOWN_LENGTH))
            {
                tokenizer.Read(UNKNOWN_LENGTH);
                return InstanceLength.Unknown;
            }

            var length = tokenizer.ReadLong();
            return new InstanceLength(length);
        }
    }
}

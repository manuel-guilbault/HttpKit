using HttpKit.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HttpKit.Ranges
{
    public class RangeParser : IHeaderParser<IRange>
    {
        private const string UNIT_RANGES_SEPARATOR = "=";
        private const string RANGES_SEPARATOR = ",";
        private const string RANGE_BOUNDS_SEPARATOR = "-";

        public IRange Parse(Tokenizer tokenizer)
        {
            if (tokenizer == null) throw new ArgumentNullException("tokenizer");

            tokenizer.SkipWhiteSpaces();
            var unit = ParseUnit(tokenizer);

            tokenizer.SkipWhiteSpaces();
            tokenizer.Read(UNIT_RANGES_SEPARATOR);

            tokenizer.SkipWhiteSpaces();
            var ranges = ParseRanges(tokenizer).ToArray();

            return new Range(unit, ranges);
        }

        protected IRangeUnit ParseUnit(Tokenizer tokenizer)
        {
            var unitName = tokenizer.ReadToken();
            return new RangeUnit(unitName);
        }

        protected IEnumerable<ISubRange> ParseRanges(Tokenizer tokenizer)
        {
            yield return ParseRange(tokenizer);
            tokenizer.SkipWhiteSpaces();

            while (tokenizer.IsNext(RANGES_SEPARATOR))
            {
                tokenizer.Read(RANGES_SEPARATOR);
                tokenizer.SkipWhiteSpaces();
                yield return ParseRange(tokenizer);
                tokenizer.SkipWhiteSpaces();
            }
        }

		protected ISubRange ParseRange(Tokenizer tokenizer)
        {
            if (tokenizer.IsNext(RANGE_BOUNDS_SEPARATOR))
            {
                return ParseSuffixRange(tokenizer);
            }

            var start = tokenizer.ReadLong();
            tokenizer.Read(RANGE_BOUNDS_SEPARATOR);
            var end = tokenizer.TryReadLong();

            return end == null
				? SubRange.CreateOffsetFromStart(start)
				: SubRange.CreateClosedRange(start, end.Value);
        }

		protected ISubRange ParseSuffixRange(Tokenizer tokenizer)
        {
            tokenizer.Read(RANGE_BOUNDS_SEPARATOR);
            var end = tokenizer.ReadLong();

			return SubRange.CreateOffsetFromEnd(end);
        }
    }
}

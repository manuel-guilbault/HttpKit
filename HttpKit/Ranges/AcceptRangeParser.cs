using HttpKit.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HttpKit.Ranges
{
    public class AcceptRangeParser : IHeaderParser<IAcceptRange>
    {
        private const string NONE = "none";
        private const string SEPARATOR = ",";

        public IAcceptRange Parse(Tokenizer tokenizer)
        {
            if (tokenizer == null) throw new ArgumentNullException("tokenizer");

            if (tokenizer.IsNextToken(NONE))
            {
                return AcceptRange.None;
            }

            return new AcceptRange(ParseUnits(tokenizer).ToArray());
        }

        protected IEnumerable<IRangeUnit> ParseUnits(Tokenizer tokenizer)
        {
            yield return ParseUnit(tokenizer);
            tokenizer.SkipWhiteSpaces();

            while (tokenizer.IsNext(SEPARATOR))
            {
                tokenizer.Read(SEPARATOR);
                tokenizer.SkipWhiteSpaces();
                yield return ParseUnit(tokenizer);
                tokenizer.SkipWhiteSpaces();
            }
        }

        protected IRangeUnit ParseUnit(Tokenizer tokenizer)
        {
            var range = tokenizer.ReadToken();
            return new RangeUnit(range);
        }
    }
}

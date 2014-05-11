using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HttpKit.Parsing
{
    public class HeaderReader<T> : IHeaderReader<T>
    {
        private readonly IHeaderParser<T> parser;

        public HeaderReader(IHeaderParser<T> parser)
        {
            if (parser == null) throw new ArgumentNullException("parser");

            this.parser = parser;
        }

        public T Read(string value)
        {
            if (value == null) throw new ArgumentNullException("value");

            var tokenizer = new Tokenizer(value);
            var result = parser.Parse(tokenizer);
            if (!tokenizer.IsAtEnd())
            {
				throw tokenizer.CreateException("End of string expected");
            }
            return result;
        }
    }
}

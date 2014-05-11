using HttpKit.Caching;
using HttpKit.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HttpKit.Ranges
{
    public class IfRangeParser : IHeaderParser<IIfRange>
    {
        private readonly IHeaderParser<IEntityTag> entityTagParser;

        public IfRangeParser()
            : this(new EntityTagParser())
        {
        }

        public IfRangeParser(IHeaderParser<IEntityTag> entityTagParser)
        {
            if (entityTagParser == null) throw new ArgumentNullException("entityTagParser");

            this.entityTagParser = entityTagParser;
        }

        protected IEntityTag TryParseEntityTag(Tokenizer tokenizer)
		{
			try
			{
				return entityTagParser.Parse(tokenizer);
			}
			catch (ParsingException)
			{
				return null;
			}
		}

		protected DateTime? TryParseLastModified(string value)
		{
            return ParserUtil.TryParseDateTime(value);
		}

        public IIfRange Parse(Tokenizer tokenizer)
        {
            if (tokenizer == null) throw new ArgumentNullException("tokenizer");

			var entityTag = TryParseEntityTag(tokenizer);
			if (entityTag != null)
			{
				return new IfRange(entityTag);
			}

			var lastModified = TryParseLastModified(tokenizer.Value);
			if (lastModified != null)
			{
				return new IfRange(lastModified.Value);
			}

			throw tokenizer.CreateException("Datetime or ETag expected");
        }
    }
}

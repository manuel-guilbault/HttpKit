using HttpKit.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HttpKit.Caching
{
    public class EntityTagConditionParser : IHeaderParser<IEntityTagCondition>
    {
        private const string ANY_FLAG = "*";
        private const string SEPARATOR = ",";

        private readonly IHeaderParser<IEntityTag> entityTagParser;

        public EntityTagConditionParser()
            : this(new EntityTagParser())
        {
        }

        public EntityTagConditionParser(IHeaderParser<IEntityTag> entityTagParser)
        {
            if (entityTagParser == null) throw new ArgumentNullException("entityTagParser");

            this.entityTagParser = entityTagParser;
        }

        public IEntityTagCondition Parse(Tokenizer tokenizer)
        {
            tokenizer.SkipWhiteSpaces();
            if (tokenizer.IsNext(ANY_FLAG))
            {
                tokenizer.Read(ANY_FLAG);
                return EntityTagCondition.Any;
            }
            else
            {
                return new EntityTagCondition(ParseEntityTags(tokenizer).ToArray());
            }
        }

        protected IEnumerable<IEntityTag> ParseEntityTags(Tokenizer tokenizer)
        {
            yield return entityTagParser.Parse(tokenizer);
            tokenizer.SkipWhiteSpaces();

            while (!tokenizer.IsAtEnd())
            {
                tokenizer.Read(SEPARATOR);
                tokenizer.SkipWhiteSpaces();
                yield return entityTagParser.Parse(tokenizer);
            }
        }
    }
}

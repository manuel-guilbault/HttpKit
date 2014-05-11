using HttpKit.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HttpKit.Caching
{
	public class RequestCacheControlParser : IHeaderParser<IRequestCacheControl>
	{
        private static readonly IRequestCacheDirectiveParser[] defaultDirectiveParsers = new IRequestCacheDirectiveParser[]
        {
            new RequestCacheDirectiveParser(RequestCacheDirective.NoCache),
            new RequestCacheDirectiveParser(RequestCacheDirective.NoStore),
            new DeltaTimeRequestCacheDirectiveParser(RequestCacheDirective.MAX_AGE, RequestCacheDirective.CreateMaxAge),
            new OptionalDeltaTimeRequestCacheDirectiveParser(RequestCacheDirective.MAX_STALE, RequestCacheDirective.CreateMaxStale),
            new DeltaTimeRequestCacheDirectiveParser(RequestCacheDirective.MIN_FRESH, RequestCacheDirective.CreateMinFresh),
            new RequestCacheDirectiveParser(RequestCacheDirective.NoTransform),
            new RequestCacheDirectiveParser(RequestCacheDirective.OnlyIfCached),
            new RequestCacheDirectiveExtensionParser()
        };

        private IRequestCacheDirectiveParser[] directiveParsers;

        public IRequestCacheDirectiveParser[] DirectiveParsers
        {
            get { return directiveParsers ?? defaultDirectiveParsers; }
            set { directiveParsers = value; }
        }

        public IRequestCacheControl Parse(Tokenizer tokenizer)
		{
			if (tokenizer == null) throw new ArgumentNullException("tokenizer");

			var cacheControl = new RequestCacheControl();

			tokenizer.SkipWhiteSpaces();

			ParseDirective(tokenizer, cacheControl);
			tokenizer.SkipWhiteSpaces();

			while (!tokenizer.IsAtEnd())
			{
				tokenizer.Read(",");
				tokenizer.SkipWhiteSpaces();

				ParseDirective(tokenizer, cacheControl);
				tokenizer.SkipWhiteSpaces();
			}

			return cacheControl;
		}

        private void ParseDirective(Tokenizer tokenizer, IRequestCacheControl cacheControl)
		{
            var parser = FindDirectiveParser(tokenizer);
            if (parser == null)
            {
                throw tokenizer.CreateException("Unknown Cache-Control directive.");
            }

            var directive = parser.Parse(tokenizer);
            if (!cacheControl.Has(directive))
            {
                cacheControl.Add(directive);
            }
		}

        private IRequestCacheDirectiveParser FindDirectiveParser(Tokenizer tokenizer)
        {
            foreach (var parser in DirectiveParsers)
            {
                if (parser.CanParse(tokenizer))
                {
                    return parser;
                }
            }
            return null;
        }
	}
}

using HttpKit.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HttpKit.Caching
{
    public class ResponseCacheControlParser : IHeaderParser<IResponseCacheControl>
	{
        private static readonly IResponseCacheDirectiveParser[] defaultDirectiveParsers = new IResponseCacheDirectiveParser[]
        {
            new ResponseCacheDirectiveParser(ResponseCacheDirective.Public),
            new FieldListResponseCacheDirectiveParser(ResponseCacheDirective.PRIVATE, ResponseCacheDirective.CreatePrivate),
            new FieldListResponseCacheDirectiveParser(ResponseCacheDirective.NO_CACHE, ResponseCacheDirective.CreateNoCache),
            new ResponseCacheDirectiveParser(ResponseCacheDirective.NoStore),
            new ResponseCacheDirectiveParser(ResponseCacheDirective.NoTransform),
            new ResponseCacheDirectiveParser(ResponseCacheDirective.MustRevalidate),
            new ResponseCacheDirectiveParser(ResponseCacheDirective.ProxyRevalidate),
            new DeltaTimeResponseCacheDirectiveParser(ResponseCacheDirective.MAX_AGE, ResponseCacheDirective.CreateMaxAge),
            new DeltaTimeResponseCacheDirectiveParser(ResponseCacheDirective.SHARED_MAX_AGE, ResponseCacheDirective.CreateSharedMaxAge),
            new ResponseCacheDirectiveExtensionParser()
        };

        private IResponseCacheDirectiveParser[] directiveParsers;

        public IResponseCacheDirectiveParser[] DirectiveParsers
        {
            get { return directiveParsers ?? defaultDirectiveParsers; }
            set { directiveParsers = value; }
        }

        public IResponseCacheControl Parse(Tokenizer tokenizer)
		{
			if (tokenizer == null) throw new ArgumentNullException("tokenizer");

			var cacheControl = new ResponseCacheControl();

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

        private void ParseDirective(Tokenizer tokenizer, IResponseCacheControl cacheControl)
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

        private IResponseCacheDirectiveParser FindDirectiveParser(Tokenizer tokenizer)
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

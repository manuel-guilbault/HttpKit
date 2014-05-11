using HttpKit.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpKit.Caching
{
    public interface IResponseCacheDirectiveParser : IHeaderParser<IResponseCacheDirective>
    {
        bool CanParse(Tokenizer tokenizer);
    }

    public class ResponseCacheDirectiveParser : IResponseCacheDirectiveParser
    {
        private readonly IResponseCacheDirective directive;

        public ResponseCacheDirectiveParser(IResponseCacheDirective directive)
        {
            if (directive == null) throw new ArgumentNullException("directive");

            this.directive = directive;
        }

        public bool CanParse(Tokenizer tokenizer)
        {
            if (tokenizer == null) throw new ArgumentNullException("tokenizer");

            return tokenizer.IsNextToken(directive.Name);
        }

        public IResponseCacheDirective Parse(Tokenizer tokenizer)
        {
            if (tokenizer == null) throw new ArgumentNullException("tokenizer");

            tokenizer.Read(directive.Name);

            return directive;
        }
    }

    public class FieldListResponseCacheDirectiveParser : IResponseCacheDirectiveParser
    {
        private readonly string name;
        private readonly Func<string[], IResponseCacheDirective> factory;

        public FieldListResponseCacheDirectiveParser(string name, Func<string[], IResponseCacheDirective> factory)
        {
            if (name == null) throw new ArgumentNullException("name");
            if (factory == null) throw new ArgumentNullException("factory");

            this.name = name;
            this.factory = factory;
        }

        public bool CanParse(Tokenizer tokenizer)
        {
            if (tokenizer == null) throw new ArgumentNullException("tokenizer");

            return tokenizer.IsNextToken(name);
        }

        public IResponseCacheDirective Parse(Tokenizer tokenizer)
        {
            if (tokenizer == null) throw new ArgumentNullException("tokenizer");

            tokenizer.Read(name);
            var fields = ReadFieldsList(tokenizer);

            return factory(fields);
        }

        private string[] ReadFieldsList(Tokenizer tokenizer)
        {
            if (!tokenizer.IsNext("="))
            {
                return new string[0];
            }

            tokenizer.Read("=");
            tokenizer.Read("\"");
            var fields = ReadFields(tokenizer).ToArray();
            tokenizer.Read("\""); ;
            return fields;
        }

        private IEnumerable<string> ReadFields(Tokenizer tokenizer)
        {
            yield return tokenizer.ReadToken();
            tokenizer.SkipWhiteSpaces();

            while (tokenizer.IsNext(","))
            {
                tokenizer.Read(",");
                tokenizer.SkipWhiteSpaces();
                yield return tokenizer.ReadToken();
                tokenizer.SkipWhiteSpaces();
            }
        }
    }

    public class DeltaTimeResponseCacheDirectiveParser : IResponseCacheDirectiveParser
    {
        private readonly string name;
        private readonly Func<TimeSpan, IResponseCacheDirective> factory;

        public DeltaTimeResponseCacheDirectiveParser(string name, Func<TimeSpan, IResponseCacheDirective> factory)
        {
            if (name == null) throw new ArgumentNullException("name");
            if (factory == null) throw new ArgumentNullException("factory");

            this.name = name;
            this.factory = factory;
        }

        public bool CanParse(Tokenizer tokenizer)
        {
            if (tokenizer == null) throw new ArgumentNullException("tokenizer");

            return tokenizer.IsNextToken(name);
        }

        public IResponseCacheDirective Parse(Tokenizer tokenizer)
        {
            if (tokenizer == null) throw new ArgumentNullException("tokenizer");

            tokenizer.Read(name);
            tokenizer.Read("=");
            var deltaInSeconds = tokenizer.ReadLong();

            var delta = TimeSpan.FromSeconds(deltaInSeconds);
            return factory(delta);
        }
    }

    public class ResponseCacheDirectiveExtensionParser : IResponseCacheDirectiveParser
    {
        public bool CanParse(Tokenizer tokenizer)
        {
            return true;
        }

        public IResponseCacheDirective Parse(Tokenizer tokenizer)
        {
            if (tokenizer == null) throw new ArgumentNullException("tokenizer");

            var name = tokenizer.ReadToken();

            string value = null;
            if (tokenizer.IsNext("="))
            {
                tokenizer.Read("=");
                value = tokenizer.ReadToken();
            }

            return ResponseCacheDirective.CreateExtension(name, value);
        }
    }
}

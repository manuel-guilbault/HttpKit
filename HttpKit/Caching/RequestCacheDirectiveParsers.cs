using HttpKit.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpKit.Caching
{
    public interface IRequestCacheDirectiveParser : IHeaderParser<IRequestCacheDirective>
    {
        bool CanParse(Tokenizer tokenizer);
    }

    public class RequestCacheDirectiveParser : IRequestCacheDirectiveParser
    {
        private readonly IRequestCacheDirective directive;

        public RequestCacheDirectiveParser(IRequestCacheDirective directive)
        {
            if (directive == null) throw new ArgumentNullException("directive");

            this.directive = directive;
        }

        public bool CanParse(Tokenizer tokenizer)
        {
            if (tokenizer == null) throw new ArgumentNullException("tokenizer");
            
            return tokenizer.IsNextToken(directive.Name);
        }

        public IRequestCacheDirective Parse(Tokenizer tokenizer)
        {
            if (tokenizer == null) throw new ArgumentNullException("tokenizer");

            tokenizer.Read(directive.Name);

            return directive;
        }
    }

    public class DeltaTimeRequestCacheDirectiveParser : IRequestCacheDirectiveParser
    {
        private readonly string name;
        private readonly Func<TimeSpan, IRequestCacheDirective> factory;

        public DeltaTimeRequestCacheDirectiveParser(string name, Func<TimeSpan, IRequestCacheDirective> factory)
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

        public IRequestCacheDirective Parse(Tokenizer tokenizer)
        {
            if (tokenizer == null) throw new ArgumentNullException("tokenizer");

            tokenizer.Read(name);
            tokenizer.Read("=");
            var deltaInSeconds = tokenizer.ReadLong();

            var delta = TimeSpan.FromSeconds(deltaInSeconds);
            return factory(delta);
        }
    }

    public class OptionalDeltaTimeRequestCacheDirectiveParser : IRequestCacheDirectiveParser
    {
        private readonly string name;
        private readonly Func<TimeSpan?, IRequestCacheDirective> factory;

        public OptionalDeltaTimeRequestCacheDirectiveParser(string name, Func<TimeSpan?, IRequestCacheDirective> factory)
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

        public IRequestCacheDirective Parse(Tokenizer tokenizer)
        {
            if (tokenizer == null) throw new ArgumentNullException("tokenizer");

            TimeSpan? delta = null;

            tokenizer.Read(name);
            if (tokenizer.IsNext("="))
            {
                tokenizer.Read("=");
                var deltaInSeconds = tokenizer.ReadLong();
                delta = TimeSpan.FromSeconds(deltaInSeconds);
            }
            
            return factory(delta);
        }
    }

    public class RequestCacheDirectiveExtensionParser : IRequestCacheDirectiveParser
    {
        public bool CanParse(Tokenizer tokenizer)
        {
            return true;
        }

        public IRequestCacheDirective Parse(Tokenizer tokenizer)
        {
            if (tokenizer == null) throw new ArgumentNullException("tokenizer");

            var name = tokenizer.ReadToken();

            string value = null;
            if (tokenizer.IsNext("="))
            {
                tokenizer.Read("=");
                value = tokenizer.ReadToken();
            }

            return RequestCacheDirective.CreateExtension(name, value);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HttpKit.Caching
{
    public class ResponseCacheControl : IResponseCacheControl, IEnumerable<IResponseCacheDirective>
	{
        private readonly IDictionary<string, IResponseCacheDirective> directives = new Dictionary<string, IResponseCacheDirective>();

        public ResponseCacheControl(params IResponseCacheDirective[] directives)
        {
            if (directives == null) throw new ArgumentNullException("directives");

            foreach (var directive in directives)
            {
                Add(directive);
            }
        }

        public bool Has(IResponseCacheDirective directive)
		{
			if (directive == null) throw new ArgumentNullException("directive");

			return Has(directive.Name);
		}

		public bool Has(string name)
		{
			if (name == null) throw new ArgumentNullException("name");

			return directives.ContainsKey(name);
		}

        public IResponseCacheDirective Get(string name)
		{
            IResponseCacheDirective directive;
			directives.TryGetValue(name, out directive);
			return directive;
		}

        public void Add(IResponseCacheDirective directive)
		{
			if (directive == null) throw new ArgumentNullException("directive");

			directives.Add(directive.Name, directive);
		}

		public void Remove(string name)
		{
			if (name == null) throw new ArgumentNullException("name");

			directives.Remove(name);
		}

		public override string ToString()
		{
			return string.Join(", ", directives.Values);
		}

        public IEnumerator<IResponseCacheDirective> GetEnumerator()
        {
            return directives.Values.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

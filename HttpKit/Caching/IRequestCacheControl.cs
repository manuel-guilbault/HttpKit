using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpKit.Caching
{
    public interface IRequestCacheControl : IEnumerable<IRequestCacheDirective>
    {
        bool Has(IRequestCacheDirective directive);
        bool Has(string name);
        IRequestCacheDirective Get(string name);
        void Add(IRequestCacheDirective directive);
        void Remove(string name);
        string ToString();
    }
}

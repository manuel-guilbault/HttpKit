using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpKit.Caching
{
    public interface IResponseCacheControl : IEnumerable<IResponseCacheDirective>
    {
        bool Has(IResponseCacheDirective directive);
        bool Has(string name);
        IResponseCacheDirective Get(string name);
        void Add(IResponseCacheDirective directive);
        void Remove(string name);
        string ToString();
    }
}

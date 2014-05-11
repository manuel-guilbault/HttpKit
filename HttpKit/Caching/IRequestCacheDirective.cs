using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpKit.Caching
{
    public interface IRequestCacheDirective
    {
        string Name { get; }

        string ToString();
    }
}

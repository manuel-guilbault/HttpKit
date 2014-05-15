using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReverseProxy.Owin
{
    public interface ICache
    {
        Task<ICacheEntry> Get(ICacheKey key);
        Task Set(ICacheKey key, ICacheEntry entry);
        Task Remove(ICacheKey key);
    }
}

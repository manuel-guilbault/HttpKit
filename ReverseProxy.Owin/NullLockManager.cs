using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReverseProxy.Owin
{
    public class NullLockManager : ILockManager
    {
        public Task<IOwinResponse> Lock(ICacheKey cacheKey, TimeSpan timeout)
        {
            return null;
        }

        public Task<IOwinResponse> Lock(ICacheKey cacheKey)
        {
            return null;
        }

        public bool Unlock(ICacheKey cacheKey, IOwinResponse response)
        {
            return false;
        }
    }
}

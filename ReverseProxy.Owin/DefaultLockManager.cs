using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReverseProxy.Owin
{
    public class DefaultLockManager : ILockManager
    {
        private readonly IDictionary<ICacheKey, TaskCompletionSource<IOwinResponse>> map = new Dictionary<ICacheKey, TaskCompletionSource<IOwinResponse>>();
        private readonly object @lock = new object();

        public Task<IOwinResponse> Lock(ICacheKey cacheKey, TimeSpan timeout)
        {
            return Lock(cacheKey, (TimeSpan?)timeout);
        }

        public Task<IOwinResponse> Lock(ICacheKey cacheKey)
        {
            return Lock(cacheKey, null);
        }

        private Task<IOwinResponse> Lock(ICacheKey cacheKey, TimeSpan? timeout)
        {
            TaskCompletionSource<IOwinResponse> taskSource;
            lock (@lock)
            {
                if (!map.TryGetValue(cacheKey, out taskSource))
                {
                    map.Add(cacheKey, new TaskCompletionSource<IOwinResponse>());
                }
            }
            return taskSource != null && timeout.HasValue
                ? taskSource.Task.Timeout(timeout.Value)
                : null;
        }

        public bool Unlock(ICacheKey cacheKey, IOwinResponse response)
        {
            TaskCompletionSource<IOwinResponse> taskSource;
            lock (@lock)
            {
                if (map.TryGetValue(cacheKey, out taskSource))
                {
                    map.Remove(cacheKey);
                }
            }

            if (taskSource == null)
            {
                return false;
            }

            taskSource.SetResult(response);
            return true;
        }
    }
}

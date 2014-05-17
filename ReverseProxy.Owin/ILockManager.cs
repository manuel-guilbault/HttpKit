using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReverseProxy.Owin
{
    public interface ILockManager
    {
        /// <summary>
        /// Try to acquire a lock on a cache entry, to prevent multiple requests from passing through
        /// at the same time for the same cache entry.
        /// If the lock is acquired (meaning the caller can go on and let the request pass through),
        /// null is returned.
        /// If another request already owns the lock for the same cache entry, a Task is returned. If
        /// the owning request passes through before the timeout expires, the task will be completed,
        /// and its result will be the owning request's pass through response.
        /// If the timeout expires before the owning request passes through, the task will fail.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        Task<IOwinResponse> Lock(ICacheKey cacheKey, TimeSpan timeout);

        /// <summary>
        /// Same as Lock(ICacheKey, TimeSpan), with infinite timeout.
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <returns></returns>
        Task<IOwinResponse> Lock(ICacheKey cacheKey);

        /// <summary>
        /// Release a lock on a cache entry. Any waiting requests will be notified so they can
        /// handle the response.
        /// If the cache entry was not locked, false is returned; otherwise true is returned.
        /// </summary>
        /// <param name="request"></param>
        bool Unlock(ICacheKey cacheKey, IOwinResponse response);
    }
}

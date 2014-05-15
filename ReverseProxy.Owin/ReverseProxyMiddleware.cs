using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HttpKit.Caching;
using HttpKit.Katana;

namespace ReverseProxy.Owin
{
    public class ReverseProxyMiddleware : OwinMiddleware
    {
        private readonly Configuration configuration;

        public ReverseProxyMiddleware(OwinMiddleware next, Configuration configuration)
            : base(next)
        {
            if (configuration == null) throw new ArgumentNullException("configuration");

            this.configuration = configuration;
        }

        public async override Task Invoke(IOwinContext context)
        {
            if (!IsCacheable(context.Request))
            {
                await Next.Invoke(context);
                return;
            }
            
            var cacheKey = GetCacheKey(context.Request);
            var cacheEntry = await TryGetFromCache(cacheKey);
            if (cacheEntry == null)
            {
            }
            else if (IsValid(cacheEntry, context.Request))
            {
                Output(context.Response, cacheEntry);
                return;
            }
            
            await Next.Invoke(context);

            if (IsCacheable(context.Response))
            {
                await Cache(cacheKey, context);
            }
        }

        private bool IsMethodSafe(IOwinRequest request)
        {
            return configuration.SafeMethods.Any(method => request.Method.Equals(method, StringComparison.InvariantCultureIgnoreCase));
        }

        private bool IsCacheable(IOwinRequest request)
        {
            if (!IsMethodSafe(request))
            {
                return false;
            }

            var cacheControl = request.GetCacheControl();
            if (cacheControl != null && (
                cacheControl.Has(RequestCacheDirective.NO_CACHE)
                || cacheControl.Has(RequestCacheDirective.NO_STORE)
                ))
            {
                return false;
            }

            return true;
        }

        private bool IsCacheable(IOwinResponse response)
        {
            var cacheControl = response.GetCacheControl();
            if (cacheControl != null && (
                cacheControl.Has(ResponseCacheDirective.NO_CACHE)
                || cacheControl.Has(ResponseCacheDirective.NO_STORE)
                || cacheControl.Has(ResponseCacheDirective.PRIVATE)
                ))
            {
                return false;
            }

            return true;
        }

        private CacheKey GetCacheKey(IOwinRequest request)
        {
            return new CacheKey(request);
        }

        private Task<ICacheEntry> TryGetFromCache(ICacheKey key)
        {
            return configuration.Cache.Get(key).ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    //TODO log
                    return null;
                }
                return task.Result;
            });
        }

        private IEnumerable<DateTime> GetExpirationDates(ICacheEntry cacheEntry, IOwinRequest request)
        {
            var date = cacheEntry.Response.GetDate();
            if (date == null)
            {
                yield break;
            }

            var requestCacheControl = request.GetCacheControl();
            if (requestCacheControl != null)
            {
                var maxAgeDirective = requestCacheControl.GetMaxAge();
                if (maxAgeDirective != null)
                {
                    yield return date.Value.Add(maxAgeDirective.Delta);
                }
            }

            var responseCacheControl = cacheEntry.Response.GetCacheControl();
            if (responseCacheControl != null)
            {
                var maxAgeDirective = responseCacheControl.GetMaxAge();
                if (maxAgeDirective != null)
                {
                    yield return date.Value.Add(maxAgeDirective.Delta);
                }
            }
        }

        private bool IsValid(ICacheEntry cacheEntry, IOwinRequest request)
        {
            var expirationDates = GetExpirationDates(cacheEntry, request).ToArray();
            if (!expirationDates.Any())
            {
                return false;
            }

            return expirationDates.Min() > Time.UtcNow;
        }

        private void Output(IOwinResponse response, ICacheEntry cacheEntry)
        {
            var variables = new[]
            {
                "owin.ResponseStatusCode",
                "owin.ResponseReasonPhrase",
                "owin.ResponseHeaders",
                "owin.ResponseBody"
            };
            foreach (var variable in variables)
            {
                object value;
                if (cacheEntry.Response.Environment.TryGetValue(variable, out value))
                {
                    response.Environment[variable] = value;
                }
                else
                {
                    response.Environment.Remove(variable);
                }
            }
        }

        private Task Cache(ICacheKey cacheKey, IOwinContext context)
        {
            return configuration.Cache.Set(cacheKey, new CacheEntry(context.Request, context.Response));
        }
    }
}

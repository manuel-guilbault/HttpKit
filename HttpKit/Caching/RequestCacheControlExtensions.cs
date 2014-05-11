using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpKit.Caching.CacheControl
{
    public static class RequestCacheControlExtensions
    {
        public static bool HasMaxAge(this RequestCacheControl cacheControl)
        {
            return cacheControl.Has(RequestCacheDirective.MAX_AGE);
        }

        public static bool HasMaxStale(this RequestCacheControl cacheControl)
        {
            return cacheControl.Has(RequestCacheDirective.MAX_STALE);
        }

        public static bool HasMinFresh(this RequestCacheControl cacheControl)
        {
            return cacheControl.Has(RequestCacheDirective.MIN_FRESH);
        }

        public static DeltaTimeRequestCacheDirective GetMaxAge(this RequestCacheControl cacheControl)
        {
            return (DeltaTimeRequestCacheDirective)cacheControl.Get(RequestCacheDirective.MAX_AGE);
        }

        public static OptionalDeltaTimeRequestCacheDirective GetMaxStale(this RequestCacheControl cacheControl)
        {
            return (OptionalDeltaTimeRequestCacheDirective)cacheControl.Get(RequestCacheDirective.MAX_STALE);
        }

        public static DeltaTimeRequestCacheDirective GetMinFresh(this RequestCacheControl cacheControl)
        {
            return (DeltaTimeRequestCacheDirective)cacheControl.Get(RequestCacheDirective.MIN_FRESH);
        }

        public static RequestCacheDirectiveExtension GetExtension(this RequestCacheControl cacheControl, string name)
        {
            return (RequestCacheDirectiveExtension)cacheControl.Get(name);
        }

        public static void RemoveMaxAge(this RequestCacheControl cacheControl)
        {
            cacheControl.Remove(RequestCacheDirective.MAX_AGE);
        }

        public static void RemoveMaxStale(this RequestCacheControl cacheControl)
        {
            cacheControl.Remove(RequestCacheDirective.MAX_STALE);
        }

        public static void RemoveMinFresh(this RequestCacheControl cacheControl)
        {
            cacheControl.Remove(RequestCacheDirective.MIN_FRESH);
        }
    }
}

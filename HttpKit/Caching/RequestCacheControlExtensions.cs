using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpKit.Caching
{
    public static class RequestCacheControlExtensions
    {
        public static bool HasMaxAge(this IRequestCacheControl cacheControl)
        {
            return cacheControl.Has(RequestCacheDirective.MAX_AGE);
        }

        public static bool HasMaxStale(this IRequestCacheControl cacheControl)
        {
            return cacheControl.Has(RequestCacheDirective.MAX_STALE);
        }

        public static bool HasMinFresh(this IRequestCacheControl cacheControl)
        {
            return cacheControl.Has(RequestCacheDirective.MIN_FRESH);
        }

        public static DeltaTimeRequestCacheDirective GetMaxAge(this IRequestCacheControl cacheControl)
        {
            return (DeltaTimeRequestCacheDirective)cacheControl.Get(RequestCacheDirective.MAX_AGE);
        }

        public static OptionalDeltaTimeRequestCacheDirective GetMaxStale(this IRequestCacheControl cacheControl)
        {
            return (OptionalDeltaTimeRequestCacheDirective)cacheControl.Get(RequestCacheDirective.MAX_STALE);
        }

        public static DeltaTimeRequestCacheDirective GetMinFresh(this IRequestCacheControl cacheControl)
        {
            return (DeltaTimeRequestCacheDirective)cacheControl.Get(RequestCacheDirective.MIN_FRESH);
        }

        public static RequestCacheDirectiveExtension GetExtension(this IRequestCacheControl cacheControl, string name)
        {
            return (RequestCacheDirectiveExtension)cacheControl.Get(name);
        }

        public static void RemoveMaxAge(this IRequestCacheControl cacheControl)
        {
            cacheControl.Remove(RequestCacheDirective.MAX_AGE);
        }

        public static void RemoveMaxStale(this IRequestCacheControl cacheControl)
        {
            cacheControl.Remove(RequestCacheDirective.MAX_STALE);
        }

        public static void RemoveMinFresh(this IRequestCacheControl cacheControl)
        {
            cacheControl.Remove(RequestCacheDirective.MIN_FRESH);
        }
    }
}

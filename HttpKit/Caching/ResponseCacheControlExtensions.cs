using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpKit.Caching.CacheControl
{
    public static class ResponseCacheControlExtensions
    {
        public static bool HasPrivate(this ResponseCacheControl cacheControl)
        {
            return cacheControl.Has(ResponseCacheDirective.PRIVATE);
        }

        public static bool HasNoCache(this ResponseCacheControl cacheControl)
        {
            return cacheControl.Has(ResponseCacheDirective.NO_CACHE);
        }

        public static bool HasMaxAge(this ResponseCacheControl cacheControl)
        {
            return cacheControl.Has(ResponseCacheDirective.MAX_AGE);
        }

        public static bool HasSharedMaxAge(this ResponseCacheControl cacheControl)
        {
            return cacheControl.Has(ResponseCacheDirective.SHARED_MAX_AGE);
        }

        public static FieldListResponseCacheDirective GetPrivate(this ResponseCacheControl cacheControl)
        {
            return (FieldListResponseCacheDirective)cacheControl.Get(ResponseCacheDirective.PRIVATE);
        }

        public static FieldListResponseCacheDirective GetNoCache(this ResponseCacheControl cacheControl)
        {
            return (FieldListResponseCacheDirective)cacheControl.Get(ResponseCacheDirective.NO_CACHE);
        }

        public static DeltaTimeResponseCacheDirective GetMaxAge(this ResponseCacheControl cacheControl)
        {
            return (DeltaTimeResponseCacheDirective)cacheControl.Get(ResponseCacheDirective.MAX_AGE);
        }

        public static DeltaTimeResponseCacheDirective GetSharedMaxAge(this ResponseCacheControl cacheControl)
        {
            return (DeltaTimeResponseCacheDirective)cacheControl.Get(ResponseCacheDirective.SHARED_MAX_AGE);
        }

        public static ResponseCacheDirectiveExtension GetExtension(this ResponseCacheControl cacheControl, string name)
        {
            return (ResponseCacheDirectiveExtension)cacheControl.Get(name);
        }

        public static void RemovePrivate(this ResponseCacheControl cacheControl)
        {
            cacheControl.Remove(ResponseCacheDirective.PRIVATE);
        }

        public static void RemoveNoCache(this ResponseCacheControl cacheControl)
        {
            cacheControl.Remove(ResponseCacheDirective.NO_CACHE);
        }

        public static void RemoveMaxAge(this ResponseCacheControl cacheControl)
        {
            cacheControl.Remove(ResponseCacheDirective.MAX_AGE);
        }

        public static void RemoveSharedMaxAge(this ResponseCacheControl cacheControl)
        {
            cacheControl.Remove(ResponseCacheDirective.SHARED_MAX_AGE);
        }
    }
}

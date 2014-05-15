using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpKit.Caching
{
    public static class ResponseCacheControlExtensions
    {
        public static bool HasPrivate(this IResponseCacheControl cacheControl)
        {
            return cacheControl.Has(ResponseCacheDirective.PRIVATE);
        }

        public static bool HasNoCache(this IResponseCacheControl cacheControl)
        {
            return cacheControl.Has(ResponseCacheDirective.NO_CACHE);
        }

        public static bool HasMaxAge(this IResponseCacheControl cacheControl)
        {
            return cacheControl.Has(ResponseCacheDirective.MAX_AGE);
        }

        public static bool HasSharedMaxAge(this IResponseCacheControl cacheControl)
        {
            return cacheControl.Has(ResponseCacheDirective.SHARED_MAX_AGE);
        }

        public static FieldListResponseCacheDirective GetPrivate(this IResponseCacheControl cacheControl)
        {
            return (FieldListResponseCacheDirective)cacheControl.Get(ResponseCacheDirective.PRIVATE);
        }

        public static FieldListResponseCacheDirective GetNoCache(this IResponseCacheControl cacheControl)
        {
            return (FieldListResponseCacheDirective)cacheControl.Get(ResponseCacheDirective.NO_CACHE);
        }

        public static DeltaTimeResponseCacheDirective GetMaxAge(this IResponseCacheControl cacheControl)
        {
            return (DeltaTimeResponseCacheDirective)cacheControl.Get(ResponseCacheDirective.MAX_AGE);
        }

        public static DeltaTimeResponseCacheDirective GetSharedMaxAge(this IResponseCacheControl cacheControl)
        {
            return (DeltaTimeResponseCacheDirective)cacheControl.Get(ResponseCacheDirective.SHARED_MAX_AGE);
        }

        public static ResponseCacheDirectiveExtension GetExtension(this IResponseCacheControl cacheControl, string name)
        {
            return (ResponseCacheDirectiveExtension)cacheControl.Get(name);
        }

        public static void RemovePrivate(this IResponseCacheControl cacheControl)
        {
            cacheControl.Remove(ResponseCacheDirective.PRIVATE);
        }

        public static void RemoveNoCache(this IResponseCacheControl cacheControl)
        {
            cacheControl.Remove(ResponseCacheDirective.NO_CACHE);
        }

        public static void RemoveMaxAge(this IResponseCacheControl cacheControl)
        {
            cacheControl.Remove(ResponseCacheDirective.MAX_AGE);
        }

        public static void RemoveSharedMaxAge(this IResponseCacheControl cacheControl)
        {
            cacheControl.Remove(ResponseCacheDirective.SHARED_MAX_AGE);
        }
    }
}

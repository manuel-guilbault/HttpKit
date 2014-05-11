using HttpKit.Caching;
using Microsoft.Owin;
using System;
using System.Linq;
using System.Text;

namespace HttpKit.Katana
{
    public static class RequestValidationExtensions
    {
        public static IEntityTagCondition GetIfMatch(this IOwinRequest request)
        {
            return request.Headers.GetIfMatch();
        }

        public static void SetIfMatch(this IOwinRequest request, IEntityTagCondition ifMatch)
        {
            request.Headers.SetIfMatch(ifMatch);
        }

        public static IEntityTagCondition GetIfNoneMatch(this IOwinRequest request)
        {
            return request.Headers.GetIfNoneMatch();
        }

        public static void SetIfNoneMatch(this IOwinRequest request, IEntityTagCondition ifNoneMatch)
        {
            request.Headers.SetIfNoneMatch(ifNoneMatch);
        }

        public static IRequestCacheControl GetCacheControl(this IOwinRequest request)
        {
            return request.Headers.GetRequestCacheControl();
        }

        public static void SetCacheControl(this IOwinRequest request, IRequestCacheControl cacheControl)
        {
            request.Headers.SetCacheControl(cacheControl);
        }
    }
}

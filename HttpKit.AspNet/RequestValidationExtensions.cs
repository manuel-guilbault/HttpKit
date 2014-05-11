using HttpKit.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HttpKit.AspNet
{
    public static class RequestValidationExtensions
    {
        public static IEntityTagCondition GetIfMatch(this HttpRequestBase request)
        {
            return request.Headers.GetIfMatch();
        }

        public static void SetIfMatch(this HttpRequestBase request, IEntityTagCondition ifMatch)
        {
            request.Headers.SetIfMatch(ifMatch);
        }

        public static IEntityTagCondition GetIfNoneMatch(this HttpRequestBase request)
        {
            return request.Headers.GetIfNoneMatch();
        }

        public static void SetIfNoneMatch(this HttpRequestBase request, IEntityTagCondition ifNoneMatch)
        {
            request.Headers.SetIfNoneMatch(ifNoneMatch);
        }

        public static IRequestCacheControl GetCacheControl(this HttpRequestBase request)
        {
            return request.Headers.GetRequestCacheControl();
        }

        public static void SetCacheControl(this HttpRequestBase request, IRequestCacheControl cacheControl)
        {
            request.Headers.SetCacheControl(cacheControl);
        }
    }
}

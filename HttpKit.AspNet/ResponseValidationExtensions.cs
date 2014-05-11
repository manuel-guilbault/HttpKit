using HttpKit.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HttpKit.AspNet
{
    public static class ResponseValidationExtensions
    {
        public static IEntityTag GetETag(this HttpResponseBase response)
        {
            return response.Headers.GetETag();
        }

        public static void SetETag(this HttpResponseBase response, IEntityTag etag)
        {
            response.Headers.SetETag(etag);
        }

        public static IResponseCacheControl GetCacheControl(this HttpResponseBase response)
        {
            return response.Headers.GetResponseCacheControl();
        }

        public static void SetCacheControl(this HttpResponseBase response, IResponseCacheControl cacheControl)
        {
            response.Headers.SetCacheControl(cacheControl);
        }
    }
}

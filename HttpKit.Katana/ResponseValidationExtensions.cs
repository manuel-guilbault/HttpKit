using HttpKit.Caching;
using Microsoft.Owin;
using System;
using System.Linq;
using System.Text;

namespace HttpKit.Katana
{
    public static class ResponseValidationExtensions
    {
        public static IEntityTag GetETag(this IOwinResponse response)
        {
            return response.Headers.GetETag();
        }

        public static void SetETag(this IOwinResponse response, IEntityTag etag)
        {
            response.Headers.SetETag(etag);
        }

        public static IResponseCacheControl GetCacheControl(this IOwinResponse response)
        {
            return response.Headers.GetResponseCacheControl();
        }

        public static void SetCacheControl(this IOwinResponse response, IResponseCacheControl cacheControl)
        {
            response.Headers.SetCacheControl(cacheControl);
        }
    }
}

using HttpKit.Caching;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpKit.AspNet
{
    public static class ValidationExtensions
    {
        private static readonly EntityTagParser entityTagParser = new EntityTagParser();
        private static readonly EntityTagConditionParser entityTagConditionParser = new EntityTagConditionParser(entityTagParser);
        private static readonly RequestCacheControlParser requestCacheControlParser = new RequestCacheControlParser();
        private static readonly ResponseCacheControlParser responseCacheControlParser = new ResponseCacheControlParser();

        public static IEntityTag GetETag(this NameValueCollection headers)
        {
            return headers.TryParse(ValidationHeaders.E_TAG, entityTagParser);
        }

        public static void SetETag(this NameValueCollection headers, IEntityTag etag)
        {
            if (etag == null) throw new ArgumentNullException("etag");

            headers[ValidationHeaders.E_TAG] = etag.ToString();
        }

        public static IEntityTagCondition GetIfMatch(this NameValueCollection headers)
        {
            return headers.TryParse(ValidationHeaders.IF_MATCH, entityTagConditionParser);
        }

        public static void SetIfMatch(this NameValueCollection headers, IEntityTagCondition ifMatch)
        {
            if (ifMatch == null) throw new ArgumentNullException("ifMatch");

            headers[ValidationHeaders.IF_MATCH] = ifMatch.ToString();
        }

        public static IEntityTagCondition GetIfNoneMatch(this NameValueCollection headers)
        {
            return headers.TryParse(ValidationHeaders.IF_NONE_MATCH, entityTagConditionParser);
        }

        public static void SetIfNoneMatch(this NameValueCollection headers, IEntityTagCondition ifNoneMatch)
        {
            if (ifNoneMatch == null) throw new ArgumentNullException("ifNoneMatch");

            headers[ValidationHeaders.IF_NONE_MATCH] = ifNoneMatch.ToString();
        }

        public static IRequestCacheControl GetRequestCacheControl(this NameValueCollection headers)
        {
            return headers.TryParse(CacheHeaders.CACHE_CONTROL, requestCacheControlParser);
        }

        public static void SetCacheControl(this NameValueCollection headers, IRequestCacheControl cacheControl)
        {
            if (cacheControl == null) throw new ArgumentNullException("cacheControl");

            headers[CacheHeaders.CACHE_CONTROL] = cacheControl.ToString();
        }

        public static IResponseCacheControl GetResponseCacheControl(this NameValueCollection headers)
        {
            return headers.TryParse(CacheHeaders.CACHE_CONTROL, responseCacheControlParser);
        }

        public static void SetCacheControl(this NameValueCollection headers, IResponseCacheControl cacheControl)
        {
            if (cacheControl == null) throw new ArgumentNullException("cacheControl");

            headers[CacheHeaders.CACHE_CONTROL] = cacheControl.ToString();
        }
    }
}

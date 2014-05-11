using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HttpKit.AspNet
{
    public static class RequestExpirationExtensions
    {
        public static DateTime? GetDate(this HttpRequestBase request)
        {
            return request.Headers.GetDate();
        }

        public static void SetDate(this HttpRequestBase request, DateTime date)
        {
            request.Headers.SetDate(date);
        }

        public static DateTime? GetIfModifiedSince(this HttpRequestBase request)
        {
            return request.Headers.GetIfModifiedSince();
        }

        public static void SetIfModifiedSince(this HttpRequestBase request, DateTime ifModifiedSince)
        {
            request.Headers.SetIfModifiedSince(ifModifiedSince);
        }

        public static DateTime? GetIfUnmodifiedSince(this HttpRequestBase request)
        {
            return request.Headers.GetIfUnmodifiedSince();
        }

        public static void SetIfUnmodifiedSince(this HttpRequestBase request, DateTime ifUnmodifiedSince)
        {
            request.Headers.SetIfUnmodifiedSince(ifUnmodifiedSince);
        }
    }
}

using HttpKit.Caching;
using Microsoft.Owin;
using System;
using System.Linq;
using System.Text;

namespace HttpKit.Katana
{
    public static class RequestExpirationExtensions
    {
        public static DateTime? GetDate(this IOwinRequest request)
        {
            return request.Headers.GetDate();
        }

        public static void SetDate(this IOwinRequest request, DateTime date)
        {
            request.Headers.SetDate(date);
        }

        public static DateTime? GetIfModifiedSince(this IOwinRequest request)
        {
            return request.Headers.GetIfModifiedSince();
        }

        public static void SetIfModifiedSince(this IOwinRequest request, DateTime ifModifiedSince)
        {
            request.Headers.SetIfModifiedSince(ifModifiedSince);
        }

        public static DateTime? GetIfUnmodifiedSince(this IOwinRequest request)
        {
            return request.Headers.GetIfUnmodifiedSince();
        }

        public static void SetIfUnmodifiedSince(this IOwinRequest request, DateTime ifUnmodifiedSince)
        {
            request.Headers.SetIfUnmodifiedSince(ifUnmodifiedSince);
        }
    }
}

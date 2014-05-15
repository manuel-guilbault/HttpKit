using HttpKit.Caching;
using HttpKit.Parsing;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpKit.AspNet
{
    public static class ExpirationExtensions
    {
        public static DateTime? GetDate(this NameValueCollection headers)
        {
            var value = headers[ExpirationHeaders.DATE];
            if (value == null) return null;

            return DateTimeParser.TryParse(value);
        }

        public static void SetDate(this NameValueCollection headers, DateTime date)
        {
            headers[ExpirationHeaders.DATE] = date.AsHttpDateTime();
        }

        public static TimeSpan? GetAge(this NameValueCollection headers)
        {
            var value = headers[ExpirationHeaders.AGE];
            if (value == null) return null;

            int seconds;
            if (!int.TryParse(value, out seconds)) return null;

            return TimeSpan.FromSeconds(seconds);
        }

        public static void SetAge(this NameValueCollection headers, TimeSpan age)
        {
            headers[ExpirationHeaders.AGE] = age.TotalSeconds.ToString();
        }

        public static DateTime? GetExpires(this NameValueCollection headers)
        {
            var value = headers[ExpirationHeaders.EXPIRES];
            if (value == null) return null;

            return DateTimeParser.TryParse(value);
        }

        public static void SetExpires(this NameValueCollection headers, DateTime expires)
        {
            headers[ExpirationHeaders.EXPIRES] = expires.AsHttpDateTime();
        }

        public static void SetExpires(this NameValueCollection headers, TimeSpan expires)
        {
            headers.SetExpires(DateTime.UtcNow.Add(expires));
        }

        public static DateTime? GetLastModified(this NameValueCollection headers)
        {
            var value = headers[ExpirationHeaders.LAST_MODIFIED];
            if (value == null) return null;

            return DateTimeParser.TryParse(value);
        }

        public static void SetLastModified(this NameValueCollection headers, DateTime lastModified)
        {
            headers[ExpirationHeaders.LAST_MODIFIED] = lastModified.AsHttpDateTime();
        }

        public static DateTime? GetIfModifiedSince(this NameValueCollection headers)
        {
            var value = headers[ExpirationHeaders.IF_MODIFIED_SINCE];
            if (value == null) return null;

            return DateTimeParser.TryParse(value);
        }

        public static void SetIfModifiedSince(this NameValueCollection headers, DateTime ifModifiedSince)
        {
            headers[ExpirationHeaders.IF_MODIFIED_SINCE] = ifModifiedSince.AsHttpDateTime();
        }

        public static DateTime? GetIfUnmodifiedSince(this NameValueCollection headers)
        {
            var value = headers[ExpirationHeaders.IF_UNMODIFIED_SINCE];
            if (value == null) return null;

            return DateTimeParser.TryParse(value);
        }

        public static void SetIfUnmodifiedSince(this NameValueCollection headers, DateTime ifUnmodifiedSince)
        {
            headers[ExpirationHeaders.IF_UNMODIFIED_SINCE] = ifUnmodifiedSince.AsHttpDateTime();
        }
    }
}

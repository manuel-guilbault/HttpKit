using HttpKit;
using HttpKit.Caching;
using HttpKit.Parsing;
using Microsoft.Owin;
using System;
using System.Linq;
using System.Net;
using System.Text;

namespace HttpKit.Katana
{
    public static class ExpirationExtensions
    {
		public static DateTime? GetDate(this IHeaderDictionary headers)
		{
			var value = headers[ExpirationHeaders.DATE];
			if (value == null) return null;

			return DateTimeParser.TryParse(value);
		}

        public static void SetDate(this IHeaderDictionary headers, DateTime date)
		{
			headers[ExpirationHeaders.DATE] = date.AsHttpDateTime();
		}

        public static TimeSpan? GetAge(this IHeaderDictionary headers)
        {
			var value = headers[ExpirationHeaders.AGE];
            if (value == null) return null;

            int seconds;
            if (!int.TryParse(value, out seconds)) return null;

            return TimeSpan.FromSeconds(seconds);
        }

        public static void SetAge(this IHeaderDictionary headers, TimeSpan age)
		{
			headers[ExpirationHeaders.AGE] = age.TotalSeconds.ToString();
		}

        public static DateTime? GetExpires(this IHeaderDictionary headers)
        {
			var value = headers[ExpirationHeaders.EXPIRES];
			if (value == null) return null;

            return DateTimeParser.TryParse(value);
        }

        public static void SetExpires(this IHeaderDictionary headers, DateTime expires)
		{
			headers[ExpirationHeaders.EXPIRES] = expires.AsHttpDateTime();
		}

        public static void SetExpires(this IHeaderDictionary headers, TimeSpan expires)
		{
			headers.SetExpires(DateTime.UtcNow.Add(expires));
		}

        public static DateTime? GetLastModified(this IHeaderDictionary headers)
        {
			var value = headers[ExpirationHeaders.LAST_MODIFIED];
			if (value == null) return null;

            return DateTimeParser.TryParse(value);
        }

        public static void SetLastModified(this IHeaderDictionary headers, DateTime lastModified)
		{
			headers[ExpirationHeaders.LAST_MODIFIED] = lastModified.AsHttpDateTime();
		}

        public static DateTime? GetIfModifiedSince(this IHeaderDictionary headers)
		{
			var value = headers[ExpirationHeaders.IF_MODIFIED_SINCE];
			if (value == null) return null;

            return DateTimeParser.TryParse(value);
		}

        public static void SetIfModifiedSince(this IHeaderDictionary headers, DateTime ifModifiedSince)
        {
			headers[ExpirationHeaders.IF_MODIFIED_SINCE] = ifModifiedSince.AsHttpDateTime();
        }

        public static DateTime? GetIfUnmodifiedSince(this IHeaderDictionary headers)
		{
			var value = headers[ExpirationHeaders.IF_UNMODIFIED_SINCE];
			if (value == null) return null;

            return DateTimeParser.TryParse(value);
		}

        public static void SetIfUnmodifiedSince(this IHeaderDictionary headers, DateTime ifUnmodifiedSince)
        {
			headers[ExpirationHeaders.IF_UNMODIFIED_SINCE] = ifUnmodifiedSince.AsHttpDateTime();
        }
    }
}

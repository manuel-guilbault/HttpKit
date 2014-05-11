using Microsoft.Owin;
using System;
using System.Linq;
using System.Text;

namespace HttpKit.Katana
{
    public static class ResponseExpirationExtensions
    {
        public static DateTime? GetDate(this IOwinResponse response)
        {
            return response.Headers.GetDate();
        }

        public static void SetDate(this IOwinResponse response, DateTime date)
        {
            response.Headers.SetDate(date);
        }

        public static TimeSpan? GetAge(this IOwinResponse response)
        {
            return response.Headers.GetAge();
        }

        public static void SetAge(this IOwinResponse response, TimeSpan age)
        {
            response.Headers.SetAge(age);
        }

        public static DateTime? GetExpires(this IOwinResponse response)
        {
            return response.Headers.GetExpires();
        }

        public static void SetExpires(this IOwinResponse response, DateTime expires)
        {
            response.Headers.SetExpires(expires);
        }

        public static void SetExpires(this IOwinResponse response, TimeSpan expires)
        {
            response.Headers.SetExpires(expires);
        }

        public static DateTime? GetLastModified(this IOwinResponse response)
        {
            return response.Headers.GetLastModified();
        }

        public static void SetLastModified(this IOwinResponse response, DateTime lastModified)
        {
            response.Headers.SetLastModified(lastModified);
        }
    }
}

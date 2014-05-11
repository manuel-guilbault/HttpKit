using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HttpKit.AspNet
{
    public static class ResponseExpirationExtensions
    {
        public static DateTime? GetDate(this HttpResponseBase response)
        {
            return response.Headers.GetDate();
        }

        public static void SetDate(this HttpResponseBase response, DateTime date)
        {
            response.Headers.SetDate(date);
        }

        public static TimeSpan? GetAge(this HttpResponseBase response)
        {
            return response.Headers.GetAge();
        }

        public static void SetAge(this HttpResponseBase response, TimeSpan age)
        {
            response.Headers.SetAge(age);
        }

        public static DateTime? GetExpires(this HttpResponseBase response)
        {
            return response.Headers.GetExpires();
        }

        public static void SetExpires(this HttpResponseBase response, DateTime expires)
        {
            response.Headers.SetExpires(expires);
        }

        public static void SetExpires(this HttpResponseBase response, TimeSpan expires)
        {
            response.Headers.SetExpires(expires);
        }

        public static DateTime? GetLastModified(this HttpResponseBase response)
        {
            return response.Headers.GetLastModified();
        }

        public static void SetLastModified(this HttpResponseBase response, DateTime lastModified)
        {
            response.Headers.SetLastModified(lastModified);
        }
    }
}

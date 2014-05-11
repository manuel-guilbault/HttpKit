using HttpKit.Ranges;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HttpKit.AspNet
{
    public static class ResponseRangeExtensions
    {
        public static IAcceptRange GetAcceptRange(this HttpResponseBase response)
        {
            return response.Headers.GetAcceptRange();
        }

        public static void SetAcceptRange(this HttpResponseBase response, IAcceptRange acceptRange)
        {
            response.Headers.SetAcceptRange(acceptRange);
        }

        public static IContentRange GetContentRange(this HttpResponseBase response)
        {
            return response.Headers.GetContentRange();
        }

        public static void SetContentRange(this HttpResponseBase response, IContentRange contentRange)
        {
            response.SetContentRange(contentRange);
        }
    }
}

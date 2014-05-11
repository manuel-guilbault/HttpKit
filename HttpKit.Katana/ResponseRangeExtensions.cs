using HttpKit.Ranges;
using Microsoft.Owin;
using System;
using System.Linq;
using System.Text;

namespace HttpKit.Katana
{
    public static class ResponseRangeExtensions
    {
        public static IAcceptRange GetAcceptRange(this IOwinResponse response)
        {
            return response.Headers.GetAcceptRange();
        }

        public static void SetAcceptRange(this IOwinResponse response, IAcceptRange acceptRange)
        {
            response.Headers.SetAcceptRange(acceptRange);
        }

        public static IContentRange GetContentRange(this IOwinResponse response)
        {
            return response.Headers.GetContentRange();
        }

        public static void SetContentRange(this IOwinResponse response, IContentRange contentRange)
        {
            response.SetContentRange(contentRange);
        }
    }
}

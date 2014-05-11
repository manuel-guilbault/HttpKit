using HttpKit.Ranges;
using Microsoft.Owin;
using System;
using System.Linq;
using System.Text;

namespace HttpKit.Katana
{
    public static class RequestRangeExtensions
    {
        public static IRange GetRange(this IOwinRequest request)
        {
            return request.Headers.GetRange();
        }

        public static void SetRange(this IOwinRequest request, IRange range)
        {
            request.Headers.SetRange(range);
        }

        public static IIfRange GetIfRange(this IOwinRequest request)
        {
            return request.Headers.GetIfRange();
        }

        public static void SetIfRange(this IOwinRequest request, IIfRange ifRange)
        {
            request.Headers.SetIfRange(ifRange);
        }
    }
}

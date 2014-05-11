using HttpKit.Ranges;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HttpKit.AspNet
{
    public static class RequestRangeExtensions
    {
        public static IRange GetRange(this HttpRequestBase request)
        {
            return request.Headers.GetRange();
        }

        public static void SetRange(this HttpRequestBase request, IRange range)
        {
            request.Headers.SetRange(range);
        }

        public static IIfRange GetIfRange(this HttpRequestBase request)
        {
            return request.Headers.GetIfRange();
        }

        public static void SetIfRange(this HttpRequestBase request, IIfRange ifRange)
        {
            request.Headers.SetIfRange(ifRange);
        }
    }
}

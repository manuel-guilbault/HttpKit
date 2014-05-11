using HttpKit.Ranges;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpKit.AspNet
{
    public static class RangeExtensions
    {
        private static readonly RangeParser rangeParser = new RangeParser();
        private static readonly AcceptRangeParser acceptRangeParser = new AcceptRangeParser();
        private static readonly ContentRangeParser contentRangeParser = new ContentRangeParser();
        private static readonly IfRangeParser ifRangeParser = new IfRangeParser();

        public static IRange GetRange(this NameValueCollection headers)
        {
            return headers.TryParse(RangeHeaders.RANGE, rangeParser);
        }

        public static void SetRange(this NameValueCollection headers, IRange range)
        {
            if (range == null) throw new ArgumentNullException("range");

            headers[RangeHeaders.RANGE] = range.ToString();
        }

        public static IAcceptRange GetAcceptRange(this NameValueCollection headers)
        {
            return headers.TryParse(RangeHeaders.ACCEPT_RANGES, acceptRangeParser);
        }

        public static void SetAcceptRange(this NameValueCollection headers, IAcceptRange acceptRange)
        {
            if (acceptRange == null) throw new ArgumentNullException("acceptRange");

            headers[RangeHeaders.ACCEPT_RANGES] = acceptRange.ToString();
        }

        public static IContentRange GetContentRange(this NameValueCollection headers)
        {
            return headers.TryParse(RangeHeaders.CONTENT_RANGE, contentRangeParser);
        }

        public static void SetContentRange(this NameValueCollection headers, IContentRange contentRange)
        {
            if (contentRange == null) throw new ArgumentNullException("contentRange");

            headers[RangeHeaders.CONTENT_RANGE] = contentRange.ToString();
        }

        public static IIfRange GetIfRange(this NameValueCollection headers)
        {
            return headers.TryParse(RangeHeaders.IF_RANGE, ifRangeParser);
        }

        public static void SetIfRange(this NameValueCollection headers, IIfRange ifRange)
        {
            if (ifRange == null) throw new ArgumentNullException("ifRange");

            headers[RangeHeaders.IF_RANGE] = ifRange.ToString();
        }
    }
}

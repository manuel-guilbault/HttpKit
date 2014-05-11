using HttpKit;
using HttpKit.Caching;
using HttpKit.Parsing;
using HttpKit.Ranges;
using Microsoft.Owin;
using System;
using System.Linq;
using System.Text;

namespace HttpKit.Katana
{
    public static class RangeExtensions
    {
        private static readonly RangeParser rangeParser = new RangeParser();
        private static readonly AcceptRangeParser acceptRangeParser = new AcceptRangeParser();
        private static readonly ContentRangeParser contentRangeParser = new ContentRangeParser();
        private static readonly IfRangeParser ifRangeParser = new IfRangeParser();

		public static IRange GetRange(this IHeaderDictionary headers)
		{
            return headers.TryParse(RangeHeaders.RANGE, rangeParser);
		}

        public static void SetRange(this IHeaderDictionary headers, IRange range)
        {
            if (range == null) throw new ArgumentNullException("range");

			headers[RangeHeaders.RANGE] = range.ToString();
        }

        public static IAcceptRange GetAcceptRange(this IHeaderDictionary headers)
        {
            return headers.TryParse(RangeHeaders.ACCEPT_RANGES, acceptRangeParser);
        }

        public static void SetAcceptRange(this IHeaderDictionary headers, IAcceptRange acceptRange)
		{
			if (acceptRange == null) throw new ArgumentNullException("acceptRange");

			headers[RangeHeaders.ACCEPT_RANGES] = acceptRange.ToString();
		}

        public static IContentRange GetContentRange(this IHeaderDictionary headers)
        {
            return headers.TryParse(RangeHeaders.CONTENT_RANGE, contentRangeParser);
        }

        public static void SetContentRange(this IHeaderDictionary headers, IContentRange contentRange)
		{
			if (contentRange == null) throw new ArgumentNullException("contentRange");

			headers[RangeHeaders.CONTENT_RANGE] = contentRange.ToString();
		}

        public static IIfRange GetIfRange(this IHeaderDictionary headers)
        {
            return headers.TryParse(RangeHeaders.IF_RANGE, ifRangeParser);
		}

        public static void SetIfRange(this IHeaderDictionary headers, IIfRange ifRange)
        {
            if (ifRange == null) throw new ArgumentNullException("ifRange");

			headers[RangeHeaders.IF_RANGE] = ifRange.ToString();
        }
    }
}

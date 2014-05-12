using HttpKit.Mvc.ActionResults;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HttpKit
{
    public static class ActionResultsExtensions
    {
        public static ActionResult Merge(this ActionResult result, ActionResult other)
        {
            return new AggregateResult(result, other);
        }

        public static ActionResult Header(this Controller controller, string name, string value)
        {
            return new HeadersResult(name, value);
        }

        public static ActionResult Header(this Controller controller, NameValueCollection headers)
        {
            return new HeadersResult(headers);
        }

        public static ActionResult Header(this ActionResult result, string name, string value)
        {
            return result.Merge(new HeadersResult(name, value));
        }

        public static ActionResult Header(this ActionResult result, NameValueCollection headers)
        {
            return result.Merge(new HeadersResult(headers));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HttpKit.Mvc.ActionResults
{
    public class HeadersResult : ActionResult
    {
        private readonly NameValueCollection headers;

        public HeadersResult(string name, string value)
            : this(new NameValueCollection())
        {
            if (name == null) throw new ArgumentNullException("name");
            if (value == null) throw new ArgumentNullException("value");

            headers.Add(name, value);
        }

        public HeadersResult(NameValueCollection headers)
        {
            if (headers == null) throw new ArgumentNullException("headers");

            this.headers = headers;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.Headers.Add(headers);
        }
    }
}

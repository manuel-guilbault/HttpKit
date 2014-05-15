using Microsoft.Owin;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ReverseProxy.Owin.Test
{
    public class OwinContextMock : OwinContext
    {
        public OwinContextMock()
            : this(CancellationToken.None)
        {
        }

        public OwinContextMock(CancellationToken cancellationToken)
            : base(new Dictionary<string, object>()
            {
                { "owin.RequestBody", new MemoryStream() },
                { "owin.RequestHeaders", new Dictionary<string, string[]>() },
                { "owin.RequestMethod", "GET" },
                { "owin.RequestPath", "/" },
                { "owin.RequestPathBase", "http://localhost" },
                { "owin.RequestProtocol", "HTTP/1.1" },
                { "owin.RequestQueryString", "" },
                { "owin.RequestScheme", "http" },

                { "owin.ResponseBody", new MemoryStream() },
                { "owin.ResponseHeaders", new Dictionary<string, string[]>() },
                //{ "owin.ResponseStatusCode", 200 },
                //{ "owin.ResponseReasonPhrase", "OK" }
                //{ "owin.ResponseProtocol", "HTTP/1.1" },

                { "owin.CallCancelled", cancellationToken },
                { "owin.Version", "1.0" }
            })
        {
        }
    }
}

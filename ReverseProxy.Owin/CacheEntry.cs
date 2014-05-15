using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReverseProxy.Owin
{
    public class CacheEntry : ICacheEntry
    {
        public CacheEntry(IOwinRequest request, IOwinResponse response)
        {
            if (request == null) throw new ArgumentNullException("request");
            if (response == null) throw new ArgumentNullException("response");

            this.Request = request;
            this.Response = response;
        }

        public IOwinRequest Request { get; private set; }
        public IOwinResponse Response { get; private set; }
    }
}

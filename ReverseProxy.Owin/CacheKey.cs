using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReverseProxy.Owin
{
    public class CacheKey : ICacheKey
    {
        private readonly IOwinRequest request;

        public CacheKey(IOwinRequest request)
        {
            if (request == null) throw new ArgumentNullException("request");

            this.request = request;
        }

        public Uri Uri
        {
            get { return request.Uri; }
        }
    }
}

using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReverseProxy.Owin
{
    public interface ICacheEntry
    {
        IOwinRequest Request { get; }
        IOwinResponse Response { get; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReverseProxy.Owin
{
    public interface ICacheKey
    {
        Uri Uri { get; }
    }
}

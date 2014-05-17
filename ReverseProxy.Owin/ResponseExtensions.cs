using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReverseProxy.Owin
{
    public static class ResponseExtensions
    {
        public static void CopyTo(this IOwinResponse source, IOwinResponse destination)
        {
            var variables = new[]
            {
                "owin.ResponseStatusCode",
                "owin.ResponseReasonPhrase",
                "owin.ResponseHeaders",
                "owin.ResponseBody"
            };
            foreach (var variable in variables)
            {
                object value;
                if (source.Environment.TryGetValue(variable, out value))
                {
                    destination.Environment[variable] = value;
                }
                else
                {
                    destination.Environment.Remove(variable);
                }
            }
        }
    }
}

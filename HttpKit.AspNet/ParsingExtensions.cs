using HttpKit.Parsing;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpKit.AspNet
{
    public static class ParsingExtensions
    {
        public static T TryParse<T>(this NameValueCollection headers, string name, IHeaderParser<T> parser)
        {
            var value = headers[name];
            if (value == null) return default(T);

            try
            {
                return new HeaderReader<T>(parser).Read(value);
            }
            catch (ParsingException)
            {
                return default(T);
            }
        }
    }
}

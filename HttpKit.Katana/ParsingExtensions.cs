using HttpKit.Parsing;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpKit.Katana
{
    public static class ParsingExtensions
    {
        public static T TryParse<T>(this IHeaderDictionary headers, string name, IHeaderParser<T> parser)
            where T : class
        {
            var value = headers[name];
            if (value == null) return null;

            try
            {
                return new HeaderReader<T>(parser).Read(value);
            }
            catch (ParsingException)
            {
                return null;
            }
        }
    }
}

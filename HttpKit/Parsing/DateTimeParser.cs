using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpKit.Parsing
{
    public class DateTimeParser : IHeaderParser<DateTime?>
    {
        public static DateTime? TryParse(string value)
        {
            DateTime dateTime;
            if (!DateTime.TryParseExact(value, "r", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal, out dateTime))
            {
                return null;
            }
            return dateTime;
        }

        public DateTime? Parse(Tokenizer tokenizer)
        {
            //TODO: rewrite this method, to try to parse a datetime from the current position instead of whole value.

            var value = TryParse(tokenizer.ToString());
            if (value.HasValue)
            {
                tokenizer.Move(tokenizer.ToString().Length);
            }
            return value;
        }
    }
}

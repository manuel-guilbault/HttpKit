using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security;
using System.Text;

namespace HttpKit.Parsing
{
    public class ParsingException : Exception
    {
        private readonly string message;

        public ParsingException(string message, string parsedValue, int position)
            : base(message)
        {
            if (message == null) throw new ArgumentNullException("message");
            if (parsedValue == null) throw new ArgumentNullException("parsedValue");

            this.message = message;
            this.ParsedValue = parsedValue;
            this.Position = position;
        }

        [SecuritySafeCritical]
        protected ParsingException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public string ParsedValue { get; private set; }

        public int Position { get; private set; }

        public override string Message
        {
            get
            {
                return string.Format(
                    "{0} in '{1}', at position {2}",
                    message,
                    ParsedValue,
                    Position
                );
            }
        }
    }
}

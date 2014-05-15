using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HttpKit.Parsing
{
    public class Tokenizer
    {
        private readonly string value;

        private int position = 0;

        public Tokenizer(string value)
        {
            if (value == null) throw new ArgumentNullException("value");

            this.value = value;
        }

        public string Value
        {
            get { return value; }
        }

        public int Position
        {
            get { return position; }
        }

        public void Move(int offset = 1)
        {
            position += offset;
        }

        public override string ToString()
        {
            return value.Substring(position);
        }
    }
}

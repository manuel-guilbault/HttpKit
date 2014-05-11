using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HttpKit.Ranges
{
    public class InstanceLength : IInstanceLength
    {
        public static readonly IInstanceLength Unknown = new UnknownInstanceLength();

        private readonly long value;

        private InstanceLength()
        {
            value = -1;
        }

        public InstanceLength(long value)
        {
            if (value < 0) throw new ArgumentException("value must be equal to or greater than zero", "value");

            this.value = value;
        }

        public long Value
        {
            get { return value; }
        }

        public override string ToString()
        {
            return value.ToString();
        }

        public override int GetHashCode()
        {
            return unchecked((int)Value);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as IInstanceLength);
        }

        public virtual bool Equals(IInstanceLength other)
        {
            return other != null && Value == other.Value;
        }

        private class UnknownInstanceLength : InstanceLength
        {
            public override string ToString()
            {
                return "*";
            }

            public override int GetHashCode()
            {
                return 1;
            }

            public override bool Equals(IInstanceLength other)
            {
                return other != null && ReferenceEquals(this, other);
            }
        }
    }
}

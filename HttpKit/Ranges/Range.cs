using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HttpKit.Ranges
{
    public class Range : IRange
    {
        private readonly IRangeUnit unit;
        private readonly ISubRange[] ranges;

        public Range(IRangeUnit unit, params ISubRange[] ranges)
        {
            if (unit == null) throw new ArgumentNullException("unit");
            if (ranges == null) throw new ArgumentNullException("ranges");
            if (!ranges.Any()) throw new ArgumentException("ranges cannot be empty", "ranges");
            if (ranges.Any(r => r == null)) throw new ArgumentException("ranges cannot contain null elements", "ranges");

            this.unit = unit;
            this.ranges = ranges.ToArray();
        }

        public IRangeUnit Unit
        {
            get { return unit; }
        }

        public ISubRange[] Ranges
        {
            get { return ranges; }
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append(unit);
            builder.Append("=");
            for (int i = 0; i < ranges.Length; ++i)
            {
                if (i > 0)
                {
                    builder.Append(",");
                }
                builder.Append(ranges[i]);
            }
            return builder.ToString();
        }

        public override int GetHashCode()
        {
            return unchecked(Unit.GetHashCode() * 17 + Ranges.GetHashCode());
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as IRange);
        }

        public virtual bool Equals(IRange other)
        {
            return other != null && Unit.Equals(other.Unit)
                && Ranges.Length == other.Ranges.Length
                && Ranges.Intersect(other.Ranges).Count() == Ranges.Length;
        }
    }
}

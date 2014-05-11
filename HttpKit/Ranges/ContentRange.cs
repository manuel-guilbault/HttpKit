using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HttpKit.Ranges
{
    public class ContentRange : IContentRange
    {
        private readonly IRangeUnit unit;
        private readonly IContentSubRange range;
        private readonly IInstanceLength instanceLength;

        public ContentRange(IRangeUnit unit, IContentSubRange range, IInstanceLength instanceLength)
        {
            if (unit == null) throw new ArgumentNullException("unit");
            if (range == null) throw new ArgumentNullException("range");
            if (instanceLength == null) throw new ArgumentNullException("instanceLength");

            this.unit = unit;
            this.range = range;
            this.instanceLength = instanceLength;
        }

        public IRangeUnit Unit
        {
            get { return unit; }
        }

        public IContentSubRange Range
        {
            get { return range; }
        }

        public IInstanceLength InstanceLength
        {
            get { return instanceLength; }
        }

        public override string ToString()
        {
            return string.Concat(unit, " ", range, "/", instanceLength);
        }

        public override int GetHashCode()
        {
            return unchecked(unit.GetHashCode() * 17 + range.GetHashCode() * 23 + instanceLength.GetHashCode());
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as IContentRange);
        }

        public virtual bool Equals(IContentRange other)
        {
            return other != null && Unit.Equals(other.Unit) && Range.Equals(other.Range) && InstanceLength.Equals(other.InstanceLength);
        }
    }
}

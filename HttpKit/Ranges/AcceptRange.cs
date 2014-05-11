using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HttpKit.Ranges
{
    public class AcceptRange : IAcceptRange
    {
        public static readonly IAcceptRange None = new AcceptNoRange();

        private readonly IRangeUnit[] units;

        private AcceptRange()
        {
        }

        public AcceptRange(params IRangeUnit[] units)
        {
            if (units == null) throw new ArgumentNullException("units");
            if (!units.Any()) throw new ArgumentException("units cannot be empty", "units");
            if (units.Any(u => u == null)) throw new ArgumentException("units cannot contain null elements", "units");

            this.units = units.ToArray();
        }

        public IRangeUnit[] Units
        {
            get { return units; }
        }

        public override string ToString()
        {
            return string.Join(",", units.Select(u => u.ToString()));
        }

        public override int GetHashCode()
        {
            var hash = units.Length;
            foreach (var unit in units)
            {
                hash = unchecked(hash * 17 + unit.GetHashCode());
            }
            return hash;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as IAcceptRange);
        }

        public virtual bool Equals(IAcceptRange other)
        {
            return other != null && Units.Length == other.Units.Length && Units.Intersect(other.Units).Count() == Units.Length;
        }

        private class AcceptNoRange : AcceptRange
        {
            public override string ToString()
            {
                return "none";
            }
        }
    }
}

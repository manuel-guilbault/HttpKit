using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace HttpKit.Ranges
{
    public class RangeUnit : IRangeUnit
    {
        private readonly string name;

        public RangeUnit(string name)
        {
            if (name == null) throw new ArgumentNullException("name");
            if (name == "") throw new ArgumentException("name must not be empty", "name");

            this.name = name;
        }

        public string Name
        {
            get { return name; }
        }

        public override string ToString()
        {
            return name;
        }

        public override int GetHashCode()
        {
            return name.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as IRangeUnit);
        }

        public virtual bool Equals(IRangeUnit other)
        {
            return other != null && name == other.Name;
        }
    }
}

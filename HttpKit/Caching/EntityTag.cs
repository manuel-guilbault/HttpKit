using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace HttpKit.Caching
{
    public class EntityTag : IEntityTag
    {
        public const EntityTagComparisonType defaultComparisonType = EntityTagComparisonType.Strong;

        private readonly bool isWeak;
        private readonly string value;

        public EntityTag(string value)
            : this(false, value)
        {
        }

        public EntityTag(bool isWeak, string value)
        {
            if (value == null) throw new ArgumentNullException("value");
            if (value == "") throw new ArgumentException("value cannot be empty", "value");

            this.isWeak = isWeak;
            this.value = value;
        }

        public bool IsWeak
        {
            get { return isWeak; }
        }

        public string Value
        {
            get { return value; }
        }

		public override int GetHashCode()
        {
            return unchecked(value.GetHashCode() * (isWeak ? 23 : 1));
		}

		public override bool Equals(object other)
		{
            return Equals(other as IEntityTag);
		}

        public bool Equals(IEntityTag other)
        {
            return Equals(other, defaultComparisonType);
        }

        public bool Equals(IEntityTag other, EntityTagComparisonType comparisonType)
        {
            if (other == null) return false;

            switch (comparisonType)
            {
                case EntityTagComparisonType.Strong:
                    return !IsWeak && !other.IsWeak && Value == other.Value;

                case EntityTagComparisonType.Weak:
                    return Value == other.Value;

                default:
                    throw new InvalidProgramException("Unknown EntityTagComparisonType." + comparisonType);
            }
        }

        public override string ToString()
        {
            var weakFlag = IsWeak ? "W/" : "";
            return string.Concat(weakFlag, @"""", value, @"""");
        }
    }
}

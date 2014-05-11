using HttpKit.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HttpKit.Ranges
{
    public class IfRange : IIfRange
    {
        private readonly IfRangeType type;
        private readonly object value;

        public IfRange(DateTime lastModified)
        {
            type = IfRangeType.LastModified;
            value = lastModified;
        }

        public IfRange(IEntityTag entityTag)
        {
            if (entityTag == null) throw new ArgumentNullException("entityTag");

            type = IfRangeType.EntityTag;
            value = entityTag;
        }

        public IfRangeType Type
        {
            get { return type; }
        }

        public IEntityTag EntityTag
        {
            get
            {
                if (type != IfRangeType.EntityTag) throw new InvalidOperationException("Type must be EntityTag");
                return (IEntityTag)value;
            }
        }

        public DateTime LastModified
        {
            get
            {
                if (type != IfRangeType.LastModified) throw new InvalidOperationException("Type must be LastModified");
                return (DateTime)value;
            }
        }

        public override string ToString()
        {
            switch (Type)
            {
                case IfRangeType.EntityTag:
                    return EntityTag.ToString();

                case IfRangeType.LastModified:
                    return LastModified.AsHttpDateTime();

                default:
                    throw new InvalidProgramException("Unknown IfRangeType: " + Type);
            }
        }

        public override int GetHashCode()
        {
            switch (Type)
            {
                case IfRangeType.EntityTag:
                    return unchecked(Type.GetHashCode() * 17 + EntityTag.GetHashCode());

                case IfRangeType.LastModified:
                    return unchecked(Type.GetHashCode() * 17 + LastModified.GetHashCode());

                default:
                    throw new InvalidProgramException("Unknown IfRangeType: " + Type);
            }
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as IIfRange);
        }

        public virtual bool Equals(IIfRange other)
        {
            if (other == null || Type != other.Type)
            {
                return false;
            }

            switch (Type)
            {
                case IfRangeType.EntityTag:
                    return EntityTag.Equals(other.EntityTag);

                case IfRangeType.LastModified:
                    return LastModified == other.LastModified;

                default:
                    throw new InvalidProgramException("Unknown IfRangeType: " + Type);
            }
        }
    }
}

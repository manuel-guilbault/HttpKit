using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpKit.Caching
{
    public class EntityTagEqualityComparer : IEqualityComparer<IEntityTag>
    {
        private readonly EntityTagComparisonType comparisonType;

        public EntityTagEqualityComparer()
            : this(EntityTag.defaultComparisonType)
        {
        }

        public EntityTagEqualityComparer(EntityTagComparisonType comparisonType)
        {
            this.comparisonType = comparisonType;
        }

        public bool Equals(IEntityTag x, IEntityTag y)
        {
            return x != null && x.Equals(y, comparisonType);
        }

        public int GetHashCode(IEntityTag obj)
        {
            if (obj == null) throw new ArgumentNullException("obj");

            return obj.GetHashCode(comparisonType);
        }
    }
}

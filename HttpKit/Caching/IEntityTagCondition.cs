using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpKit.Caching
{
    public interface IEntityTagCondition
    {
        IEnumerable<IEntityTag> ValidTags { get; }

        bool IsValid(IEntityTag entityTag);
        bool IsValid(IEntityTag entityTag, EntityTagComparisonType comparisonType);
        bool IsValid(IEnumerable<IEntityTag> entityTags);
        bool IsValid(IEnumerable<IEntityTag> entityTags, EntityTagComparisonType comparisonType);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpKit.Caching
{
    public interface IEntityTag : IEquatable<IEntityTag>
    {
        bool IsWeak { get; }
        string Value { get; }
        bool Equals(IEntityTag other, EntityTagComparisonType comparisonType);
    }
}

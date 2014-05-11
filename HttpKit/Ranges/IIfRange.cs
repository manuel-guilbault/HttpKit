using HttpKit.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpKit.Ranges
{
    public interface IIfRange : IEquatable<IIfRange>
    {
        IfRangeType Type { get; }
        IEntityTag EntityTag { get; }
        DateTime LastModified { get; }
    }
}

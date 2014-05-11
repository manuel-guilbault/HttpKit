using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpKit.Ranges
{
    public interface IContentRange : IEquatable<IContentRange>
    {
        IRangeUnit Unit { get; }
        IContentSubRange Range { get; }
        IInstanceLength InstanceLength { get; }
    }
}

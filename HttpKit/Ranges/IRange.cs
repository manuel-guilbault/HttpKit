using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpKit.Ranges
{
    public interface IRange : IEquatable<IRange>
    {
        IRangeUnit Unit { get; }
        ISubRange[] Ranges { get; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpKit.Ranges
{
    public interface ISubRange : IEquatable<ISubRange>
    {
        SubRangeType Type { get; }
        long From { get; }
        long To { get; }
    }
}

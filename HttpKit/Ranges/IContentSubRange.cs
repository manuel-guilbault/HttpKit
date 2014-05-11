using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpKit.Ranges
{
    public interface IContentSubRange : IEquatable<IContentSubRange>
    {
        long StartAt { get; }
        long EndAt { get; }
    }
}

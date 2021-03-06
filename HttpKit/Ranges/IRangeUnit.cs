﻿using HttpKit.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpKit.Ranges
{
    public interface IRangeUnit : IEquatable<IRangeUnit>
    {
        string Name { get; }
    }
}

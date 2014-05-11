using System;
using System.Linq;
using System.Text;

namespace HttpKit.Caching
{
    public static class ValidationHeaders
    {
        public const string E_TAG = "ETag";
        public const string IF_MATCH = "If-Match";
        public const string IF_NONE_MATCH = "If-None-Match";
    }
}

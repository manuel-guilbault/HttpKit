using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReverseProxy.Owin
{
    public static class Time
    {
        private static DateTime? fixedCurrentTime;

        public static DateTime UtcNow
        {
            get { return fixedCurrentTime ?? DateTime.UtcNow; }
        }

        public static void SetFixedCurrentTime(DateTime value)
        {
            fixedCurrentTime = value;
        }
    }
}

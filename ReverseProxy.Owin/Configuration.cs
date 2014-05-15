using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReverseProxy.Owin
{
    public class Configuration
    {
        private readonly ICache cache;

        private string[] safeMethods = new[]
        {
            "get",
            "head"
        };

        public Configuration(ICache cache)
        {
            if (cache == null) throw new ArgumentNullException("cache");

            this.cache = cache;
        }

        public ICache Cache
        {
            get { return cache; }
        }

        public string[] SafeMethods
        {
            get { return safeMethods; }
            set
            {
                if (value == null) throw new ArgumentNullException();
                safeMethods = value;
            }
        }
    }
}

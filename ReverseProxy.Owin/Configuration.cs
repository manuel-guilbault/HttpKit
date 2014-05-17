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
        private readonly ILockManager lockManager;

        private string[] safeMethods = new[]
        {
            "get",
            "head"
        };

        public Configuration(ICache cache, ILockManager lockManager)
        {
            if (cache == null) throw new ArgumentNullException("cache");
            if (lockManager == null) throw new ArgumentNullException("lockManager");

            this.cache = cache;
            this.lockManager = lockManager;
        }

        public ICache Cache
        {
            get { return cache; }
        }

        public ILockManager LockManager
        {
            get { return lockManager; }
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

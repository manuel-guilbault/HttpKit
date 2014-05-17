using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReverseProxy.Owin.Test.Mocks
{
    public class ConfigurationMock
    {
        public ConfigurationMock()
        {
            CacheMock = new CacheMock();
            LockManagerMock = new LockManagerMock();
            Configuration = new Configuration(CacheMock.Object, LockManagerMock.Object);
        }

        public Configuration Configuration { get; private set; }
        public Mock<ICache> CacheMock { get; private set; }
        public Mock<ILockManager> LockManagerMock { get; private set; }
    }
}

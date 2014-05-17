using Microsoft.Owin;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReverseProxy.Owin.Test.Mocks
{
    public class LockManagerMock : Mock<ILockManager>
    {
        public LockManagerMock()
        {
            Setup(lm => lm.Lock(It.IsAny<ICacheKey>())).Returns<Task<IOwinResponse>>(null).Verifiable();
            Setup(lm => lm.Lock(It.IsAny<ICacheKey>(), It.IsAny<TimeSpan>())).Returns<Task<IOwinResponse>>(null).Verifiable();
            Setup(lm => lm.Unlock(It.IsAny<ICacheKey>(), It.IsAny<IOwinResponse>())).Verifiable();
        }
    }
}

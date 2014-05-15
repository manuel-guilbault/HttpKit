using Microsoft.Owin;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReverseProxy.Owin.Test.Mocks
{
    public class CacheMock : Mock<ICache>
    {
        public CacheMock()
        {
            Setup(cache => cache.Get(It.IsAny<ICacheKey>()))
                .Returns(Task.FromResult<ICacheEntry>(null))
                .Verifiable();
            Setup(cache => cache.Set(It.IsAny<ICacheKey>(), It.IsAny<ICacheEntry>()))
                .Returns(Task.FromResult<object>(null))
                .Verifiable();
            Setup(cache => cache.Remove(It.IsAny<ICacheKey>()))
                .Returns(Task.FromResult<object>(null))
                .Verifiable();
        }
    }
}

using Microsoft.Owin;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReverseProxy.Owin.Test
{
    public static class MockExtensions
    {
        public static void AssertPassedThrough(this Mock<OwinMiddleware> next)
        {
            next.Verify(n => n.Invoke(It.IsAny<IOwinContext>()), Times.Once, "Request SHOULD have passed trought");
        }

        public static void AssertPassedThrough(this Mock<OwinMiddleware> next, IOwinContext context)
        {
            next.Verify(n => n.Invoke(context), Times.Once, "Request SHOULD have passed trought");
        }

        public static void AssertNotPassedThrough(this Mock<OwinMiddleware> next)
        {
            next.Verify(n => n.Invoke(It.IsAny<IOwinContext>()), Times.Never, "Request SHOULD NOT have passed trought");
        }

        public static void AssertSet(this Mock<ICache> cache, ICacheKey key, ICacheEntry entry)
        {
            cache.Verify(c => c.Set(key, entry), Times.Once, "Response SHOULD have been cached");
        }

        public static void AssertNotSet(this Mock<ICache> cache, ICacheKey key)
        {
            cache.Verify(c => c.Set(key, It.IsAny<ICacheEntry>()), Times.Never, "Response SHOULD NOT have been cached");
        }

        public static void AssertRemoved(this Mock<ICache> cache, ICacheKey key)
        {
            cache.Verify(c => c.Remove(key), Times.Once, "Cached responses SHOULD have been removed from cache");
        }

        public static void AssertLock(this Mock<ILockManager> lockManager)
        {
            lockManager.Verify(lm => lm.Lock(It.IsAny<ICacheKey>()), Times.Once);
        }

        public static void AssertLock(this Mock<ILockManager> lockManager, ICacheKey cacheKey)
        {
            lockManager.Verify(lm => lm.Lock(cacheKey), Times.Once);
        }

        public static void AssertUnlock(this Mock<ILockManager> lockManager)
        {
            lockManager.Verify(lm => lm.Unlock(It.IsAny<ICacheKey>(), It.IsAny<IOwinResponse>()), Times.Once);
        }

        public static void AssertUnlock(this Mock<ILockManager> lockManager, ICacheKey cacheKey)
        {
            lockManager.Verify(lm => lm.Unlock(cacheKey, It.IsAny<IOwinResponse>()), Times.Once);
        }

        public static void AssertUnlock(this Mock<ILockManager> lockManager, ICacheKey cacheKey, IOwinResponse response)
        {
            lockManager.Verify(lm => lm.Unlock(cacheKey, response), Times.Once);
        }
    }
}

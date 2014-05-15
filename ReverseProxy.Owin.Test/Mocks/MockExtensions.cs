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
            next.AssertPassedThrough(It.IsAny<IOwinContext>());
        }

        public static void AssertPassedThrough(this Mock<OwinMiddleware> next, IOwinContext context)
        {
            next.Verify(n => n.Invoke(context), Times.Once, "Request SHOULD have passed trought");
        }

        public static void AssertNotPassedThrough(this Mock<OwinMiddleware> next)
        {
            next.Verify(n => n.Invoke(It.IsAny<IOwinContext>()), Times.Never, "Request SHOULD NOT have passed trought");
        }

        public static void AssertSet(this Mock<ICache> cache, IOwinResponse response)
        {
            cache.AssertSet(response);
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
    }
}

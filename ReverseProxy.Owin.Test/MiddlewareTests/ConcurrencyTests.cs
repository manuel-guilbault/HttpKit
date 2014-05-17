using Microsoft.Owin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ReverseProxy.Owin.Test.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReverseProxy.Owin.Test.MiddlewareTests
{
    [TestClass]
    public class ConcurrencyTests
    {
        [TestMethod]
        public void RequestPassingThroughShouldLockThenUnlock()
        {
            var context = new OwinContextMock();

            var next = new NextOwinMiddlewareMock();
            var configuration = new ConfigurationMock();

            var sut = new ReverseProxyMiddleware(next.Object, configuration.Configuration);
            sut.Invoke(context).Wait();

            next.AssertPassedThrough(context);
            configuration.LockManagerMock.AssertLock();
            configuration.LockManagerMock.AssertUnlock();
        }

        [TestMethod]
        public void RequestThrowingErrorWhenPassingThroughShouldUnlockAnyway()
        {
            var context = new OwinContextMock();

            var next = new NextOwinMiddlewareMock();
            next.Setup(n => n.Invoke(It.IsAny<IOwinContext>())).Throws(new TimeoutException());

            var configuration = new ConfigurationMock();

            var sut = new ReverseProxyMiddleware(next.Object, configuration.Configuration);
            sut.Invoke(context).AssertIsFaultedWith<TimeoutException>();

            configuration.LockManagerMock.AssertLock();
            configuration.LockManagerMock.AssertUnlock();
        }
    }
}

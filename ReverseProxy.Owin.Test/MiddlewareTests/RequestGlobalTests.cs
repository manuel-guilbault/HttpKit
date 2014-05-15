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
    public class RequestGlobalTests
    {
        [TestMethod]
        public void UnsafeMethodShouldPassThrough()
        {
            var context = new OwinContextMock();
            context.Request.Method = "POST";

            var next = new NextOwinMiddlewareMock();
            var configuration = new ConfigurationMock();

            var sut = new ReverseProxyMiddleware(next.Object, configuration.Configuration);
            sut.Invoke(context).Wait();

            next.AssertPassedThrough(context);
            configuration.CacheMock.AssertNotSet(It.IsAny<ICacheKey>());
        }
    }
}

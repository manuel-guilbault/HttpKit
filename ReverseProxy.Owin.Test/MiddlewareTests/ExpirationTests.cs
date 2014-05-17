using HttpKit.Caching;
using HttpKit.Katana;
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
    public class ExpirationTests
    {
        [TestMethod]
        public void RequestWithNoCacheDirectiveShouldPassThrough()
        {
            var context = new OwinContextMock();
            context.Request.SetCacheControl(new RequestCacheControl(RequestCacheDirective.NoCache));

            var next = new NextOwinMiddlewareMock();
            var configuration = new ConfigurationMock();

            var sut = new ReverseProxyMiddleware(next.Object, configuration.Configuration);
            sut.Invoke(context).Wait();

            next.AssertPassedThrough(context);
            configuration.CacheMock.AssertNotSet(It.IsAny<ICacheKey>());
        }

        [TestMethod]
        public void RequestWithNoStoreDirectiveShouldPassThrough()
        {
            var context = new OwinContextMock();
            context.Request.SetCacheControl(new RequestCacheControl(RequestCacheDirective.NoStore));

            var next = new NextOwinMiddlewareMock();
            var configuration = new ConfigurationMock();

            var sut = new ReverseProxyMiddleware(next.Object, configuration.Configuration);
            sut.Invoke(context).Wait();

            next.AssertPassedThrough(context);
            configuration.CacheMock.AssertNotSet(It.IsAny<ICacheKey>());
        }

        [TestMethod]
        public void RequestWithUnexpiredMaxAgeDirectiveShouldUseCachedResponse()
        {
            var context = new OwinContextMock();
            context.Request.SetCacheControl(new RequestCacheControl(RequestCacheDirective.CreateMaxAge(TimeSpan.FromMinutes(10))));

            Time.SetFixedCurrentTime(new DateTime(2014, 5, 14, 12, 0, 0, DateTimeKind.Utc));

            var next = new NextOwinMiddlewareMock();

            var cachedContext = new OwinContextMock();
            cachedContext.Response.SetDate(new DateTime(2014, 5, 14, 11, 59, 0, DateTimeKind.Utc));

            var configuration = new ConfigurationMock();
            configuration.CacheMock.Setup(cache => cache.Get(It.IsAny<ICacheKey>()))
                .Returns(Task.FromResult<ICacheEntry>(new CacheEntry(cachedContext.Request, cachedContext.Response)));

            var sut = new ReverseProxyMiddleware(next.Object, configuration.Configuration);
            sut.Invoke(context).Wait();

            next.AssertNotPassedThrough();
        }

        [TestMethod]
        public void RequestWithExpiredMaxAgeDirectiveShouldPassThrough()
        {
            var context = new OwinContextMock();
            context.Request.SetCacheControl(new RequestCacheControl(RequestCacheDirective.CreateMaxAge(TimeSpan.FromMinutes(10))));

            Time.SetFixedCurrentTime(new DateTime(2014, 5, 14, 12, 0, 0, DateTimeKind.Utc));

            var next = new NextOwinMiddlewareMock();

            var cachedContext = new OwinContextMock();
            cachedContext.Response.SetDate(new DateTime(2014, 5, 14, 11, 0, 0, DateTimeKind.Utc));

            var configuration = new ConfigurationMock();
            configuration.CacheMock.Setup(cache => cache.Get(It.IsAny<ICacheKey>()))
                .Returns(Task.FromResult<ICacheEntry>(new CacheEntry(cachedContext.Request, cachedContext.Response)));

            var sut = new ReverseProxyMiddleware(next.Object, configuration.Configuration);
            sut.Invoke(context).Wait();

            next.AssertPassedThrough(context);
        }

        [TestMethod]
        public void RequestWithMatchedMinFreshDirectiveShouldUseCachedResponse()
        {
            var context = new OwinContextMock();
            context.Request.SetCacheControl(new RequestCacheControl(RequestCacheDirective.CreateMinFresh(TimeSpan.FromMinutes(3))));

            Time.SetFixedCurrentTime(new DateTime(2014, 5, 14, 12, 0, 0, DateTimeKind.Utc));

            var next = new NextOwinMiddlewareMock();

            var cachedContext = new OwinContextMock();
            cachedContext.Response.SetDate(Time.UtcNow.AddMinutes(-5));
            cachedContext.Response.SetCacheControl(new ResponseCacheControl(ResponseCacheDirective.CreateMaxAge(TimeSpan.FromMinutes(10))));

            var configuration = new ConfigurationMock();
            configuration.CacheMock.Setup(cache => cache.Get(It.IsAny<ICacheKey>()))
                .Returns(Task.FromResult<ICacheEntry>(new CacheEntry(cachedContext.Request, cachedContext.Response)));

            var sut = new ReverseProxyMiddleware(next.Object, configuration.Configuration);
            sut.Invoke(context).Wait();

            next.AssertNotPassedThrough();
        }

        [TestMethod]
        public void RequestWithUnmatchedMinFreshDirectiveShouldPassThrough()
        {
            var context = new OwinContextMock();
            context.Request.SetCacheControl(new RequestCacheControl(RequestCacheDirective.CreateMinFresh(TimeSpan.FromMinutes(8))));

            Time.SetFixedCurrentTime(new DateTime(2014, 5, 14, 12, 0, 0, DateTimeKind.Utc));

            var next = new NextOwinMiddlewareMock();

            var cachedContext = new OwinContextMock();
            cachedContext.Response.SetDate(Time.UtcNow.AddMinutes(-5));
            cachedContext.Response.SetCacheControl(new ResponseCacheControl(ResponseCacheDirective.CreateMaxAge(TimeSpan.FromMinutes(10))));

            var configuration = new ConfigurationMock();
            configuration.CacheMock.Setup(cache => cache.Get(It.IsAny<ICacheKey>()))
                .Returns(Task.FromResult<ICacheEntry>(new CacheEntry(cachedContext.Request, cachedContext.Response)));

            var sut = new ReverseProxyMiddleware(next.Object, configuration.Configuration);
            sut.Invoke(context).Wait();

            next.AssertPassedThrough(context);
        }

        [TestMethod]
        public void ResponseWithNoCacheDirectiveShouldNotBeCached()
        {
            var context = new OwinContextMock();

            var next = new NextOwinMiddlewareMock();
            context.Response.SetCacheControl(new ResponseCacheControl(ResponseCacheDirective.NoCache));

            var configuration = new ConfigurationMock();

            var sut = new ReverseProxyMiddleware(next.Object, configuration.Configuration);
            sut.Invoke(context).Wait();

            next.AssertPassedThrough(context);
            configuration.CacheMock.AssertNotSet(It.IsAny<ICacheKey>());
        }

        [TestMethod]
        public void ResponseWithNoStoreDirectiveShouldNotBeCached()
        {
            var context = new OwinContextMock();

            var next = new NextOwinMiddlewareMock();
            context.Response.SetCacheControl(new ResponseCacheControl(ResponseCacheDirective.NoStore));

            var configuration = new ConfigurationMock();

            var sut = new ReverseProxyMiddleware(next.Object, configuration.Configuration);
            sut.Invoke(context).Wait();

            next.AssertPassedThrough(context);
            configuration.CacheMock.AssertNotSet(It.IsAny<ICacheKey>());
        }

        [TestMethod]
        public void ResponseWithPrivateDirectiveShouldNotBeCached()
        {
            var context = new OwinContextMock();

            var next = new NextOwinMiddlewareMock();
            context.Response.SetCacheControl(new ResponseCacheControl(ResponseCacheDirective.Private));

            var configuration = new ConfigurationMock();

            var sut = new ReverseProxyMiddleware(next.Object, configuration.Configuration);
            sut.Invoke(context).Wait();

            next.AssertPassedThrough(context);
            configuration.CacheMock.AssertNotSet(It.IsAny<ICacheKey>());
        }

        [TestMethod]
        public void ResponseWithUnexpiredMaxAgeDirectiveShouldUseCachedResponse()
        {
            var context = new OwinContextMock();

            Time.SetFixedCurrentTime(new DateTime(2014, 5, 14, 12, 0, 0, DateTimeKind.Utc));

            var next = new NextOwinMiddlewareMock();

            var cachedContext = new OwinContextMock();
            cachedContext.Response.SetDate(new DateTime(2014, 5, 14, 11, 59, 0, DateTimeKind.Utc));
            cachedContext.Response.SetCacheControl(new ResponseCacheControl(ResponseCacheDirective.CreateMaxAge(TimeSpan.FromMinutes(10))));

            var configuration = new ConfigurationMock();
            configuration.CacheMock.Setup(cache => cache.Get(It.IsAny<ICacheKey>()))
                .Returns(Task.FromResult<ICacheEntry>(new CacheEntry(cachedContext.Request, cachedContext.Response)));

            var sut = new ReverseProxyMiddleware(next.Object, configuration.Configuration);
            sut.Invoke(context).Wait();

            next.AssertNotPassedThrough();
        }

        [TestMethod]
        public void ResponseWithExpiredMaxAgeDirectiveShouldPassThrough()
        {
            var context = new OwinContextMock();

            Time.SetFixedCurrentTime(new DateTime(2014, 5, 14, 12, 0, 0, DateTimeKind.Utc));

            var next = new NextOwinMiddlewareMock();

            var cachedContext = new OwinContextMock();
            cachedContext.Response.SetDate(new DateTime(2014, 5, 14, 11, 0, 0, DateTimeKind.Utc));
            cachedContext.Response.SetCacheControl(new ResponseCacheControl(ResponseCacheDirective.CreateMaxAge(TimeSpan.FromMinutes(10))));

            var configuration = new ConfigurationMock();
            configuration.CacheMock.Setup(cache => cache.Get(It.IsAny<ICacheKey>()))
                .Returns(Task.FromResult<ICacheEntry>(new CacheEntry(cachedContext.Request, cachedContext.Response)));

            var sut = new ReverseProxyMiddleware(next.Object, configuration.Configuration);
            sut.Invoke(context).Wait();

            next.AssertPassedThrough(context);
        }

        //[TestMethod]
        //public void RequestWithUnexpiredMaxStaleShouldUseCachedResponse()
        //{
        //    var context = new OwinContextMock();
        //    context.Request.SetCacheControl(new RequestCacheControl(RequestCacheDirective.CreateMaxStale(TimeSpan.FromMinutes(10))));

        //    Time.SetFixedCurrentTime(new DateTime(2014, 5, 14, 12, 0, 0, DateTimeKind.Utc));

        //    var next = new NextOwinMiddlewareMock();

        //    var cachedContext = new OwinContextMock();
        //    cachedContext.Response.SetDate(new DateTime(2014, 5, 14, 11, 59, 0, DateTimeKind.Utc));

        //    var configuration = new ConfigurationMock();
        //    configuration.CacheMock.Setup(cache => cache.Get(It.IsAny<ICacheKey>()))
        //        .Returns(Task.FromResult<ICacheEntry>(new CacheEntry(cachedContext.Request, cachedContext.Response)));

        //    var sut = new ReverseProxyMiddleware(next.Object, configuration.Configuration);
        //    sut.Invoke(context).Wait();

        //    next.AssertNotPassedThrough();
        //}

        //[TestMethod]
        //public void RequestWithValidMaxAgeButExpiredMaxStaleShouldPassThrough()
        //{

        //}
    }
}

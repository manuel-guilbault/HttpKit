using HttpKit.Caching;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpKit.Test.Caching
{
    [TestClass]
    public class RequestCacheDirectiveTest
    {
        [TestMethod]
        public void SimpleCacheDirectiveToString()
        {
            var sut = RequestCacheDirective.NoCache;

            var result = sut.ToString();

            Assert.AreEqual(result, RequestCacheDirective.NO_CACHE);
        }

        [TestMethod]
        public void OptionalDeltaTimeCacheDirectiveToStringWithoutDelta()
        {
            var sut = RequestCacheDirective.CreateMaxStale();

            var result = sut.ToString();

            Assert.AreEqual(result, RequestCacheDirective.MAX_STALE);
        }

        [TestMethod]
        public void OptionalDeltaTimeCacheDirectiveToStringWithDelta()
        {
            var sut = RequestCacheDirective.CreateMaxStale(TimeSpan.FromSeconds(60));

            var result = sut.ToString();

            Assert.AreEqual(result, RequestCacheDirective.MAX_STALE + "=60");
        }

        [TestMethod]
        public void DeltaTimeCacheDirectiveToString()
        {
            var sut = RequestCacheDirective.CreateMinFresh(TimeSpan.FromSeconds(30));

            var result = sut.ToString();

            Assert.AreEqual(result, RequestCacheDirective.MIN_FRESH + "=30");
        }

        [TestMethod]
        public void CacheDirectiveExtensionToStringWithoutValue()
        {
            var sut = RequestCacheDirective.CreateExtension("test");

            var result = sut.ToString();

            Assert.AreEqual(result, "test");
        }

        [TestMethod]
        public void CacheDirectiveExtensionToStringWithValue()
        {
            var sut = RequestCacheDirective.CreateExtension("test", "value");

            var result = sut.ToString();

            Assert.AreEqual(result, "test=value");
        }
    }
}

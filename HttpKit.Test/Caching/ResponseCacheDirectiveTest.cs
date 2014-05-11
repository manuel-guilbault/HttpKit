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
    public class ResponseCacheDirectiveTest
    {
        [TestMethod]
        public void SimpleCacheDirectiveToString()
        {
            var sut = ResponseCacheDirective.Public;

            var result = sut.ToString();

            Assert.AreEqual(result, ResponseCacheDirective.PUBLIC);
        }

        [TestMethod]
        public void DeltaTimeCacheDirectiveToString()
        {
            var sut = ResponseCacheDirective.CreateMaxAge(TimeSpan.FromSeconds(30));

            var result = sut.ToString();

            Assert.AreEqual(result, ResponseCacheDirective.MAX_AGE + "=30");
        }

        [TestMethod]
        public void FieldListCacheDirectiveToStringWithoutFields()
        {
            FieldListCacheDirectiveToString(
                ResponseCacheDirective.CreateNoCache(),
                ResponseCacheDirective.NO_CACHE
            );
        }

        [TestMethod]
        public void FieldListCacheDirectiveToStringWithOneField()
        {
            const string fieldName = "field";

            FieldListCacheDirectiveToString(
                ResponseCacheDirective.CreateNoCache(fieldName),
                string.Concat(ResponseCacheDirective.NO_CACHE, "=\"", fieldName, "\"")
            );
        }

        [TestMethod]
        public void FieldListCacheDirectiveToStringWithTwoFields()
        {
            const string field1Name = "field1";
            const string field2Name = "field2";

            FieldListCacheDirectiveToString(
                ResponseCacheDirective.CreateNoCache(field1Name, field2Name),
                string.Concat(ResponseCacheDirective.NO_CACHE, "=\"", field1Name, ",", field2Name, "\"")
            );
        }
        
        private void FieldListCacheDirectiveToString(FieldListResponseCacheDirective sut, string expectedToString)
        {
            var result = sut.ToString();

            Assert.AreEqual(result, expectedToString);
        }

        [TestMethod]
        public void CacheDirectiveExtensionToStringWithoutValue()
        {
            var sut = ResponseCacheDirective.CreateExtension("test");

            var result = sut.ToString();

            Assert.AreEqual(result, "test");
        }

        [TestMethod]
        public void CacheDirectiveExtensionToStringWithValue()
        {
            var sut = ResponseCacheDirective.CreateExtension("test", "value");

            var result = sut.ToString();

            Assert.AreEqual(result, "test=value");
        }
    }
}

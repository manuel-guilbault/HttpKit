using HttpKit.Caching;
using HttpKit.Ranges;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpKit.Test.Ranges
{
    [TestClass]
    public class IfRangeTest
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetEntityTagForLastModifiedTypedThrowsException()
        {
            var sut = new IfRange(DateTime.Now);

            var result = sut.EntityTag;
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetLastModifiedForEntityTagTypedThrowsException()
        {
            var sut = new IfRange(new EntityTag("test"));

            var result = sut.LastModified;
        }

        [TestMethod]
        public void TestGetLastModifiedForLastModifiedTyped()
        {
            var now = DateTime.Now;

            var sut = new IfRange(now);

            Assert.AreEqual(IfRangeType.LastModified, sut.Type);
            Assert.AreEqual(now, sut.LastModified);
        }

        [TestMethod]
        public void TestGetEntityTagForEntityTagTyped()
        {
            var etag = new EntityTag("test");

            var sut = new IfRange(etag);

            Assert.AreEqual(IfRangeType.EntityTag, sut.Type);
            Assert.AreEqual(etag, sut.EntityTag);
        }

        [TestMethod]
        public void TestToStringForLastModifiedType()
        {
            var lastModified = new DateTime(2014, 5, 11, 20, 45, 30, DateTimeKind.Utc);

            var sut = new IfRange(lastModified);

            Assert.AreEqual("Sun, 11 May 2014 20:45:30 GMT", sut.ToString());
        }

        [TestMethod]
        public void TestToStringForEntityTagType()
        {
            var etag = new EntityTag("test");

            var sut = new IfRange(etag);

            Assert.AreEqual(@"""test""", sut.ToString());
        }
    }
}

using HttpKit.Ranges;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpKit.Test.Ranges
{
    [TestClass]
    public class AcceptRangeTest
    {
        [TestMethod]
        public void TestNoRangeToString()
        {
            var sut = AcceptRange.None;

            var result = sut.ToString();

            Assert.AreEqual("none", result);
        }

        [TestMethod]
        public void TestRangesToString()
        {
            var sut = new AcceptRange(new RangeUnit("bytes"), new RangeUnit("kilobytes"));

            var result = sut.ToString();

            Assert.AreEqual("bytes,kilobytes", result);
        }
    }
}

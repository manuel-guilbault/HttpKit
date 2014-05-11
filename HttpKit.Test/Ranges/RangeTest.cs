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
    public class RangeTest
    {
        [TestMethod]
        public void TestToString()
        {
            var sut = new Range(new RangeUnit("bytes"), 
                SubRange.CreateOffsetFromStart(10), 
                SubRange.CreateClosedRange(40, 50),
                SubRange.CreateOffsetFromEnd(20)
            );

            Assert.AreEqual("bytes=10-,40-50,-20", sut.ToString());
        }
    }
}

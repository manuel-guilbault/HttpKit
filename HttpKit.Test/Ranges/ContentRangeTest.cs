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
    public class ContentRangeTest
    {
        [TestMethod]
        public void TestToString()
        {
            var sut = new ContentRange(new RangeUnit("bytes"), new ContentSubRange(1, 100), new InstanceLength(120));

            var result = sut.ToString();

            Assert.AreEqual("bytes 1-100/120", result);
        }
    }
}

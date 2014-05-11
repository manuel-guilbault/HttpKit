using HttpKit.Parsing;
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
    public class RangeParserTest
    {
        [TestMethod]
        public void TestParse()
        {
            var sut = new RangeParser();

            var result = sut.Parse(new Tokenizer("bytes=10-,30-50,-20"));

            var expected = new Range(new RangeUnit("bytes"),
                SubRange.CreateOffsetFromStart(10),
                SubRange.CreateClosedRange(30, 50),
                SubRange.CreateOffsetFromEnd(20)
            );
            Assert.AreEqual(expected, result);
        }
    }
}

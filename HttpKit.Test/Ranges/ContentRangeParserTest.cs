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
    public class ContentRangeParserTest
    {
        [TestMethod]
        public void TestKnownValues()
        {
            var sut = new ContentRangeParser();

            var result = sut.Parse(new Tokenizer("bytes 1-100/120"));

            Assert.AreEqual(new ContentRange(new RangeUnit("bytes"), new ContentSubRange(1, 100), new InstanceLength(120)), result);
        }

        [TestMethod]
        public void TestUnknownRangeWithKnownLength()
        {
            var sut = new ContentRangeParser();

            var result = sut.Parse(new Tokenizer("bytes */120"));

            Assert.AreEqual(new ContentRange(new RangeUnit("bytes"), ContentSubRange.Unknown, new InstanceLength(120)), result);
        }

        [TestMethod]
        public void TestKnownRangeWithUnknownLength()
        {
            var sut = new ContentRangeParser();

            var result = sut.Parse(new Tokenizer("bytes 1-100/*"));

            Assert.AreEqual(new ContentRange(new RangeUnit("bytes"), new ContentSubRange(1, 100), InstanceLength.Unknown), result);
        }

        [TestMethod]
        public void TestUnknownValues()
        {
            var sut = new ContentRangeParser();

            var result = sut.Parse(new Tokenizer("bytes */*"));

            Assert.AreEqual(new ContentRange(new RangeUnit("bytes"), ContentSubRange.Unknown, InstanceLength.Unknown), result);
        }
    }
}

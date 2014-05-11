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
    public class AcceptRangeParserTest
    {
        [TestMethod]
        public void ParseNone()
        {
            var sut = new AcceptRangeParser();

            var result = sut.Parse(new Tokenizer("none"));

            Assert.AreEqual(AcceptRange.None, result);
        }

        [TestMethod]
        public void ParseRangeUnits()
        {
            var sut = new AcceptRangeParser();

            var result = sut.Parse(new Tokenizer("bytes,kilobytes"));

            Assert.AreEqual(new AcceptRange(new RangeUnit("bytes"), new RangeUnit("kilobytes")), result);
        }
    }
}

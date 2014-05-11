using HttpKit.Caching;
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
    public class IfRangeParserTest
    {
        [TestMethod]
        public void TestParseEntityTag()
        {
            var sut = new IfRangeParser();

            var result = sut.Parse(new Tokenizer(@"""test"""));

            Assert.AreEqual(new IfRange(new EntityTag("test")), result);
        }

        [TestMethod]
        public void TestParseLastModified()
        {
            var sut = new IfRangeParser();

            var result = sut.Parse(new Tokenizer("Sun, 11 May 2014 20:45:30 GMT"));

            Assert.AreEqual(new IfRange(new DateTime(2014, 5, 11, 20, 45, 30, DateTimeKind.Utc)), result);
        }

        [TestMethod]
        [ExpectedException(typeof(ParsingException))]
        public void TestParseInvalid()
        {
            var sut = new IfRangeParser();

            sut.Parse(new Tokenizer("bah---"));
        }
    }
}

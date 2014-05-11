using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HttpKit.Caching;
using HttpKit.Parsing;

namespace HttpKit.Test.Caching
{
    [TestClass]
    public class EntityTagParserTest
    {
        [TestMethod]
        public void ParseStrongETag()
        {
            var sut = new EntityTagParser();

            var result = sut.Parse(new Tokenizer(@"""1234"""));

            Assert.AreEqual(result.IsWeak, false);
            Assert.AreEqual(result.Value, "1234");
        }

        [TestMethod]
        public void ParseWeakETag()
        {
            var sut = new EntityTagParser();

            var result = sut.Parse(new Tokenizer(@"W/""1234"""));

            Assert.AreEqual(result.IsWeak, true);
            Assert.AreEqual(result.Value, "1234");
        }

        [TestMethod]
        [ExpectedException(typeof(ParsingException))]
        public void ParseFails()
        {
            var sut = new EntityTagParser();

            sut.Parse(new Tokenizer(@"1234"""));
        }
    }
}

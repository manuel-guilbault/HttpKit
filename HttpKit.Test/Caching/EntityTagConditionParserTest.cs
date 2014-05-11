using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HttpKit.Caching;
using Moq;
using HttpKit.Parsing;

namespace HttpKit.Test.Caching
{
    [TestClass]
    public class EntityTagConditionParserTest
    {
        [TestMethod]
        public void ParseAny()
        {
            var sut = new EntityTagConditionParser();

            var result = sut.Parse(new Tokenizer("*"));

            Assert.AreEqual(result, EntityTagCondition.Any);
        }

        [TestMethod]
        public void ParseEtags()
        {
            var sut = new EntityTagConditionParser();

            var result = sut.Parse(new Tokenizer(@"""1234"",""4321"""));

            Assert.IsTrue(result.ValidTags.SequenceEqual(new[] { new EntityTag("1234"), new EntityTag("4321") }));
        }

        [TestMethod]
        [ExpectedException(typeof(ParsingException))]
        public void ParseFails()
        {
            var sut = new EntityTagConditionParser();

            sut.Parse(new Tokenizer(@"""1234""""4321"""));
        }
    }
}

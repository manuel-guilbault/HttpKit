using HttpKit.Caching;
using HttpKit.Parsing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpKit.Test.Parsing
{
    [TestClass]
    public class HeaderReaderTest
    {
        [TestMethod]
        public void Read()
        {
            var expectedResult = "test";

            var parser = new Mock<IHeaderParser<string>>();
            parser.Setup(p => p.Parse(It.IsAny<Tokenizer>())).Callback<Tokenizer>(t => t.Read("test")).Returns(expectedResult);

            var sut = new HeaderReader<string>(parser.Object);

            var result = sut.Read("test");

            Assert.AreEqual(result, expectedResult);
        }

        [TestMethod]
        [ExpectedException(typeof(ParsingException))]
        public void IncompleteReadFails()
        {
            var parser = new Mock<IHeaderParser<string>>();
            parser.Setup(p => p.Parse(It.IsAny<Tokenizer>())).Callback<Tokenizer>(t => t.Read("test"));

            var sut = new HeaderReader<string>(parser.Object);

            sut.Read("test failing");
        }
    }
}

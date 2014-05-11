using HttpKit.Parsing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpKit.Test.Parsing
{
    [TestClass]
    public class TokenizerTest
    {
        [TestMethod]
        public void TestMove()
        {
            var sut = new Tokenizer("12345");

            sut.Move(2);

            Assert.AreEqual(2, sut.Position);

            sut.Move(2);

            Assert.AreEqual(4, sut.Position);
        }

        [TestMethod]
        public void TestToString()
        {
            var sut = new Tokenizer("12345");
            sut.Move(2);

            Assert.AreEqual("345", sut.ToString());
        }
    }
}

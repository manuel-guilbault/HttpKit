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
    public class TokenizerExtensionsTest
    {
        [TestMethod]
        public void IsAtEndTrue()
        {
            var sut = new Tokenizer("12345");

            sut.Move(3);

            Assert.IsTrue(sut.IsAtEnd(3));
        }

        [TestMethod]
        public void IsAtEndFalse()
        {
            var sut = new Tokenizer("12345");

            sut.Move(2);

            Assert.IsFalse(sut.IsAtEnd(2));
        }

        [TestMethod]
        public void PeekChar()
        {
            var sut = new Tokenizer("12345");

            var result = sut.PeekChar(2);

            Assert.AreEqual(result, '3');
            Assert.AreEqual(0, sut.Position);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PeekCharNegativeOffset()
        {
            var sut = new Tokenizer("12345");

            sut.PeekChar(-1);
        }

        [TestMethod]
        [ExpectedException(typeof(ParsingException))]
        public void PeekCharOutOfBounds()
        {
            var sut = new Tokenizer("12345");

            sut.PeekChar(5);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PeekNegativeOffset()
        {
            var sut = new Tokenizer("12345");

            sut.Peek(-1, 2);
        }

        [TestMethod]
        public void PeekInRange()
        {
            var sut = new Tokenizer("12345");

            var result = sut.Peek(2, 2);

            Assert.AreEqual("34", result);
            Assert.AreEqual(0, sut.Position);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void PeekOffsetOutOfRange()
        {
            var sut = new Tokenizer("12345");

            sut.Peek(6, 2);
        }

        [TestMethod]
        public void PeekLengthOutOfRange()
        {
            var sut = new Tokenizer("12345");

            var result = sut.Peek(3, 4);

            Assert.AreEqual("45", result);
            Assert.AreEqual(0, sut.Position);
        }
    }
}

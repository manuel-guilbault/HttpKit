using HttpKit.Caching;
using HttpKit.Parsing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpKit.Test.Caching
{
    [TestClass]
    public class ResponseCacheDirectiveParsersTest
    {
        [TestMethod]
        public void CanParseForSimpleCacheDirective()
        {
            var sut = new ResponseCacheDirectiveParser(ResponseCacheDirective.Public);

            var tokenizer = new Tokenizer(ResponseCacheDirective.PUBLIC);
            var result = sut.CanParse(tokenizer);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CannotParseForSimpleCacheDirective()
        {
            var sut = new ResponseCacheDirectiveParser(ResponseCacheDirective.Public);

            var tokenizer = new Tokenizer(ResponseCacheDirective.MUST_REVALIDATE);
            var result = sut.CanParse(tokenizer);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ParseSimpleCacheDirective()
        {
            var sut = new ResponseCacheDirectiveParser(ResponseCacheDirective.Public);

            var tokenizer = new Tokenizer(ResponseCacheDirective.PUBLIC);
            var result = sut.Parse(tokenizer);

            Assert.AreEqual(result, ResponseCacheDirective.Public);
        }

        [TestMethod]
        [ExpectedException(typeof(ParsingException))]
        public void ParseFailsForSimpleCacheDirective()
        {
            var sut = new ResponseCacheDirectiveParser(ResponseCacheDirective.Public);

            var tokenizer = new Tokenizer(ResponseCacheDirective.MUST_REVALIDATE);
            sut.Parse(tokenizer);
        }

        [TestMethod]
        public void CanParseForDeltaTimeCacheDirective()
        {
            var sut = new DeltaTimeResponseCacheDirectiveParser(ResponseCacheDirective.MAX_AGE, ResponseCacheDirective.CreateMaxAge);

            var tokenizer = new Tokenizer(ResponseCacheDirective.MAX_AGE);
            var result = sut.CanParse(tokenizer);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CannotParseForDeltaTimeCacheDirective()
        {
            var sut = new DeltaTimeResponseCacheDirectiveParser(ResponseCacheDirective.MAX_AGE, ResponseCacheDirective.CreateMaxAge);

            var tokenizer = new Tokenizer(ResponseCacheDirective.MUST_REVALIDATE);
            var result = sut.CanParse(tokenizer);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ParseDeltaTimeCacheDirective()
        {
            var sut = new DeltaTimeResponseCacheDirectiveParser(ResponseCacheDirective.MAX_AGE, ResponseCacheDirective.CreateMaxAge);

            var tokenizer = new Tokenizer(string.Concat(ResponseCacheDirective.MAX_AGE, "=60"));
            var result = sut.Parse(tokenizer);

            Assert.IsInstanceOfType(result, typeof(DeltaTimeResponseCacheDirective));

            var deltaTimeCacheDirective = (DeltaTimeResponseCacheDirective)result;
            Assert.AreEqual(deltaTimeCacheDirective.Name, ResponseCacheDirective.MAX_AGE);
            Assert.AreEqual(deltaTimeCacheDirective.Delta, TimeSpan.FromSeconds(60));
        }

        [TestMethod]
        [ExpectedException(typeof(ParsingException))]
        public void ParseFailsForDeltaTimeCacheDirectiveWithNameOnly()
        {
            ParseFailsForDeltaTimeCacheDirective(ResponseCacheDirective.MAX_AGE);
        }

        [TestMethod]
        [ExpectedException(typeof(ParsingException))]
        public void ParseFailsForDeltaTimeCacheDirectiveWithInvalidName()
        {
            ParseFailsForDeltaTimeCacheDirective(ResponseCacheDirective.PUBLIC);
        }

        [TestMethod]
        [ExpectedException(typeof(ParsingException))]
        public void ParseFailsForDeltaTimeCacheDirectiveWithInvalidDelta()
        {
            ParseFailsForDeltaTimeCacheDirective(string.Concat(ResponseCacheDirective.MAX_AGE, "=not-an-int"));
        }

        private void ParseFailsForDeltaTimeCacheDirective(string value)
        {
            var sut = new DeltaTimeResponseCacheDirectiveParser(ResponseCacheDirective.MAX_AGE, ResponseCacheDirective.CreateMaxAge);

            var tokenizer = new Tokenizer(value);
            sut.Parse(tokenizer);
        }

        [TestMethod]
        public void CanParseForFieldListCacheDirective()
        {
            var sut = new FieldListResponseCacheDirectiveParser(ResponseCacheDirective.NO_CACHE, ResponseCacheDirective.CreateNoCache);

            var tokenizer = new Tokenizer(ResponseCacheDirective.NO_CACHE);
            var result = sut.CanParse(tokenizer);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CannotParseForFieldListCacheDirective()
        {
            var sut = new FieldListResponseCacheDirectiveParser(ResponseCacheDirective.NO_CACHE, ResponseCacheDirective.CreateNoCache);

            var tokenizer = new Tokenizer(ResponseCacheDirective.MUST_REVALIDATE);
            var result = sut.CanParse(tokenizer);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ParseForFieldListCacheDirectiveWithoutFields()
        {
            ParseForFieldListCacheDirective("", new string[0]);
        }

        [TestMethod]
        public void ParseForFieldListCacheDirectiveWithOneField()
        {
            const string fieldName = "field";

            ParseForFieldListCacheDirective("=\"" + fieldName + "\"", new[] { fieldName });
        }

        [TestMethod]
        public void ParseForFieldListCacheDirectiveWithTwoFields()
        {
            const string field1Name = "field1";
            const string field2Name = "field2";

            ParseForFieldListCacheDirective("=\"" + field1Name + "," + field2Name + "\"", new[] { field1Name, field2Name });
        }

        private void ParseForFieldListCacheDirective(string fieldsToParse, params string[] expectedFields)
        {
            var sut = new FieldListResponseCacheDirectiveParser(ResponseCacheDirective.NO_CACHE, ResponseCacheDirective.CreateNoCache);

            var tokenizer = new Tokenizer(ResponseCacheDirective.NO_CACHE + fieldsToParse);
            var result = sut.Parse(tokenizer);

            Assert.IsInstanceOfType(result, typeof(FieldListResponseCacheDirective));

            var fieldListCacheDirective = (FieldListResponseCacheDirective)result;
            Assert.IsTrue(
                expectedFields.SequenceEqual(fieldListCacheDirective.Fields), 
                string.Format(
                    "Field list is not matching. Expected: <{0}>. Actual result: <{1}>.", 
                    string.Join(",", expectedFields),
                    string.Join(",", fieldListCacheDirective.Fields)
                )
            );
        }

        [TestMethod]
        [ExpectedException(typeof(ParsingException))]
        public void ParseFailsForFieldListCacheDirectiveWithTrailingToken()
        {
            ParseFailsForFieldListCacheDirective(ResponseCacheDirective.NO_CACHE + "=\"toto,tata test\"");
        }

        private void ParseFailsForFieldListCacheDirective(string value)
        {
            var sut = new FieldListResponseCacheDirectiveParser(ResponseCacheDirective.NO_CACHE, ResponseCacheDirective.CreateNoCache);

            var tokenizer = new Tokenizer(value);
            sut.Parse(tokenizer);
        }

        [TestMethod]
        public void CanParseForCacheDirectiveExtension()
        {
            var sut = new ResponseCacheDirectiveExtensionParser();

            var tokenizer = new Tokenizer("an-extension");
            var result = sut.CanParse(tokenizer);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ParseCacheDirectiveExtensionWithoutValue()
        {
            const string name = "test-extension";

            ParseCacheDirectiveExtension(name, name, null);
        }

        [TestMethod]
        public void ParseCacheDirectiveExtensionWithValue()
        {
            const string name = "test-extension";
            const string value = "a-test-value";

            ParseCacheDirectiveExtension(name + "=" + value, name, value);
        }

        private void ParseCacheDirectiveExtension(string headerValue, string expectedName, string expectedValue)
        {
            var sut = new ResponseCacheDirectiveExtensionParser();

            var tokenizer = new Tokenizer(headerValue);
            var result = sut.Parse(tokenizer);

            Assert.IsInstanceOfType(result, typeof(ResponseCacheDirectiveExtension));

            var cacheDirectiveExtension = (ResponseCacheDirectiveExtension)result;
            Assert.AreEqual(cacheDirectiveExtension.Name, expectedName);
            Assert.AreEqual(cacheDirectiveExtension.Value, expectedValue);
        }
    }
}

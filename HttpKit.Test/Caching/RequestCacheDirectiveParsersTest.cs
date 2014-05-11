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
    public class RequestCacheDirectiveParsersTest
    {
        [TestMethod]
        public void CanParseForSimpleCacheDirective()
        {
            var sut = new RequestCacheDirectiveParser(RequestCacheDirective.NoCache);

            var tokenizer = new Tokenizer(RequestCacheDirective.NO_CACHE);
            var result = sut.CanParse(tokenizer);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CannotParseForSimpleCacheDirective()
        {
            var sut = new RequestCacheDirectiveParser(RequestCacheDirective.NoCache);

            var tokenizer = new Tokenizer(RequestCacheDirective.NO_STORE);
            var result = sut.CanParse(tokenizer);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ParseSimpleCacheDirective()
        {
            var sut = new RequestCacheDirectiveParser(RequestCacheDirective.NoCache);

            var tokenizer = new Tokenizer(RequestCacheDirective.NO_CACHE);
            var result = sut.Parse(tokenizer);

            Assert.AreEqual(result, RequestCacheDirective.NoCache);
        }

        [TestMethod]
        [ExpectedException(typeof(ParsingException))]
        public void ParseFailsForSimpleCacheDirective()
        {
            var sut = new RequestCacheDirectiveParser(RequestCacheDirective.NoCache);

            var tokenizer = new Tokenizer(RequestCacheDirective.NO_STORE);
            sut.Parse(tokenizer);
        }

        [TestMethod]
        public void CanParseForDeltaTimeCacheDirective()
        {
            var sut = new DeltaTimeRequestCacheDirectiveParser(RequestCacheDirective.MAX_AGE, RequestCacheDirective.CreateMaxAge);

            var tokenizer = new Tokenizer(RequestCacheDirective.MAX_AGE);
            var result = sut.CanParse(tokenizer);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CannotParseForDeltaTimeCacheDirective()
        {
            var sut = new DeltaTimeRequestCacheDirectiveParser(RequestCacheDirective.MAX_AGE, RequestCacheDirective.CreateMaxAge);

            var tokenizer = new Tokenizer(RequestCacheDirective.MAX_STALE);
            var result = sut.CanParse(tokenizer);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ParseDeltaTimeCacheDirective()
        {
            var sut = new DeltaTimeRequestCacheDirectiveParser(RequestCacheDirective.MAX_AGE, RequestCacheDirective.CreateMaxAge);

            var tokenizer = new Tokenizer(string.Concat(RequestCacheDirective.MAX_AGE, "=60"));
            var result = sut.Parse(tokenizer);

            Assert.IsInstanceOfType(result, typeof(DeltaTimeRequestCacheDirective));

            var deltaTimeCacheDirective = (DeltaTimeRequestCacheDirective)result;
            Assert.AreEqual(deltaTimeCacheDirective.Name, RequestCacheDirective.MAX_AGE);
            Assert.AreEqual(deltaTimeCacheDirective.Delta, TimeSpan.FromSeconds(60));
        }

        [TestMethod]
        [ExpectedException(typeof(ParsingException))]
        public void ParseFailsForDeltaTimeCacheDirectiveWithNameOnly()
        {
            ParseFailsForDeltaTimeCacheDirective(RequestCacheDirective.MAX_AGE);
        }

        [TestMethod]
        [ExpectedException(typeof(ParsingException))]
        public void ParseFailsForDeltaTimeCacheDirectiveWithInvalidName()
        {
            ParseFailsForDeltaTimeCacheDirective(RequestCacheDirective.NO_CACHE);
        }

        [TestMethod]
        [ExpectedException(typeof(ParsingException))]
        public void ParseFailsForDeltaTimeCacheDirectiveWithInvalidDelta()
        {
            ParseFailsForDeltaTimeCacheDirective(string.Concat(RequestCacheDirective.MAX_AGE, "=not-an-int"));
        }

        private void ParseFailsForDeltaTimeCacheDirective(string value)
        {
            var sut = new DeltaTimeRequestCacheDirectiveParser(RequestCacheDirective.MAX_AGE, RequestCacheDirective.CreateMaxAge);

            var tokenizer = new Tokenizer(value);
            sut.Parse(tokenizer);
        }

        [TestMethod]
        public void CanParseForOptionalDeltaTimeCacheDirectiveWithoutDelta()
        {
            CanParseForOptionalDeltaTimeCacheDirective(RequestCacheDirective.MAX_STALE);
        }

        [TestMethod]
        public void CanParseForOptionalDeltaTimeCacheDirectiveWithDelta()
        {
            CanParseForOptionalDeltaTimeCacheDirective(string.Concat(RequestCacheDirective.MAX_STALE, "=60"));
        }

        private void CanParseForOptionalDeltaTimeCacheDirective(string value)
        {
            var sut = new OptionalDeltaTimeRequestCacheDirectiveParser(RequestCacheDirective.MAX_STALE, RequestCacheDirective.CreateMaxStale);

            var tokenizer = new Tokenizer(value);
            var result = sut.CanParse(tokenizer);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CannotParseForOptionalDeltaTimeCacheDirective()
        {
            var sut = new OptionalDeltaTimeRequestCacheDirectiveParser(RequestCacheDirective.MAX_STALE, RequestCacheDirective.CreateMaxStale);

            var tokenizer = new Tokenizer(RequestCacheDirective.NO_CACHE);
            var result = sut.CanParse(tokenizer);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ParseOptionalDeltaTimeCacheDirectiveWithoutDelta()
        {
            ParseOptionalDeltaTimeCacheDirective(RequestCacheDirective.MAX_STALE, null);
        }

        [TestMethod]
        public void ParseOptionalDeltaTimeCacheDirectiveWithDelta()
        {
            ParseOptionalDeltaTimeCacheDirective(
                string.Concat(RequestCacheDirective.MAX_STALE, "=60"),
                TimeSpan.FromSeconds(60)
            );
        }
        
        private void ParseOptionalDeltaTimeCacheDirective(string value, TimeSpan? expectedDelta)
        {
            var sut = new OptionalDeltaTimeRequestCacheDirectiveParser(RequestCacheDirective.MAX_STALE, RequestCacheDirective.CreateMaxStale);

            var tokenizer = new Tokenizer(value);
            var result = sut.Parse(tokenizer);

            Assert.IsInstanceOfType(result, typeof(OptionalDeltaTimeRequestCacheDirective));

            var deltaTimeCacheDirective = (OptionalDeltaTimeRequestCacheDirective)result;
            Assert.AreEqual(deltaTimeCacheDirective.Name, RequestCacheDirective.MAX_STALE);
            Assert.AreEqual(deltaTimeCacheDirective.Delta, expectedDelta);
        }

        [TestMethod]
        [ExpectedException(typeof(ParsingException))]
        public void ParseFailsForOptionalDeltaTimeCacheDirectiveWithNoDelta()
        {
            ParseFailsForOptionalDeltaTimeCacheDirective(RequestCacheDirective.MAX_STALE + "=");
        }

        [TestMethod]
        [ExpectedException(typeof(ParsingException))]
        public void ParseFailsForOptionalDeltaTimeCacheDirectiveWithInvalidDelta()
        {
            ParseFailsForOptionalDeltaTimeCacheDirective(RequestCacheDirective.MAX_STALE + "=not-an-int");
        }

        private void ParseFailsForOptionalDeltaTimeCacheDirective(string value)
        {
            var sut = new OptionalDeltaTimeRequestCacheDirectiveParser(RequestCacheDirective.MAX_STALE, RequestCacheDirective.CreateMaxStale);

            var tokenizer = new Tokenizer(value);
            sut.Parse(tokenizer);
        }

        [TestMethod]
        public void CanParseForCacheDirectiveExtension()
        {
            var sut = new RequestCacheDirectiveExtensionParser();

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
            var sut = new RequestCacheDirectiveExtensionParser();

            var tokenizer = new Tokenizer(headerValue);
            var result = sut.Parse(tokenizer);

            Assert.IsInstanceOfType(result, typeof(RequestCacheDirectiveExtension));

            var cacheDirectiveExtension = (RequestCacheDirectiveExtension)result;
            Assert.AreEqual(cacheDirectiveExtension.Name, expectedName);
            Assert.AreEqual(cacheDirectiveExtension.Value, expectedValue);
        }
    }
}

using HttpKit.Caching;
using HttpKit.Parsing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpKit.Test.Caching
{
    [TestClass]
    public class RequestCacheControlParserTest
    {
        [TestMethod]
        [ExpectedException(typeof(ParsingException))]
        public void ParseNoneFails()
        {
            var sut = new RequestCacheControlParser();

            var tokenizer = new Tokenizer("");
            sut.Parse(tokenizer);
        }

        [TestMethod]
        public void ParseOne()
        {
            var sut = new RequestCacheControlParser()
            {
                DirectiveParsers = new IRequestCacheDirectiveParser[]
                { 
                    new RequestCacheDirectiveParser(RequestCacheDirective.NoCache)
                }
            };

            var tokenizer = new Tokenizer(RequestCacheDirective.NO_CACHE);
            var result = sut.Parse(tokenizer);

            Assert.IsTrue(result.Has(RequestCacheDirective.NoCache));
        }

        [TestMethod]
        public void ParseTwo()
        {
            var sut = new RequestCacheControlParser();

            var tokenizer = new Tokenizer(string.Concat(
                RequestCacheDirective.NO_CACHE,
                ", ",
                RequestCacheDirective.MAX_AGE, "=60"
            ));
            var result = sut.Parse(tokenizer);

            Assert.IsTrue(result.Has(RequestCacheDirective.NoCache));
            Assert.IsTrue(result.Has(RequestCacheDirective.MAX_AGE));
        }
    }
}

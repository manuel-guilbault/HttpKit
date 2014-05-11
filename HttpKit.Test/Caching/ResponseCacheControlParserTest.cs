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
    public class ResponseCacheControlParserTest
    {
        [TestMethod]
        [ExpectedException(typeof(ParsingException))]
        public void ParseNoneFails()
        {
            var sut = new ResponseCacheControlParser();

            var tokenizer = new Tokenizer("");
            sut.Parse(tokenizer);
        }

        [TestMethod]
        public void ParseOne()
        {
            var sut = new ResponseCacheControlParser()
            {
                DirectiveParsers = new IResponseCacheDirectiveParser[]
                { 
                    new ResponseCacheDirectiveParser(ResponseCacheDirective.Public)
                }
            };

            var tokenizer = new Tokenizer(ResponseCacheDirective.PUBLIC);
            var result = sut.Parse(tokenizer);

            Assert.IsTrue(result.Has(ResponseCacheDirective.Public));
        }

        [TestMethod]
        public void ParseTwo()
        {
            var sut = new ResponseCacheControlParser();

            var tokenizer = new Tokenizer(string.Concat(
                ResponseCacheDirective.PUBLIC,
                ", ",
                ResponseCacheDirective.NO_CACHE, "=\"field\""
            ));
            var result = sut.Parse(tokenizer);

            Assert.IsTrue(result.Has(ResponseCacheDirective.Public));
            Assert.IsTrue(result.Has(ResponseCacheDirective.NO_CACHE));
        }
    }
}

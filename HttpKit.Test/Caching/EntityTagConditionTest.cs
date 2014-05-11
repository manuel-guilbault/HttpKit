using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HttpKit.Caching;

namespace HttpKit.Test.Caching
{
    [TestClass]
    public class EntityTagConditionTest
    {
        [TestMethod]
        public void AnyConditionIsValidWithAnyETag()
        {
            var sut = EntityTagCondition.Any;

            var result = sut.IsValid(new EntityTag("1234"));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void MatchingETagIsValid()
        {
            var sut = new EntityTagCondition(new EntityTag("1234"));

            var result = sut.IsValid(new EntityTag("1234"));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void NotMatchingETagIsInvalid()
        {
            var sut = new EntityTagCondition(new EntityTag("1234"));

            var result = sut.IsValid(new EntityTag("4321"));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void AnyToString()
        {
            var sut = EntityTagCondition.Any;

            var result = sut.ToString();

            Assert.AreEqual(result, @"*");
        }

        [TestMethod]
        public void TwoETagsToString()
        {
            var sut = new EntityTagCondition(new EntityTag("1234"), new EntityTag("4321"));

            var result = sut.ToString();

            Assert.AreEqual(result, @"""1234"",""4321""");
        }
    }
}

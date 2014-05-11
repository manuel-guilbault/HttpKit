using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HttpKit.Caching;

namespace HttpKit.Test.Caching
{
    [TestClass]
    public class EntityTagTest
    {
        [TestMethod]
        public void StrongEqualityForTwoIdenticalStrongTags()
        {
            var tag1 = new EntityTag(false, "1234");
            var tag2 = new EntityTag(false, "1234");

            Assert.IsTrue(tag1.Equals(tag2, EntityTagComparisonType.Strong));
        }

        [TestMethod]
        public void StrongInequalityForTwoDifferentStrongTags()
        {
            var tag1 = new EntityTag(false, "1234");
            var tag2 = new EntityTag(false, "4321");

            Assert.IsFalse(tag1.Equals(tag2, EntityTagComparisonType.Strong));
        }

        [TestMethod]
        public void StrongInequalityForTwoIdenticalWeakTags()
        {
            var tag1 = new EntityTag(true, "1234");
            var tag2 = new EntityTag(true, "1234");

            Assert.IsFalse(tag1.Equals(tag2, EntityTagComparisonType.Strong));
        }

        [TestMethod]
        public void StrongInequalityForTwoDifferentWeakTags()
        {
            var tag1 = new EntityTag(true, "1234");
            var tag2 = new EntityTag(true, "4321");

            Assert.IsFalse(tag1.Equals(tag2, EntityTagComparisonType.Strong));
        }

        [TestMethod]
        public void StrongInequalityForTwoDifferentTags()
        {
            var tag1 = new EntityTag(true, "1234");
            var tag2 = new EntityTag(false, "4321");

            Assert.IsFalse(tag1.Equals(tag2, EntityTagComparisonType.Strong));
        }

        [TestMethod]
        public void WeakEqualityForTwoIdenticalTagsWithDifferentWeakness()
        {
            var tag1 = new EntityTag(true, "1234");
            var tag2 = new EntityTag(false, "1234");

            Assert.IsTrue(tag1.Equals(tag2, EntityTagComparisonType.Weak));
        }

        [TestMethod]
        public void WeakInequalityForTwoDifferentTags()
        {
            var tag1 = new EntityTag(true, "1234");
            var tag2 = new EntityTag(true, "4321");

            Assert.IsFalse(tag1.Equals(tag2, EntityTagComparisonType.Weak));
        }

        [TestMethod]
        public void StrongToString()
        {
            var tag = new EntityTag(false, "1234");

            Assert.AreEqual(tag.ToString(), @"""1234""");
        }

        [TestMethod]
        public void WeakToString()
        {
            var tag = new EntityTag(true, "1234");

            Assert.AreEqual(tag.ToString(), @"W/""1234""");
        }
    }
}

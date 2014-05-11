using HttpKit.Caching;
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
    public class EntityTagEqualityComparerTest
    {
        [TestMethod]
        public void TestEqualsWithStrongComparison()
        {
            var comparisonType = EntityTagComparisonType.Strong;

            var etag1 = new Mock<IEntityTag>();
            var etag2 = new Mock<IEntityTag>();

            etag1.Setup(et => et.Equals(etag2.Object, comparisonType)).Returns(true);

            var sut = new EntityTagEqualityComparer(comparisonType);

            var result = sut.Equals(etag1.Object, etag2.Object);

            Assert.AreEqual(result, true);
        }

        [TestMethod]
        public void TestEqualsWithWeakComparison()
        {
            var comparisonType = EntityTagComparisonType.Weak;

            var etag1 = new Mock<IEntityTag>();
            var etag2 = new Mock<IEntityTag>();

            etag1.Setup(et => et.Equals(etag2.Object, comparisonType)).Returns(true);

            var sut = new EntityTagEqualityComparer(comparisonType);

            var result = sut.Equals(etag1.Object, etag2.Object);

            Assert.AreEqual(result, true);
        }

        [TestMethod]
        public void TestNotEqualsWithNull()
        {
            var comparisonType = EntityTagComparisonType.Weak;

            IEntityTag etag1 = null;
            var etag2 = new Mock<IEntityTag>();

            var sut = new EntityTagEqualityComparer(comparisonType);

            var result = sut.Equals(etag1, etag2.Object);

            Assert.AreEqual(result, false);
        }

        [TestMethod]
        public void TestGetHashCode()
        {
            var entityTag = new EntityTag("test");

            var sut = new EntityTagEqualityComparer();

            var result = sut.GetHashCode(entityTag);

            Assert.AreEqual(result, entityTag.GetHashCode());
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpKit.Test
{
    [TestClass]
    public class DateTimeExtensionsTest
    {
        [TestMethod]
        public void AsHttpDateTimeConvertsLocalToUtc()
        {
            var date = new DateTime(2014, 5, 15, 12, 0, 0, DateTimeKind.Local);

            var result = date.AsHttpDateTime();

            var expected = TimeZoneInfo.ConvertTimeToUtc(date).ToString("r", CultureInfo.InvariantCulture);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void AsHttpDateTimeUtcNoConverts()
        {
            var date = new DateTime(2014, 5, 15, 12, 0, 0, DateTimeKind.Utc);

            var result = date.AsHttpDateTime();

            var expected = date.ToString("r", CultureInfo.InvariantCulture);
            Assert.AreEqual(expected, result);
        }
    }
}

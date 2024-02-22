using Microsoft.VisualStudio.TestTools.UnitTesting;
using MPFastDevLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPFastDevLibrary.Common.Tests
{
    [TestClass()]
    public class DateTimeHelperTests
    {
        [TestMethod()]
        public void MinuteToHHmmFormatTest()
        {
            var str1 = DateTimeHelper.MinuteToFormatHHmm(-5);
            Assert.AreEqual("00:00", str1);
            var str2 = DateTimeHelper.MinuteToFormatHHmm(5);
            Assert.AreEqual("00:05", str2);
            var str3 = DateTimeHelper.MinuteToFormatHHmm(65);
            Assert.AreEqual("01:05", str3);
            var str4 = DateTimeHelper.MinuteToFormatHHmm(655);
            Assert.AreEqual("10:55", str4);
            var str5 = DateTimeHelper.MinuteToFormatHHmm(6055);
            Assert.AreEqual("100:55", str5);
        }

        [TestMethod()]
        public void SecendToFormatHmsTest()
        {
            var str1 = DateTimeHelper.SecendToFormatHms(-5);
            Assert.AreEqual("00:00:00", str1);
            var str2 = DateTimeHelper.SecendToFormatHms(5);
            Assert.AreEqual("00:00:05", str2);
            var str3 = DateTimeHelper.SecendToFormatHms(65);
            Assert.AreEqual("00:01:05", str3);
            var str4 = DateTimeHelper.SecendToFormatHms(655);
            Assert.AreEqual("00:10:55", str4);
            var str5 = DateTimeHelper.SecendToFormatHms(3605);
            Assert.AreEqual("01:00:05", str5);
            var str6 = DateTimeHelper.SecendToFormatHms(4205);
            Assert.AreEqual("01:10:05", str6);
            var str7 = DateTimeHelper.SecendToFormatHms(36000);
            Assert.AreEqual("10:00:00", str7);
        }
    }
}

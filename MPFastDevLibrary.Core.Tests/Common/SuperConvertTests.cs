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
    public class SuperConvertTests
    {
        [TestMethod()]
        public void ConvertToStringTest()
        {

        }

        [TestMethod()]
        public void ConvertToStringTest1()
        {

        }

        [TestMethod()]
        public void ByteArrayToStringHexTest()
        {

        }

        [TestMethod()]
        public void HexStringToBtyeArrayTest()
        {

        }

        [TestMethod()]
        public void DateTimeToTimeStampTest()
        {
            var dt = DateTime.Parse("2022/9/15 22:02:54");
            var ts = SuperConvert.DateTimeToTimeStamp(dt);
            long res = 1663250574000;
            Assert.AreEqual(res, ts);
        }

        [TestMethod()]
        public void TimeStampToDateTimeTest()
        {
            long res = 1663250574000;
            var dt = DateTime.Parse("2022/9/15 22:02:54");
            var ts = SuperConvert.TimeStampToDateTime(res);
            Assert.AreEqual(ts.ToString(), "2022/9/15 22:02:54");
        }
    }
}
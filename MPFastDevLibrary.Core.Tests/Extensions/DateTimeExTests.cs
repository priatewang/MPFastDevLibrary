using Microsoft.VisualStudio.TestTools.UnitTesting;
using MPFastDevLibrary.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPFastDevLibrary.Extensions.Tests
{
    [TestClass()]
    public class DateTimeExTests
    {
        [TestMethod()]
        public void ToWeekDay_CHTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ToWeekNumberTest()
        {
            Assert.Fail();

            var arr = "00 FF EF".Split(' ');
            var buf = arr.Select(x => Convert.ToByte(x, 16));
        }

        [TestMethod()]
        public void IsTodayTest()
        {
            var date = DateTime.Now;
            var res1 = date.IsToday();
            Assert.IsTrue(res1);
            var dt2 = date.AddDays(1);
            var res2 = dt2.IsToday();
            Assert.IsFalse(res2);
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using MPFastDevLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPFastDevLibrary.Extensions.Tests
{
    [TestClass()]
    public class StringExtensionTests
    {
        [TestMethod()]
        public void IsIntegerTest()
        {
            string s = "12443";
            var res1 = s.IsInteger();
            Assert.IsTrue(res1);

            string s2 = "sdw1231";
            var res2 = s2.IsInteger();
            Assert.IsFalse(res2);
        }

        [TestMethod()]
        public void IsDoubleTest()
        {
            string s = "124.43";
            var res1 = s.IsDouble();
            Assert.IsTrue(res1);
            string s2 = "123.34231";
            var res2 = s2.IsDouble();
            Assert.IsTrue(res2);
            string s3 = "dsajdghah1231";
            var res3 = s3.IsDouble();
            Assert.IsFalse(res3);
        }

        [TestMethod()]
        public void IsNumberTest()
        {
            string s = "124.43";
            var res1 = s.IsNumber();
            Assert.IsFalse(res1);
            string s2 = "1231";
            var res2 = s2.IsNumber();
            Assert.IsTrue(res2);
            string s3 = "dsajdghah1231";
            var res3 = s3.IsNumber();
            Assert.IsFalse(res3);
        }

        [TestMethod()]
        public void IsCharactersTest()
        {
            string s = "adasasf";
            var res1 = s.IsCharacters();
            Assert.IsTrue(res1);
            string s2 = "ADSscscvf";
            var res2 = s2.IsCharacters();
            Assert.IsTrue(res2);
            string s3 = "dsajdghah1231";
            var res3 = s3.IsCharacters();
            Assert.IsFalse(res3);
        }
    }
}

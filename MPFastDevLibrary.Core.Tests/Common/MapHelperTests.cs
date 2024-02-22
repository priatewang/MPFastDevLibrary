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
    public class MapHelperTests
    {
        class TestClass1
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public string Desc { get; set; }
        }

        class TestClass2
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public string Desc { get; set; }
        }

        [TestMethod()]
        public void MappingTest()
        {
            TestClass1 t1 = new TestClass1()
            {
                Id = 1,
                Desc = "1号",
                Name = "Name1",
            };
            var t2 = MapHelper.Mapping<TestClass2, TestClass1>(t1);

            Assert.AreEqual(1, t2.Id);
            Assert.AreEqual("1号", t2.Desc);
            Assert.AreEqual("Name1", t2.Name);
        }

        [TestMethod()]
        public void MappingTest1()
        {
            TestClass1 t1 = new TestClass1()
            {
                Id = 1,
                Desc = "1号",
                Name = "Name1",
            };

            TestClass1 t2 = new TestClass1();
            MapHelper.Mapping(ref t2, t1);

            Assert.AreEqual(1, t2.Id);
            Assert.AreEqual("1号", t2.Desc);
            Assert.AreEqual("Name1", t2.Name);
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using MPFastDevLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using DescriptionAttribute = System.ComponentModel.DescriptionAttribute;

namespace MPFastDevLibrary.Common.Tests
{
    [TestClass()]
    public class EnumHelperTests
    {
        enum MyEnum
        {
            [Description("测试1")]
            Test1,

            [Description("测试2")]
            Test2,

            [Description("测试3")]
            Test3,
        }

        [TestMethod()]
        public void GetEnumContentsTest()
        {
            var list = EnumHelper.GetEnumContents<MyEnum>();
            Assert.IsNotNull(list);
            Assert.AreEqual(3, list.Count);
            Assert.AreEqual("Test1", list[0]);
        }

        [TestMethod()]
        public void GetEnumDescriptionsTest()
        {
            var list = EnumHelper.GetEnumDescriptions<MyEnum>();
            Assert.IsNotNull(list);
            Assert.AreEqual(3, list.Count);
            Assert.AreEqual("测试1", list[0]);
        }

        [TestMethod()]
        public void GetDescriptionTest()
        {
            var str = MyEnum.Test1.GetDescription();
            Assert.AreEqual("测试1", str);
        }

        [TestMethod()]
        public void EnumConvertByDescriptionTest()
        {
            var em = EnumHelper.EnumConvertByDescription<MyEnum>("测试2");
            Assert.AreEqual(MyEnum.Test2, em);
        }

        [TestMethod()]
        public void ConvertToEnumTest()
        {
            var em = EnumHelper.ConvertToEnum<MyEnum>("Test2");
            Assert.AreEqual(MyEnum.Test2, em);
        }
    }
}

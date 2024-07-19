using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MPFastDevLibrary.Common;

namespace MPFastDevLibrary.Common.Tests
{
    [TestClass()]
    public class CsvHelperTests
    {
        [TestMethod()]
        public void WriteToCsvByDataTableTest()
        {
            DataTable dt = new DataTable();
            for (int i = 0; i < 200; i++)
            {
                dt.Columns.Add();
            }
            Random random = new Random();
            for (int i = 0; i < 100; i++)
            {
                string[] arr = new string[200];
                for (int j = 0; j < 200; j++)
                {
                    arr[j] = random.Next(100).ToString();
                }
                dt.Rows.Add(arr);
            }

            bool issuccess = CsvHelper.WriteToCsvByDataTable(dt, "csvTest.csv", false);
            Assert.IsTrue(issuccess);
            var lines = File.ReadAllLines("csvTest.csv");
            Assert.AreEqual(100, lines.Length);
        }

        [TestMethod()]
        public void ReadCsvToDataTableTest()
        {
            var dt = CsvHelper.ReadCsvToDataTable("csvTest.csv", false);
            Assert.IsNotNull(dt);
            Assert.AreEqual(200, dt.Columns.Count);
            Assert.AreEqual(100, dt.Rows.Count);
        }

        [TestMethod()]
        public void ReadCsvByStreamTest()
        {
            var dt = CsvHelper.ReadCsvByStream("csvTest.csv", false);
            Assert.IsNotNull(dt);
            Assert.AreEqual(200, dt.Columns.Count);
            Assert.AreEqual(100, dt.Rows.Count);
        }

        [TestMethod()]
        public void ReadCsvTest()
        {
            var lines = CsvHelper.ReadCsv("csvTest.csv", false);
            Assert.AreEqual(100, lines.Count);
        }

        [TestMethod()]
        public void ReadCsvTest1()
        {
            string[] lines = new string[]
            {
                "ID,Name,Desc,Value,Type",
                "1,test,测试,5.3,System.Double",
                "2,test2,测试2,77,System.Int32"
            };
            var res = CsvHelper.ReadCsv<TestModel>("lines.csv");
            Assert.AreEqual(2, res.Count);
            //列出测试数据
            Assert.AreEqual(1, res[0].ID);
            Assert.AreEqual("test", res[0].Name);
            Assert.AreEqual(5.3, res[0].Value);
            Assert.AreEqual(typeof(double), res[0].Type);
            Assert.AreEqual(2, res[1].ID);
            Assert.AreEqual("test2", res[1].Name);
            Assert.AreEqual(77.0, res[1].Value);
            Assert.AreEqual(typeof(int), res[1].Type);
        }

        [TestMethod()]
        public void WriteCsvTest()
        {
            //测试数据
            var list = new List<TestModel>()
            {
                new TestModel()
                {
                    ID = 1,
                    Name = "test",
                    Desc = "测试",
                    Value = 5.3,
                    Type = typeof(double)
                },
                new TestModel()
                {
                    ID = 2,
                    Name = "test2",
                    Desc = "测试2",
                    Value = 77,
                    Type = typeof(int)
                }
            };
            //var res = CsvHelper.WriteCsv(list, "lines.csv");
            //Assert.AreEqual(3, res.Count);
            //Assert.AreEqual("ID,Name,Desc,Value,Type", res[0]);
            //Assert.AreEqual("1,test,测试,5.3,System.Double", res[1]);
            //Assert.AreEqual("2,test2,测试2,77,System.Int32", res[2]);
        }

        public class TestModel
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public string Desc { get; set; }
            public double Value { get; set; }

            public Type Type { get; set; }
        }
    }
}

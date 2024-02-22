using Microsoft.VisualStudio.TestTools.UnitTesting;
using MPFastDevLibrary.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}

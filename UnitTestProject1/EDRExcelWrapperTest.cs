using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RKPModels;

namespace UnitTestProject1
{
    [TestClass]
    public class EDRExcelWrapperTest
    {
        const string kennliste_path = @"..\..\..\Kennliste2.xlsx";
        [TestMethod]
        public void RightColumn3()
        {
            var excelWrapper = new EDRExcelWrapper(kennliste_path);
            string[] obj = excelWrapper.GetRow(2);
            Assert.AreEqual(obj[2].Trim(), "LP FY 20");
            Assert.AreEqual(obj[7].Trim(), "Pump ID");
        }
        [TestMethod]
        public void SearchTest()
        {
            var excelWrapper = new EDRExcelWrapper(kennliste_path);
            var res = excelWrapper.SearchRow("D951-5001");
            Assert.AreEqual(1, res.Count);
            string[] obj = res[0];
            Assert.AreEqual(obj[0].Trim(), "D951-5001");
            Assert.AreEqual(obj[1].Trim(), "HPR18B1RKP019SM28F1Y00RKP019SM28F1Y00");
        }
        [TestMethod]
        public void SearchCaseInsensitive()
        {
            var excelWrapper = new EDRExcelWrapper(kennliste_path);
            var res = excelWrapper.SearchRow("d951-5001");
            Assert.AreEqual(1, res.Count);
            string[] obj = res[0];
            Assert.AreEqual(obj[0].Trim(), "D951-5001");
            Assert.AreEqual(obj[1].Trim(), "HPR18B1RKP019SM28F1Y00RKP019SM28F1Y00");
        }
        [TestMethod]
        public void SearchPump1()
        {
            var excelWrapper = new EDRExcelWrapper(kennliste_path);
            var res = excelWrapper.SearchRow("D954-2305");
            Assert.AreEqual(1, res.Count);
            string[] obj = res[0];
            Assert.AreEqual(obj[0].Trim(), "D954-2305");
            Assert.AreEqual(obj[1].Trim(), "HPR15A7RKP063KM28S2Z11");
        }
        [TestMethod]
        public void RightColumnCount()
        {
            var excelWrapper = new EDRExcelWrapper(kennliste_path);
            for(int cnt = 50; cnt < 100; cnt++)
            {
                var res = excelWrapper.GetRow(cnt);
                Assert.IsTrue(51 <= res.Length,"there are only "+res.Length+" columns in row "+cnt);
            }
        }
        [TestMethod]
        public void RestartAtEndSearch()
        {
            var excelWrapper = new EDRExcelWrapper(kennliste_path);
            Assert.IsNotNull(excelWrapper.GetRow(100));
            Assert.IsNotNull(excelWrapper.GetRow(99));
        }
        [TestMethod]
        public void FindLastPumpInList()
        {
            var excelWrapper = new EDRExcelWrapper(kennliste_path);
            var lastpump = "D959Z5003";
            var retlist = (excelWrapper.SearchRow(lastpump));
            Assert.IsNotNull(retlist);
            Assert.AreEqual(1, retlist.Count);
            Assert.AreEqual("D959Z5003", retlist[0][0]);
        }
    }
}

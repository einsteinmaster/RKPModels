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
            // ssl_marius
            // 789Marius987.-
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
    }
}

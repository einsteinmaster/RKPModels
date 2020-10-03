using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RKPModels;

namespace UnitTestProject1
{
    [TestClass]
    public class StatisticsTest
    {
        const string kennliste_path = @"..\..\..\Kennliste2.xlsx";
        [TestMethod]
        public void NoExcept()
        {
            MaterialMatrix avaiMat = new MaterialMatrix();
            avaiMat[3, 0] = 1;
            avaiMat[3, 13] = 1;
            avaiMat[3, 38] = 1;
            var stat = Statistics.CalcStatistics(new EDRExcelWrapper(kennliste_path), avaiMat);
        }
        [TestMethod]
        public void HasResults()
        {
            MaterialMatrix avaiMat = new MaterialMatrix();
            avaiMat[3, 0] = 1;
            avaiMat[3, 13] = 1;
            avaiMat[3, 38] = 1;
            var stat = Statistics.CalcStatistics(new EDRExcelWrapper(kennliste_path), avaiMat);
            Assert.AreEqual(3, stat.countAllAvai);
        }
        [TestMethod]
        public void HasAllPumps()
        {
            MaterialMatrix avaiMat = new MaterialMatrix(3);
            var stat = Statistics.CalcStatistics(new EDRExcelWrapper(kennliste_path), avaiMat);
            Assert.AreEqual(stat.resolvable, stat.countAllAvai);
        }
        [TestMethod]
        public void HasNoPump()
        {
            MaterialMatrix avaiMat = new MaterialMatrix();
            var stat = Statistics.CalcStatistics(new EDRExcelWrapper(kennliste_path), avaiMat);
            Assert.AreEqual(null, stat);
        }
        [TestMethod]
        public void HasLess3Mat()
        {
            MaterialMatrix avaiMat = new MaterialMatrix();
            avaiMat[3, 0] = 1;
            avaiMat[3, 13] = 1;
            var stat = Statistics.CalcStatistics(new EDRExcelWrapper(kennliste_path), avaiMat);
            Assert.AreEqual(null, stat);
        }
        [TestMethod]
        public void SaveNonCalculateablePumps()
        {
            MaterialMatrix avaiMat = new MaterialMatrix(100);
            var stat = Statistics.CalcStatistics(new EDRExcelWrapper(kennliste_path), avaiMat);
            List<string[]> nonCalculatable = new List<string[]>(); 
            for(int cnt = 0;cnt < stat.allPumps.Count; cnt++)
            {
                var pump = stat.allPumps[cnt];
                if (string.IsNullOrWhiteSpace(pump[0])) throw new Exception();
                if (stat.allAvaiPumps.Contains(pump))
                {

                }
                else
                {
                    nonCalculatable.Add(pump);
                }
            }
            Assert.AreEqual(stat.countAll - stat.resolvable, nonCalculatable.Count);
            StringBuilder str = new StringBuilder();
            for (int cnt = 0; cnt < nonCalculatable.Count; cnt++)
            {
                var pump = nonCalculatable[cnt];
                if (string.IsNullOrWhiteSpace(pump[0])) throw new Exception();
                for (int x = 0; x < pump.Length; x++)
                {
                    str.Append(pump[x].Replace(';',' ').Replace('\n',' ').Replace('\r',' '));
                    str.Append(";");
                }
                str.AppendLine();
            }
            File.WriteAllText("notCalculatable.csv", str.ToString());
        }
    }
}

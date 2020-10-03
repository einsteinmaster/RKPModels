using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RKPModels;

namespace UnitTestProject1
{
    [TestClass]
    public class AvailibilityCheckTest
    {
        [TestMethod]
        public void GehauseSoloDa()
        {
            MaterialMatrix avaiMat = new MaterialMatrix();
            avaiMat[1, 0] = 1;
            avaiMat[1, 16] = 1;
            avaiMat[1, 33] = 1;
            string[] obj = new string[50];
            for (int cnt = 0; cnt < obj.Length; cnt++) obj[cnt] = "testStr_" + cnt;
            obj[0] = "D";
            obj[7] = "HK";
            obj[8] = "R";
            obj[9] = "14";
            obj[10] = "XX";
            obj[11] = "RKP";
            obj[12] = "032";
            obj[13] = "H";
            obj[14] = "M";
            obj[15] = "35";
            obj[16] = "D1";
            obj[17] = "Z";
            obj[18] = "00";
            obj[19] = "";
            Assert.IsTrue(AvailabilityCheck.IsAvailable(obj, avaiMat));
        }
        [TestMethod]
        public void GehauseSoloNichtDa()
        {
            MaterialMatrix avaiMat = new MaterialMatrix(100);
            avaiMat[1, 0] = 0;
            string[] obj = new string[50];
            for (int cnt = 0; cnt < obj.Length; cnt++) obj[cnt] = "testStr_" + cnt;
            obj[0] = "D";
            obj[7] = "HK";
            obj[8] = "R";
            obj[9] = "14";
            obj[10] = "XX";
            obj[11] = "RKP";
            obj[12] = "032";
            obj[13] = "H";
            obj[14] = "M";
            obj[15] = "35";
            obj[16] = "D1";
            obj[17] = "Z";
            obj[18] = "00";
            obj[19] = "";
            Assert.IsFalse(AvailabilityCheck.IsAvailable(obj, avaiMat));
        }
        [TestMethod]
        public void GehauseDS1Da()
        {
            MaterialMatrix avaiMat = new MaterialMatrix();
            avaiMat[1, 1] = 1;
            avaiMat[1, 14] = 1;
            avaiMat[1, 33] = 1;
            string[] obj = new string[50];
            for (int cnt = 0; cnt < obj.Length; cnt++) obj[cnt] = "testStr_" + cnt;
            obj[0] = "D";
            obj[7] = "HK";
            obj[8] = "R";
            obj[9] = "14";
            obj[10] = "A7";
            obj[11] = "RKP";
            obj[12] = "032";
            obj[13] = "H";
            obj[14] = "M";
            obj[15] = "35";
            obj[16] = "D1";
            obj[17] = "Z";
            obj[18] = "00";
            obj[19] = "DS1";
            obj[20] = "";
            Assert.IsTrue(AvailabilityCheck.IsAvailable(obj, avaiMat));
        }
        [TestMethod]
        public void GehauseDS1NichtDa()
        {
            MaterialMatrix avaiMat = new MaterialMatrix(100);
            avaiMat[1, 1] = 0;
            string[] obj = new string[50];
            for (int cnt = 0; cnt < obj.Length; cnt++) obj[cnt] = "testStr_" + cnt;
            obj[0] = "D";
            obj[7] = "HK";
            obj[8] = "R";
            obj[9] = "14";
            obj[10] = "A7";
            obj[11] = "RKP";
            obj[12] = "032";
            obj[13] = "H";
            obj[14] = "M";
            obj[15] = "35";
            obj[16] = "D1";
            obj[17] = "Z";
            obj[18] = "00";
            obj[19] = "DS1";
            obj[20] = "";
            Assert.IsFalse(AvailabilityCheck.IsAvailable(obj, avaiMat));
        }
        [TestMethod]
        public void Lagerdeckel1DoppelDa()
        {
            MaterialMatrix avaiMat = new MaterialMatrix();
            avaiMat[3, 1] = 1;
            avaiMat[3, 14] = 1;
            avaiMat[3, 33] = 1;
            avaiMat[0, 0] = 1;
            avaiMat[0, 16] = 1;
            avaiMat[0, 30] = 1;
            string[] obj = new string[50];
            for (int cnt = 0; cnt < obj.Length; cnt++) obj[cnt] = "testStr_" + cnt;
            obj[0] = "D";
            obj[7] = "HK";
            obj[8] = "R";
            obj[9] = "14";
            obj[10] = "A7";
            obj[11] = "RKP";
            obj[12] = "063";
            obj[13] = "H";
            obj[14] = "M";
            obj[15] = "35";
            obj[16] = "D1";
            obj[17] = "Z";
            obj[18] = "00";
            obj[19] = "RKP";
            obj[20] = "019";
            obj[21] = "H";
            obj[22] = "M";
            obj[23] = "35";
            obj[24] = "C1";
            obj[25] = "Z";
            obj[26] = "00";
            obj[27] = "";
            Assert.IsTrue(AvailabilityCheck.IsAvailable(obj, avaiMat));
        }
        [TestMethod]
        public void Lagerdeckel1DoppelNichtDa()
        {
            MaterialMatrix avaiMat = new MaterialMatrix(100);
            avaiMat[3, 1] = 0;
            string[] obj = new string[50];
            for (int cnt = 0; cnt < obj.Length; cnt++) obj[cnt] = "testStr_" + cnt;
            obj[0] = "D";
            obj[7] = "HK";
            obj[8] = "R";
            obj[9] = "14";
            obj[10] = "A7";
            obj[11] = "RKP";
            obj[12] = "063";
            obj[13] = "H";
            obj[14] = "M";
            obj[15] = "35";
            obj[16] = "D1";
            obj[17] = "Z";
            obj[18] = "00";
            obj[19] = "RKP";
            obj[20] = "019";
            obj[21] = "H";
            obj[22] = "M";
            obj[23] = "35";
            obj[24] = "D1";
            obj[25] = "Z";
            obj[26] = "00";
            obj[27] = "";
            Assert.IsFalse(AvailabilityCheck.IsAvailable(obj, avaiMat));
        }
        [TestMethod]
        public void ReglerS1Da()
        {
            MaterialMatrix avaiMat = new MaterialMatrix();
            avaiMat[3, 0] = 1;
            avaiMat[3, 13] = 1;
            avaiMat[3, 40] = 1;
            string[] obj = new string[50];
            for (int cnt = 0; cnt < obj.Length; cnt++) obj[cnt] = "testStr_" + cnt;
            obj[0] = "D";
            obj[7] = "HK";
            obj[8] = "R";
            obj[9] = "14";
            obj[10] = "A7";
            obj[11] = "RKP";
            obj[12] = "063";
            obj[13] = "H";
            obj[14] = "M";
            obj[15] = "35";
            obj[16] = "S1";
            obj[17] = "Z";
            obj[18] = "00";
            obj[19] = "";
            Assert.IsTrue(AvailabilityCheck.IsAvailable(obj, avaiMat));
        }
        [TestMethod]
        public void ReglerS1NichtDa()
        {
            MaterialMatrix avaiMat = new MaterialMatrix(100);
            avaiMat[3, 40] = 0;
            string[] obj = new string[50];
            for (int cnt = 0; cnt < obj.Length; cnt++) obj[cnt] = "testStr_" + cnt;
            obj[0] = "D";
            obj[7] = "HK";
            obj[8] = "R";
            obj[9] = "14";
            obj[10] = "A7";
            obj[11] = "RKP";
            obj[12] = "063";
            obj[13] = "H";
            obj[14] = "M";
            obj[15] = "35";
            obj[16] = "S1";
            obj[17] = "Z";
            obj[18] = "00";
            obj[19] = "";
            Assert.IsFalse(AvailabilityCheck.IsAvailable(obj, avaiMat));
        }
        [TestMethod]
        public void DeckelHFCB1Da()
        {
            MaterialMatrix avaiMat = new MaterialMatrix();
            avaiMat[3, 1] = 1;
            avaiMat[3, 19] = 1;
            avaiMat[3, 40] = 1;
            string[] obj = new string[50];
            for (int cnt = 0; cnt < obj.Length; cnt++) obj[cnt] = "testStr_" + cnt;
            obj[0] = "D";
            obj[7] = "HK";
            obj[8] = "R";
            obj[9] = "14";
            obj[10] = "B1";
            obj[11] = "RKP";
            obj[12] = "063";
            obj[13] = "H";
            obj[14] = "C";
            obj[15] = "35";
            obj[16] = "S1";
            obj[17] = "Z";
            obj[18] = "00";
            obj[19] = "DS1";
            obj[20] = "";
            Assert.IsTrue(AvailabilityCheck.IsAvailable(obj, avaiMat));
        }
        [TestMethod]
        public void DeckelHFCB1NichtDa()
        {
            MaterialMatrix avaiMat = new MaterialMatrix(100);
            avaiMat[3, 19] = 0;
            string[] obj = new string[50];
            for (int cnt = 0; cnt < obj.Length; cnt++) obj[cnt] = "testStr_" + cnt;
            obj[0] = "D";
            obj[7] = "HK";
            obj[8] = "R";
            obj[9] = "14";
            obj[10] = "B1";
            obj[11] = "RKP";
            obj[12] = "063";
            obj[13] = "H";
            obj[14] = "C";
            obj[15] = "35";
            obj[16] = "S1";
            obj[17] = "Z";
            obj[18] = "00";
            obj[19] = "DS1";
            obj[20] = "";
            Assert.IsFalse(AvailabilityCheck.IsAvailable(obj, avaiMat));
        }
        [TestMethod]
        public void UnvalidKey1()
        {
            MaterialMatrix avaiMat = new MaterialMatrix(100);
            string[] obj = new string[50];
            for (int cnt = 0; cnt < obj.Length; cnt++) obj[cnt] = "testStr_" + cnt;
            obj[0] = "D";
            obj[7] = "HK";
            obj[8] = "R";
            obj[9] = "14";
            obj[10] = "H1";
            obj[11] = "RKP";
            obj[12] = "063";
            obj[13] = "H";
            obj[14] = "A";
            obj[15] = "35";
            obj[16] = "S1";
            obj[17] = "Z";
            obj[18] = "00";
            obj[19] = "DS1";
            obj[20] = "";
            Assert.IsFalse(AvailabilityCheck.IsAvailable(obj, avaiMat));
        }
        [TestMethod]
        public void UnvalidKey2()
        {
            MaterialMatrix avaiMat = new MaterialMatrix(100);
            string[] obj = new string[50];
            for (int cnt = 0; cnt < obj.Length; cnt++) obj[cnt] = "testStr_" + cnt;
            obj[0] = "D";
            obj[7] = "HK";
            obj[8] = "R";
            obj[9] = "14";
            obj[10] = "B1";
            obj[11] = "RKP";
            obj[12] = "063";
            obj[13] = "H";
            obj[14] = "M";
            obj[15] = "35";
            obj[16] = "S1";
            obj[17] = "Z";
            obj[18] = "00";
            obj[19] = "";
            obj[20] = "";
            Assert.IsFalse(AvailabilityCheck.IsAvailable(obj, avaiMat));
        }
        [TestMethod]
        public void UnvalidKey3()
        {
            MaterialMatrix avaiMat = new MaterialMatrix(100);
            string[] obj = new string[50];
            for (int cnt = 0; cnt < obj.Length; cnt++) obj[cnt] = "testStr_" + cnt;
            obj[0] = "D";
            obj[7] = "HK";
            obj[8] = "R";
            obj[9] = "14";
            obj[10] = "A1";
            obj[11] = "RKP";
            obj[12] = "063";
            obj[13] = "H";
            obj[14] = "C";
            obj[15] = "35";
            obj[16] = "S1";
            obj[17] = "Z";
            obj[18] = "00";
            obj[19] = "DS1";
            obj[20] = "";
            Assert.IsFalse(AvailabilityCheck.IsAvailable(obj, avaiMat));
        }
        [TestMethod]
        public void UnvalidKey4()
        {
            MaterialMatrix avaiMat = new MaterialMatrix(100);
            string[] obj = new string[50];
            for (int cnt = 0; cnt < obj.Length; cnt++) obj[cnt] = "testStr_" + cnt;
            obj[0] = "D";
            obj[7] = "HK";
            obj[8] = "L";
            obj[9] = "14";
            obj[10] = "A1";
            obj[11] = "FRP";
            obj[12] = "019";
            obj[13] = "H";
            obj[14] = "M";
            obj[15] = "35";
            obj[16] = "D1";
            obj[17] = "Z";
            obj[18] = "00";
            obj[19] = "";
            obj[20] = "";
            Assert.IsFalse(AvailabilityCheck.IsAvailable(obj, avaiMat));
        }
        [TestMethod]
        public void UnvalidKey5()
        {
            MaterialMatrix avaiMat = new MaterialMatrix(100);
            string[] obj = new string[50];
            for (int cnt = 0; cnt < obj.Length; cnt++) obj[cnt] = "testStr_" + cnt;
            obj[0] = "D";
            obj[7] = "HK";
            obj[8] = "R";
            obj[9] = "14";
            obj[10] = "A7";
            obj[11] = "RKP";
            obj[12] = "063";
            obj[13] = "H";
            obj[14] = "M";
            obj[15] = "35";
            obj[16] = "D1";
            obj[17] = "Z";
            obj[18] = "00";
            obj[19] = "RKP";
            obj[20] = "019";
            obj[21] = "H";
            obj[22] = "M";
            obj[23] = "35";
            obj[24] = "D3";
            obj[25] = "Z";
            obj[26] = "00";
            obj[27] = "";
            Assert.IsFalse(AvailabilityCheck.IsAvailable(obj, avaiMat));
        }
        [TestMethod]
        public void ReglerS2Da()
        {
            MaterialMatrix avaiMat = new MaterialMatrix();
            avaiMat[3, 0] = 1;
            avaiMat[3, 13] = 1;
            avaiMat[3, 38] = 1;
            string[] obj = new string[20];
            for (int cnt = 0; cnt < obj.Length; cnt++) obj[cnt] = "testStr_" + cnt;
            obj[0] = "D";
            obj[7] = "HP";
            obj[8] = "R";
            obj[9] = "15";
            obj[10] = "A7";
            obj[11] = "RKP";
            obj[12] = "063";
            obj[13] = "K";
            obj[14] = "M";
            obj[15] = "18";
            obj[16] = "S2";
            obj[17] = "Z";
            obj[18] = "11";
            obj[19] = "";
            Assert.IsTrue(AvailabilityCheck.IsAvailable(obj, avaiMat));
        }
        [TestMethod]
        public void ReglerS2NichtDa()
        {
            MaterialMatrix avaiMat = new MaterialMatrix(100);
            avaiMat[3, 38] = 0;
            string[] obj = new string[20];
            for (int cnt = 0; cnt < obj.Length; cnt++) obj[cnt] = "testStr_" + cnt;
            obj[0] = "D";
            obj[7] = "HP";
            obj[8] = "R";
            obj[9] = "15";
            obj[10] = "A7";
            obj[11] = "RKP";
            obj[12] = "063";
            obj[13] = "K";
            obj[14] = "M";
            obj[15] = "18";
            obj[16] = "S2";
            obj[17] = "Z";
            obj[18] = "11";
            obj[19] = "";
            Assert.IsFalse(AvailabilityCheck.IsAvailable(obj, avaiMat));
        }
    }
}

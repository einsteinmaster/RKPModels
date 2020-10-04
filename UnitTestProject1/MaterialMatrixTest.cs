using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RKPModels;

namespace UnitTestProject1
{
    [TestClass]
    public class MaterialMatrixTest
    {
        const string path_lagerVorlage = @"..\..\..\lagerVorlage.csv";
        [TestMethod]
        public void TestInactive37()
        {
            MaterialMatrix mat = new MaterialMatrix();
            Assert.IsFalse(mat.IsInactive(0, 37));
            Assert.IsTrue(mat.IsInactive(1, 37));
            Assert.IsTrue(mat.IsInactive(2, 37));
            Assert.IsFalse(mat.IsInactive(3, 37));
            Assert.IsTrue(mat.IsInactive(4, 37));
            Assert.IsTrue(mat.IsInactive(5, 37));
            Assert.IsFalse(mat.IsInactive(6, 37));
            mat[0, 37] = 455;
            Assert.AreEqual(mat[0, 37], 455);
            Assert.AreEqual(mat[1, 37], 455);
            Assert.AreEqual(mat[2, 37], 455);
            Assert.AreNotEqual(mat[3, 37], 455);
            Assert.AreNotEqual(mat[4, 37], 455);
            Assert.AreNotEqual(mat[5, 37], 455);
            Assert.AreNotEqual(mat[6, 37], 455);
            mat[2, 37] = 32;
            Assert.AreEqual(mat[0, 37], 32);
            Assert.AreEqual(mat[1, 37], 32);
            Assert.AreEqual(mat[2, 37], 32);
            Assert.AreNotEqual(mat[3, 37], 32);
            Assert.AreNotEqual(mat[4, 37], 32);
            Assert.AreNotEqual(mat[5, 37], 32);
            Assert.AreNotEqual(mat[6, 37], 32);
            mat[5, 37] = 55;
            Assert.AreEqual(mat[0, 37], 32);
            Assert.AreEqual(mat[1, 37], 32);
            Assert.AreEqual(mat[2, 37], 32);
            Assert.AreEqual(mat[3, 37], 55);
            Assert.AreEqual(mat[4, 37], 55);
            Assert.AreEqual(mat[5, 37], 55);
            Assert.AreNotEqual(mat[6, 37], 55);
            mat[6, 37] = 67;
            Assert.AreEqual(mat[0, 37], 32);
            Assert.AreEqual(mat[1, 37], 32);
            Assert.AreEqual(mat[2, 37], 32);
            Assert.AreEqual(mat[3, 37], 55);
            Assert.AreEqual(mat[4, 37], 55);
            Assert.AreEqual(mat[5, 37], 55);
            Assert.AreEqual(mat[6, 37], 67);
            mat[3, 37] = -43;
            Assert.AreEqual(mat[0, 37], 32);
            Assert.AreEqual(mat[1, 37], 32);
            Assert.AreEqual(mat[2, 37], 32);
            Assert.AreEqual(mat[3, 37], -43);
            Assert.AreEqual(mat[4, 37], -43);
            Assert.AreEqual(mat[5, 37], -43);
            Assert.AreEqual(mat[6, 37], 67);
        }
        [TestMethod]
        public void TestAddition1()
        {
            MaterialMatrix mat = new MaterialMatrix();
            mat[5, 14] = 5;
            Assert.AreEqual(mat[5, 14], 5);
            MaterialMatrix mat2 = new MaterialMatrix();
            mat2[5, 14] = -40;
            Assert.AreEqual(mat2[5, 14], -40);
            mat = mat + mat2;
            Assert.AreEqual(mat[5, 14], -35);
        }
        [TestMethod]
        public void TestAddition2()
        {
            MaterialMatrix mat = new MaterialMatrix(1);
            mat[3, 26] = 5;
            Assert.AreEqual(mat[3, 26], 5);
            Assert.AreEqual(mat[2, 26], 1);
            MaterialMatrix mat2 = new MaterialMatrix(23);
            mat2[3, 26] = -40;
            Assert.AreEqual(mat2[3, 26], -40);
            Assert.AreEqual(mat2[2, 26], 23);
            mat = mat + mat2;
            Assert.AreEqual(mat[3, 26], -35);
            Assert.AreEqual(mat[2, 26], 24);
        }
        [TestMethod]
        public void TestIsNegative1()
        {
            MaterialMatrix mat = new MaterialMatrix(1);
            Assert.IsTrue(mat.IsNotNegativ());
            mat = new MaterialMatrix();
            Assert.IsTrue(mat.IsNotNegativ());
            mat = new MaterialMatrix(-1);
            Assert.IsFalse(mat.IsNotNegativ());
        }
        [TestMethod]
        public void TestIsNegative2()
        {
            MaterialMatrix mat = new MaterialMatrix(-1);
            mat[3, 34] = 5;
            Assert.IsFalse(mat.IsNotNegativ());
            mat = new MaterialMatrix(1);
            mat[3, 34] = 5;
            Assert.IsTrue(mat.IsNotNegativ());
            mat[3, 34] = -5;
            Assert.IsFalse(mat.IsNotNegativ());
            mat = new MaterialMatrix();
            mat[3, 34] = 5;
            Assert.IsTrue(mat.IsNotNegativ());
            mat[3, 34] = -5;
            Assert.IsFalse(mat.IsNotNegativ());
        }
        [TestMethod]
        public void TestDeserialization()
        {
            MaterialMatrix mat = MaterialMatrix.ReadFromFile(path_lagerVorlage);
            Assert.AreEqual(6, mat[0, 0]);
            Assert.AreEqual(1, mat[0, 38]);
            Assert.AreEqual(3, mat[6, 38]);
            Assert.AreEqual(0, mat[6, 39]);
            Assert.AreEqual(0, mat[6, 40]);
        }
    }
}

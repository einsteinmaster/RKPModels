using Microsoft.VisualStudio.TestTools.UnitTesting;
using RKPModels;

namespace UnitTestProject1
{
    [TestClass]
    public class SQLTest
    {
        const string host = @"10.250.0.10";
        const string kennliste_path = @"..\..\..\Kennliste2.xlsx";
        const string path_lagerVorlage = @"..\..\..\lagerVorlage.csv";
        [TestMethod]
        public void ImportKennliste()
        {
            using (var client = new MySQLArticleClient(host))
            {
                client.ImportFromFile(kennliste_path);
            }
        }
        [TestMethod]
        public void GetPump()
        {
            using (var client = new MySQLArticleClient(host))
            {
                string article = "0514400337";
                var key = client.GetProductkey(article);
                Assert.IsNotNull(key);
                Assert.AreEqual(article, key[0]);
                Assert.AreEqual("HPR18A1RKP019SC21F1Z00", key[1]);
                Assert.AreEqual("HP", key[7]);
            }
        }
        [TestMethod]
        public void SearchTest()
        {
            var excelWrapper = new MySQLArticleClient(host);
            var res = excelWrapper.GetProductkey("D951-5001");
            string[] obj = res;
            Assert.AreEqual(obj[0].Trim(), "D951-5001");
            Assert.AreEqual(obj[1].Trim(), "HPR18B1RKP019SM28F1Y00RKP019SM28F1Y00");
        }
        [TestMethod]
        public void SearchCaseInsensitive()
        {
            var excelWrapper = new MySQLArticleClient(host);
            var res = excelWrapper.GetProductkey("d951-5001");
            string[] obj = res;
            Assert.AreEqual(obj[0].Trim(), "D951-5001");
            Assert.AreEqual(obj[1].Trim(), "HPR18B1RKP019SM28F1Y00RKP019SM28F1Y00");
        }
        [TestMethod]
        public void SearchPump1()
        {
            var excelWrapper = new MySQLArticleClient(host);
            var res = excelWrapper.GetProductkey("D954-2305");
            string[] obj = res;
            Assert.AreEqual(obj[0].Trim(), "D954-2305");
            Assert.AreEqual(obj[1].Trim(), "HPR15A7RKP063KM28S2Z11");
        }
        [TestMethod]
        public void PartialSearch()
        {
            var excelWrapper = new MySQLArticleClient(host);
            var res = excelWrapper.SearchProductkey("D951-504");
            Assert.AreEqual(1, res.Count);
            string[] obj = res[0];
            Assert.AreEqual(obj[0].Trim(), "D951-5041");
            Assert.AreEqual(obj[1].Trim(), "HPR18A7RKP019SM28D6ZA0RKP019SM28D6ZA0");
        }
        [TestMethod]
        public void NotFoundSearch()
        {
            var excelWrapper = new MySQLArticleClient(host);
            var res = excelWrapper.GetProductkey("D951-5051");
            Assert.IsNull(res);
        }
        [TestMethod]
        public void SetMaterialMatrix()
        {
            var excelWrapper = new MySQLArticleClient(host);
            MaterialMatrix mat = MaterialMatrix.ReadFromFile(path_lagerVorlage);
            excelWrapper.SetAvailibilityMatrix(mat);
        }
        [TestMethod]
        public void GetMaterialMatrix()
        {
            var excelWrapper = new MySQLArticleClient(host);
            var res = excelWrapper.GetAvailibilityMatrix();
            Assert.IsNotNull(res);
        }
        [TestMethod]
        public void MaterialMatrixValues()
        {
            var excelWrapper = new MySQLArticleClient(host);
            MaterialMatrix mat = MaterialMatrix.ReadFromFile(path_lagerVorlage);
            excelWrapper.SetAvailibilityMatrix(mat);
            var res = excelWrapper.GetAvailibilityMatrix();
            Assert.IsNotNull(res);
            Assert.AreEqual(mat, res);
        }
        [TestMethod]
        public void AvailibilityValues()
        {
            var excelWrapper = new MySQLArticleClient(host);
            string article = "D951-5001";
            bool avai = true;
            excelWrapper.SetAvailibility(article, avai);
            var res = excelWrapper.GetAvailibility(article);
            Assert.AreEqual(avai, res);
            avai = false;
            excelWrapper.SetAvailibility(article, avai);
            res = excelWrapper.GetAvailibility(article);
            Assert.AreEqual(avai,res);
        }
    }
}

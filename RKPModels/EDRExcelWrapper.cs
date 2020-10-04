using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection.Emit;

namespace RKPModels
{
    public class EDRExcelWrapper : ExcelWrapper , IArticleDB
    {
        private string filename;
        private int colCount;
        private int rowCount;
        private string[,] data;
        private FileStream stream;
        private IExcelDataReader reader;
        private DataSet dataset;
        private DataTable table;
        private EnumerableRowCollection<DataRow> enumerable;
        private IEnumerator<DataRow> enumerator;
        private int currentRow;

        public EDRExcelWrapper(string str)
        {
            filename = str;
            // before open file because also opens file
            CalcTableSize();
            if (!File.Exists(str)) throw new FileNotFoundException(str);
            try
            {
                stream = File.Open(filename, FileMode.Open, FileAccess.Read);
                reader = ExcelReaderFactory.CreateReader(stream);
                dataset = reader.AsDataSet();
                table = dataset.Tables[0];
                enumerable = table.AsEnumerable();
                enumerator = enumerable.GetEnumerator();
                enumerator.MoveNext();
            }
            finally
            {
                stream.Close();
                stream = null;
                reader = null;
            }
            currentRow = 0;
            data = new string[rowCount, colCount];
        }

        public string GetFilename()
        {
            return filename;
        }

        public void CalcTableSize()
        {
            using (var stream = File.Open(filename, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet();
                    var table = result.Tables[0];
                    rowCount = table.Rows.Count;
                    colCount = table.Columns.Count;
                }
            }
        }

        public string[] GetRow(int c)
        {
            if (c >= rowCount)
                return null;
            if (data[c, 0] == null)
            {
                while (c != currentRow)
                {
                    if (++currentRow >= rowCount)
                    {
                        currentRow = 0;
                        enumerator = enumerable.GetEnumerator();
#if DEBUG
                        if (enumerator.Current[0].ToString() != table.Rows[0].ToString())
                            throw new Exception("cant go to begin");
#endif
                    }
                    else
                    {
                        enumerator.MoveNext();
                    }
                    if (data[currentRow, 0] == null)
                    {
                        SetDataByDataRow(currentRow, enumerator.Current);
                    }
                }
            }
            return GetDataStringArr(c);
        }

        private void SetDataByDataRow(int r, DataRow row)
        {
#if DEBUG
            if (row == null) throw new ArgumentNullException(r.ToString());
            if (row[0] == null) throw new ArgumentNullException(r.ToString());
#endif
            for (int cnt = 0; cnt < colCount; cnt++)
            {
                data[r, cnt] = row[cnt]?.ToString();
            }
        }

        private string[] GetDataStringArr(int r)
        {
            if (data[r, 0] == null) return null;
            string[] ret;
            ret = new string[colCount];
            for (int cnt = 0; cnt < colCount; cnt++)
            {
                ret[cnt] = data[r, cnt];
            }
            return ret;
        }

        private string[] GetCol(DataRow row)
        {
            string[] ret = new string[colCount];
            for (int cnt = 0; cnt < colCount; cnt++)
            {
                ret[cnt] = (row[cnt]).ToString();
            }
            return ret;
        }

        public List<string[]> SearchRow(string searchStr)
        {
            searchStr = searchStr.ToUpper();
            List<string[]> ret = new List<string[]>();
            using (var stream = File.Open(filename, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet();
                    var table = result.Tables[0];
                    foreach (DataRow row in result.Tables[0].Rows)
                    {
                        string val = (row[0]).ToString();
                        if (val.Contains(searchStr))
                        {
                            ret.Add(GetCol(row));
                        }
                    }
                }
            }
            return ret;
        }

        public void SetFilename(string str)
        {
            filename = str;
        }

        public int GetColumnCount()
        {
            return colCount;
        }

        public int GetRowCount()
        {
            return rowCount;
        }

        public string[] GetProductkey(string article)
        {
            var list = SearchRow(article);
            if (list.Count > 1)
                throw new ArgumentException("article " + article + " has more than 1 result (" + list.Count + ")");
            if (list == null || list.Count == 0)
                return null;
            return list[0];
        }

        public void Dispose()
        {
        }

        public IList<string[]> SearchProductkey(string article)
        {
            return SearchRow(article);
        }
    }
}

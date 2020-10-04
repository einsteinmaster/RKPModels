using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;

namespace RKPModels
{
    public class MySQLArticleClient : IArticleDB
    {
        MySqlConnection conn;
        bool close_after_request;
        string connStr;
        string[] colnameArr;

        public MySQLArticleClient(string host,bool close_after_request = true)
        {
            connStr = "server=" + host + ";user=apprkp;database=rkparticles;port=3306;password=c39rBP8XMtu30Nzm";
            if (!close_after_request)
            {
                conn = new MySqlConnection(connStr);
                conn.Open();
            }
            this.close_after_request = close_after_request;
            colnameArr = GetColNames().Split(',');
        }

        public void Dispose()
        {
            if(!close_after_request)
                conn.Close();
        }

        public string[] GetProductkey(string article)
        {
            if (close_after_request)
            {
                conn = new MySqlConnection(connStr);
                conn.Open();
            }
            try
            {
                string sql = "SELECT * FROM articles WHERE articlenum='" + article + "'";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                try
                {
                    if (rdr.Read())
                    {
                        string[] ret = new string[51];
                        for (int cnt = 0; cnt < ret.Length; cnt++)
                        {
                            ret[cnt] = rdr[1 + cnt].ToString();
                        }
                        return ret;
                    }
                    else
                    {
                        return null;
                    }
                }
                finally
                {
                    rdr.Close();
                }
            }
            finally
            {
                if (close_after_request)
                    conn.Close();
            }     
        }

        private static HashAlgorithm sha = SHA256.Create();
        private int GetStringArrHash(string[] arr)
        {
            //using (MemoryStream ms = new MemoryStream())
            //{
            //    foreach (dynamic t in arr)
            //    {
            //        byte[] bytes = BitConverter.GetBytes(t);
            //        ms.Write(bytes, 0, bytes.Length);
            //    }
            //    var hash = sha.ComputeHash(ms.ToArray());
            //    int rethash = hash[0];
            //    for(int cnt = 1; cnt < 32; cnt++)
            //    {
            //        rethash += (hash[cnt] << (cnt*8));
            //    }
            //    return rethash;
            //}
            int val = 0xf0f0f0f;
            for (int cnt = 0; cnt < arr.Length; cnt++)
            {
                val ^= unchecked(arr[cnt].GetHashCode());
            }
            return val;
        }

        public void ImportFromFile(string filename)
        {
            if (close_after_request)
            {
                conn = new MySqlConnection(connStr);
                conn.Open();
            }
            try
            {
                MySqlCommand cmd = new MySqlCommand("DELETE FROM articles", conn);
                cmd.ExecuteNonQuery();

                EDRExcelWrapper excel = new EDRExcelWrapper(filename);

                int row = 49;
                string[] productkey = excel.GetRow(row);
                while(productkey != null)
                {
                    // if not a valid entry
                    if(String.IsNullOrWhiteSpace(productkey[0]) ||
                        String.IsNullOrWhiteSpace(productkey[7]) ||
                        String.IsNullOrWhiteSpace(productkey[8]) ||
                        String.IsNullOrWhiteSpace(productkey[9]) ||
                        String.IsNullOrWhiteSpace(productkey[10]) ||
                        String.IsNullOrWhiteSpace(productkey[11]) ||
                        String.IsNullOrWhiteSpace(productkey[12]) ||
                        String.IsNullOrWhiteSpace(productkey[13]) ||
                        String.IsNullOrWhiteSpace(productkey[14]) ||
                        String.IsNullOrWhiteSpace(productkey[15]) ||
                        String.IsNullOrWhiteSpace(productkey[16]) ||
                        String.IsNullOrWhiteSpace(productkey[17]) ||
                        String.IsNullOrWhiteSpace(productkey[18])
                        )
                    {
                        Debug.WriteLine("Illegal Entry row:" + row);
                    }
                    else
                    {
                        // reset all empty columns to 0 to not cause trouble with sql
                        int minimalColCount = 10;
                        for (int cnt = 0; cnt < minimalColCount; cnt++)
                            if (String.IsNullOrWhiteSpace(productkey[cnt]))
                                productkey[cnt] = "0";

                        // compute number of lines we want to insert
                        // we will have always one additional column
                        // insertColCount refrences the count in destination table
                        int insertColCount = minimalColCount;
                        while (!String.IsNullOrWhiteSpace(productkey[insertColCount++])) ;

                        // insert colnames
                        string colnamestr = colnameArr[0];
                        for (int cnt = 1; cnt < insertColCount; cnt++) colnamestr += "," + colnameArr[cnt];
                        string sql = "INSERT INTO articles (" + colnamestr + ") VALUES (";

                        // insert values
                        sql += GetStringArrHash(productkey);
                        for (int cnt = 1; cnt < insertColCount; cnt++)
                        {
                            string val = "'" + productkey[cnt - 1].Replace(',', '.') + "'";
                            sql += "," + val;
                        }

                        sql += ")";

                        //Debug.WriteLine(sql);

                        cmd = new MySqlCommand(sql, conn);
                        cmd.ExecuteNonQuery();
                    }
                    productkey = excel.GetRow(++row);
                }
            }
            finally
            {
                if (close_after_request)
                    conn.Close();
            }            
        }

        public string GetColNames()
        {
            if (close_after_request)
            {
                conn = new MySqlConnection(connStr);
                conn.Open();
            }
            try
            {
                string sql = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = N'articles'";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                
                string ret = "";
                for (int cnt = 0; cnt < 52; cnt++)
                {
                    rdr.Read();
                    if (cnt > 0)
                        ret += ",";
                    ret += rdr[0];
                }
                rdr.Close();
                return ret;
            }
            finally
            {
                if (close_after_request)
                    conn.Close();
            }
        }

        public IList<string[]> SearchProductkey(string article)
        {
            if (close_after_request)
            {
                conn = new MySqlConnection(connStr);
                conn.Open();
            }
            try
            {
                string sql = "SELECT * FROM articles WHERE (articlenum REGEXP '" + article + "')";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                List<string[]> results = new List<string[]>();
                try
                {
                    while (rdr.Read())
                    {
                        string[] ret = new string[51];
                        for (int cnt = 0; cnt < ret.Length; cnt++)
                        {
                            ret[cnt] = rdr[1 + cnt].ToString();
                        }
                        results.Add(ret);
                    }
                    return results;
                }
                finally
                {
                    rdr.Close();
                }
            }
            finally
            {
                if (close_after_request)
                    conn.Close();
            }
        }
    }
}

using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelSearchBox
{
    public class DataSheetCreator
    {
        private string filename;
        Dictionary<string, string>[] descDict;
        string[] propertys;
        public DataSheetCreator(string filename)
        {
            this.filename = filename;
            descDict = new Dictionary<string, string>[13];
            for (int cnt = 0; cnt < descDict.Length; cnt++) descDict[cnt] = new Dictionary<string, string>();
            propertys = new string[13];
            CreateDictionary();
        }
        public string GetFilename()
        {
            return filename;
        }
        public void SetFilename(string str)
        {
            filename = str;
        }
        private void CreateDictionary()
        {
            using (var stream = File.Open(filename, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet();
                    var table = result.Tables[6];
                    if (!table.Rows[0][0].ToString().Contains("Pos"))
                    {
                        throw new Exception("wrong table");
                    }
                    int key = -1;
                    string subtype = "";
                    foreach (DataRow row in table.Rows)
                    {
                        string val = (row[0]).ToString();
                        int num = -1;
                        if (int.TryParse(val, out num))
                        {
                            key = num;
                            propertys[key] = row[2].ToString();
                            subtype = "";
                        }
                        else if (val.StartsWith("-"))
                        {
                            key = -1;
                        }
                        else if (key > 0)
                        {
                            if (!string.IsNullOrWhiteSpace(row[1].ToString()))
                            {
                                string key_name = subtype + row[1].ToString();

                                if (descDict[key].ContainsKey(key_name))
                                {
                                    throw new Exception("double entrys in description at key " + key + " with value " + row[1].ToString());
                                }

                                switch (key)
                                {
                                    case 3:
                                        break;
                                    default:
                                        descDict[key].Add(key_name, row[2].ToString());
                                        break;
                                }
                            }
                            else
                            {
                                // subtype description
                                string potential_subkey = row[2].ToString().Trim();
                                switch (key)
                                {
                                    case 6:
                                        if (potential_subkey.Contains("ZFL, ZFS, ZFG"))
                                        {
                                            subtype = "Z_";
                                        }
                                        else if (potential_subkey.Contains("AZP"))
                                        {
                                            subtype = "AZP_";
                                        }
                                        break;
                                    case 7:
                                        if (potential_subkey.Contains("Pumpengröße 5-31cm3"))
                                        {
                                            subtype = "ZA_";
                                        }
                                        else if (potential_subkey.Contains("Pumpengröße 32-50cm3"))
                                        {
                                            subtype = "ZB_";
                                        }
                                        else if (potential_subkey.Contains("Pumpengröße 19cm3"))
                                        {
                                            subtype = "019_";
                                        }
                                        else if (potential_subkey.Contains("Pumpengröße 32-45cm3"))
                                        {
                                            subtype = "032_";
                                        }
                                        else if (potential_subkey.Contains("Pumpengröße 63cm3"))
                                        {
                                            subtype = "063_";
                                        }
                                        else if (potential_subkey.Contains("Pumpengröße 80cm3"))
                                        {
                                            subtype = "080_";
                                        }
                                        else if (potential_subkey.Contains("Pumpengröße 100cm3"))
                                        {
                                            subtype = "100_";
                                        }
                                        else if (potential_subkey.Contains("Pumpengröße 140cm3"))
                                        {
                                            subtype = "140_";
                                        }
                                        break;
                                    case 12:
                                        if (potential_subkey.Contains("Leistungsregler S1, S2, S3"))
                                        {
                                            subtype = "S_";
                                        }
                                        else if (potential_subkey.Contains("Digitalregler D1…D8"))
                                        {
                                            subtype = "D_";
                                        }
                                        else if (potential_subkey.Contains("Doppelzahnradpumpe"))
                                        {
                                            subtype = "Z_";
                                        }
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                    }
                }
            }
        }
        public string CreateDataSheet(string[] obj)
        {
            var csv = new StringBuilder();
            csv.AppendLine("Pumpennummer:;" + obj[0]);
            csv.AppendLine("Pumpenschlüssel:;" + obj[1]);
            for (int cnt = 0; (cnt + 7) < obj.Length; cnt++)
            {
                int objIndex = cnt + 7;
                int dictIndex = (cnt + 1);
                if (dictIndex > 12)
                {
                    dictIndex = ((cnt - 12) % 8) + 5;

                    if (dictIndex == 5)
                    {
                        // no more modules (double pump)
                        if (string.IsNullOrWhiteSpace(obj[objIndex]))
                            break;
                        else
                        {
                            // info about what stage
                            csv.AppendLine("Pumpenstufe " + (((cnt - 12) / 8) + 2));
                        }
                    }
                }

                string subtypekey = "";

                // get subtypes for cases 6,7,12
                switch (dictIndex)
                {
                    case 6:
                        if (obj[objIndex - 1] == "RKP")
                        {

                        }
                        else if (obj[objIndex - 1].StartsWith("Z"))
                        {
                            subtypekey = "Z_";
                        }
                        else if (obj[objIndex - 1] == "AZP")
                        {
                            subtypekey = "AZP_";
                        }
                        break;
                    case 7:
                        int pumpsize = -1;
                        if(!int.TryParse(obj[objIndex-1],out pumpsize)){
                            throw new Exception("pumpsize is not valid <" + obj[objIndex - 1] + "> at key position <" + (objIndex - 1) + ">");
                        }
                        if (obj[objIndex - 2] == "RKP" || obj[objIndex - 2] == "FRP")
                        {
                            if (pumpsize <= 19)
                            {
                                subtypekey = "019_";
                            }
                            else if (pumpsize > 19 && pumpsize <= 45)
                            {
                                subtypekey = "032_";
                            }
                            else if (pumpsize > 45 && pumpsize <= 63)
                            {
                                subtypekey = "063_";
                            }
                            else if (pumpsize > 63 && pumpsize <= 80)
                            {
                                subtypekey = "080_";
                            }
                            else if (pumpsize > 80 && pumpsize <= 100)
                            {
                                subtypekey = "100_";
                            }
                            else if (pumpsize > 100)
                            {
                                subtypekey = "140_";
                            }
                        }else if(obj[objIndex - 2] == "AZP")
                        {
                            if (pumpsize < 32)
                            {
                                subtypekey = "ZA_";
                            }
                            else if (pumpsize >= 32)
                            {
                                subtypekey = "ZB_";
                            }
                        }
                        break;
                    case 12:
                        if (obj[objIndex - 2].StartsWith("S") || obj[objIndex-2] == "ZK")
                        {
                            subtypekey = "S_";
                        }
                        else if (obj[objIndex - 2].StartsWith("D"))
                        {
                            subtypekey = "D_";
                        }
                        else if (obj[objIndex - 7] == "AZP" || obj[objIndex-7].StartsWith("Z"))
                        {
                            subtypekey = "Z_";
                        }
                        break;
                    default:
                        break;
                }

                string fulldictKey = subtypekey + obj[objIndex];
                string keyValue = (descDict[dictIndex].ContainsKey(fulldictKey) ? descDict[dictIndex][fulldictKey] : "");
                // rpm case
                if (dictIndex == 3)
                {
                    keyValue = "n = " + obj[objIndex] + "00 min-1";
                }
                if(dictIndex == 12 && string.IsNullOrWhiteSpace(keyValue))
                {
                    keyValue = "keine";
                }

                // csv restricted characters
                keyValue = keyValue.Replace('\n', ',');
                keyValue = keyValue.Replace('\r', ',');
                keyValue = keyValue.Replace(';', ',');

                csv.AppendLine(propertys[dictIndex] + ';' + obj[objIndex] + ';' + keyValue);
            }
            return csv.ToString();
        }
    }
}

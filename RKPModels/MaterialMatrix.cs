using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RKPModels
{
    public class MaterialMatrix
    {
        public const int INACTIVE_ENTRY = int.MinValue;
        public const int SIZE_X = 7;
        public const int SIZE_Y = 41;
        public static Dictionary<int, string> LabelMatX;
        public static Dictionary<int, string> LabelMatY;
        private int[,] Data;
        static MaterialMatrix()
        {
            LabelMatX = new Dictionary<int, string>();
            LabelMatX.Add(0, "19");
            LabelMatX.Add(1, "32");
            LabelMatX.Add(2, "45");
            LabelMatX.Add(3, "63");
            LabelMatX.Add(4, "80");
            LabelMatX.Add(5, "100");
            LabelMatX.Add(6, "140");
            LabelMatY = new Dictionary<int, string>();
            LabelMatY.Add(0, "Solo");
            LabelMatY.Add(1, "DS1");
            LabelMatY.Add(2, "HD Solo");
            LabelMatY.Add(3, "HD DS1");
            LabelMatY.Add(4, "Hubfix B1");
            LabelMatY.Add(5, "links Solo");
            LabelMatY.Add(6, "links DS1");
            LabelMatY.Add(7, "A1");
            LabelMatY.Add(8, "A1 DS1");
            LabelMatY.Add(9, "B1 DS1");
            LabelMatY.Add(10, "C3");
            LabelMatY.Add(11, "C3 DS1");
            LabelMatY.Add(12, "D3 DS1");
            LabelMatY.Add(13, "A7");
            LabelMatY.Add(14, "A7 DS1");
            LabelMatY.Add(15, "B7 DS1");
            LabelMatY.Add(16, "XX");
            LabelMatY.Add(17, "XX DS1");
            LabelMatY.Add(18, "A7 HFA");
            LabelMatY.Add(19, "B1 HFC DS1");
            LabelMatY.Add(20, "A1 Solo HFC");
            LabelMatY.Add(21, "F2");
            LabelMatY.Add(22, "F1");
            LabelMatY.Add(23, "H1");
            LabelMatY.Add(24, "H1 Flow");
            LabelMatY.Add(25, "H2");
            LabelMatY.Add(26, "J1");
            LabelMatY.Add(27, "R1");
            LabelMatY.Add(28, "U1");
            LabelMatY.Add(29, "G2");
            LabelMatY.Add(30, "C1");
            LabelMatY.Add(31, "E1");
            LabelMatY.Add(32, "E2");
            LabelMatY.Add(33, "D1");
            LabelMatY.Add(34, "D2");
            LabelMatY.Add(35, "Dx");
            LabelMatY.Add(36, "Y");
            LabelMatY.Add(37, "B2");
            LabelMatY.Add(38, "S2");
            LabelMatY.Add(39, "S3");
            LabelMatY.Add(40, "S1");
        }

        public MaterialMatrix Clone()
        {
            MaterialMatrix m2 = new MaterialMatrix();
            m2.Data = (int[,])this.Data.Clone();
            return m2;
        }

        public MaterialMatrix()
        {
            Data = new int[SIZE_X, SIZE_Y];
            for (int cnt = 7; cnt <= 20; cnt++)
                Data[2, cnt] = INACTIVE_ENTRY;
            for (int cnt = 7; cnt <= 20; cnt++)
                Data[4, cnt] = INACTIVE_ENTRY;
            for (int cnt = 7; cnt <= 20; cnt++)
                Data[5, cnt] = INACTIVE_ENTRY;

            for (int cnt = 21; cnt <= 29; cnt++)
                Data[1, cnt] = INACTIVE_ENTRY;
            for (int cnt = 21; cnt <= 30; cnt++)
                Data[2, cnt] = INACTIVE_ENTRY;
            for (int cnt = 21; cnt <= 30; cnt++)
                Data[4, cnt] = INACTIVE_ENTRY;
            for (int cnt = 21; cnt <= 37; cnt++)
                Data[5, cnt] = INACTIVE_ENTRY;

            for (int cnt = 36; cnt <= 37; cnt++)
                Data[1, cnt] = INACTIVE_ENTRY;
            for (int cnt = 36; cnt <= 37; cnt++)
                Data[2, cnt] = INACTIVE_ENTRY;
            for (int cnt = 36; cnt <= 37; cnt++)
                Data[4, cnt] = INACTIVE_ENTRY;
            for (int cnt = 36; cnt <= 37; cnt++)
                Data[5, cnt] = INACTIVE_ENTRY;

            // irrelevant entrys for this application
            for (int cnt = 0; cnt <= 6; cnt++)
                Data[cnt, 4] = INACTIVE_ENTRY;
            for (int cnt = 0; cnt <= 6; cnt++)
                Data[cnt, 24] = INACTIVE_ENTRY;
            for (int cnt = 0; cnt <= 6; cnt++)
                Data[cnt, 35] = INACTIVE_ENTRY;
            for (int cnt = 0; cnt <= 6; cnt++)
                Data[cnt, 36] = INACTIVE_ENTRY;
        }
        public MaterialMatrix(int val) : this()
        {
            for (int x = 0; x < SIZE_X; x++)
            {
                for (int y = 0; y < SIZE_Y; y++)
                {
                    if (this.Data[x, y] != INACTIVE_ENTRY)
                        this.Data[x, y] = val;
                }
            }
        }
        public static MaterialMatrix operator +(MaterialMatrix m1, MaterialMatrix m2)
        {
            MaterialMatrix m3 = new MaterialMatrix();
            for (int x = 0; x < SIZE_X; x++)
            {
                for (int y = 0; y < SIZE_Y; y++)
                {
                    if (m3.Data[x, y] != INACTIVE_ENTRY)
                        m3.Data[x, y] = m1.Data[x, y] + m2.Data[x, y];
                }
            }
            return m3;
        }
        public static MaterialMatrix operator -(MaterialMatrix m1, MaterialMatrix m2)
        {
            MaterialMatrix m3 = new MaterialMatrix();
            for (int x = 0; x < SIZE_X; x++)
            {
                for (int y = 0; y < SIZE_Y; y++)
                {
                    if (m3.Data[x, y] != INACTIVE_ENTRY)
                        m3.Data[x, y] = m1.Data[x, y] - m2.Data[x, y];
                }
            }
            return m3;
        }

        public bool IsNotNegativ()
        {
            for (int x = 0; x < SIZE_X; x++)
            {
                for (int y = 0; y < SIZE_Y; y++)
                {
                    if (this.Data[x, y] != INACTIVE_ENTRY && this.Data[x, y] < 0) return false;
                }
            }
            return true;
        }

        public int this[int x, int y]
        {
            get
            {
                int tmp_x = x;
                while (Data[tmp_x, y] == INACTIVE_ENTRY)
                {
                    if (tmp_x == 0 && Data[tmp_x, y] == INACTIVE_ENTRY)
                        throw new Exception("try to read inactive entry (" + x + "," + y + ")");
                    tmp_x--;
                }
                return Data[tmp_x, y];
            }
            set
            {
                // int.MinValue is reserved for not active (use right value)
                if (value == INACTIVE_ENTRY) throw new Exception("value overflow");
                int tmp_x = x;
                while (Data[tmp_x, y] == INACTIVE_ENTRY)
                {
                    if (tmp_x == 0 && Data[tmp_x, y] == INACTIVE_ENTRY)
                        throw new Exception("try to write inactive entry (" + x + "," + y + ")");
                    tmp_x--;
                }
                Data[tmp_x, y] = value;
            }
        }

        public bool IsInactive(int x, int y)
        {
            return (Data[x, y] == INACTIVE_ENTRY);
        }

        public int CountNonNegativeEntrys()
        {
            int sum = 0;
            for (int x = 0; x < SIZE_X; x++)
            {
                for (int y = 0; y < SIZE_Y; y++)
                {
                    if (this.Data[x, y] != INACTIVE_ENTRY && this.Data[x, y] > 0) sum++;
                }
            }
            return sum;
        }

        public string Serialize()
        {
            StringBuilder str = new StringBuilder();

            // print version for portability
            str.AppendLine("MaterialMatrix Serialize v1.0");

            str.Append(';');
            for (int x = 0; x < SIZE_X; x++)
            {
                str.Append(LabelMatX[x]);
                str.Append(';');
            }
            str.AppendLine();

            for (int y = 0; y < SIZE_Y; y++)
            {
                str.Append(LabelMatY[y]);
                str.Append(';');
                for (int x = 0; x < SIZE_X; x++)
                {
                    if (IsInactive(x, y))
                    {
                        str.Append("NaN");
                    }
                    else
                    {
                        str.Append(this[x, y]);
                    }
                    str.Append(';');
                }
                str.AppendLine();
            }

            return str.ToString();
        }

        public void SaveToFile(string path)
        {
            File.WriteAllText(path, Serialize());
        }

        public static MaterialMatrix ReadFromFile(string path)
        {
            MaterialMatrix mat = new MaterialMatrix();
            using (TextFieldParser parser = new TextFieldParser(path))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(";");
                if (!parser.EndOfData) parser.ReadFields(); // name/ version of serialize
                if (!parser.EndOfData) parser.ReadFields(); // label of x
                for (int y = 0; y < SIZE_Y && !parser.EndOfData; y++)
                {
                    //Processing row
                    string[] fields = parser.ReadFields();
                    for (int x = 0; x < SIZE_X && x < fields.Length; x++)
                    {
                        int val = 0;
                        if (fields[x + 1] == "NaN")
                        {
                            mat.Data[x, y] = INACTIVE_ENTRY;
                        }
                        else if (string.IsNullOrWhiteSpace(fields[x + 1]))
                        {
                            mat.Data[x, y] = 0;
                        }
                        else if (int.TryParse(fields[x + 1], out val))
                        {
                            mat.Data[x, y] = val;
                        }
                        else
                        {
                            throw new Exception("Illegal character at serialization");
                        }
                    }
                }
            }
            return mat;
        }
    }
}

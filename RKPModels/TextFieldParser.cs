using System;
using System.Collections.Generic;

namespace RKPModels
{
    internal enum FieldType
    {
        Delimited
    }
    internal class TextFieldParser : IDisposable
    {
        string[] lines;
        string delimiter;
        int pos;

        public TextFieldParser(string path)
        {
            lines = System.IO.File.ReadAllLines(path);
            pos = 0;
        }

        public FieldType TextFieldType { get; set; }

        public bool EndOfData 
        { 
            get
            {
                return pos >= lines.Length;
            }
        }

        public void Dispose()
        {
        }

        internal void SetDelimiters(string v)
        {
            delimiter = v;
        }

        internal string[] ReadFields()
        {
            Stack<string> values = new Stack<string>();
            string tmp = "";
            for(int cnt = 0; cnt < lines[pos].Length; cnt++)
            {
                char next = lines[pos][cnt];
                if(next == delimiter[0])
                {
                    values.Push(tmp);
                    tmp = "";
                }else if(next == '\n')
                {
                    break;
                }else if(next == '\r')
                {
                    break;
                }
                else
                {
                    tmp += next;
                }
            }
            if (!String.IsNullOrEmpty(tmp))
                values.Push(tmp);
            pos++;
            string[] ret = new string[values.Count];
            for (int cnt = 0; cnt < ret.Length; cnt++)
            {
                ret[ret.Length - 1 - cnt] = values.Pop();
            }
            return ret;
        }
    }
}
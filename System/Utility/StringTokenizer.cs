using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace VorliasEngine2D.System.Utility
{
    public class StringTokenizer
    {
        string source;
        string delim = " ";
        int position = 0;
        int length = 0;
        StringReader reader;

        /// <summary>
        /// The token position
        /// </summary>
        public int Position
        {
            get
            {
                return position;
            }
        }

        /// <summary>
        /// The delimiter
        /// </summary>
        public string Delimiter
        {
            get
            {
                return delim;
            }
            set
            {
                delim = value;
            }
        }

        /// <summary>
        /// Gets the tokens based on a predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public string[] Where(Func<string, bool> predicate)
        {
            //List<string> results = new List<string>();
            //foreach (string token in Lines)
            //{
            //   if (predicate.Invoke(token))
            //        results.Add(token);
            //}

            //return results.ToArray();
            return null;
        }

        /// <summary>
        /// Resets the token position
        /// </summary>
        public void Reset()
        {
            position = 0;
        }

        public string Peek()
        {
            char next;
            string result = "";
            do
            {
                next = (char)reader.Peek();
                if (next != '\n' && next != ' ' && next != '\r' && next != 0xFFFF)
                {
                    result += next;
                }
                else
                    break;
            }
            while (true);

            return result.Trim();
        }

        /// <summary>
        /// Gets the next token
        /// </summary>
        /// <returns>The next token</returns>
        public string Read()
        {
            char next;
            string result = "";
            do
            {
                next = (char) reader.Peek();
                if (next != '\n' && next != ' ' && next != '\r' && next != 0xFFFF)
                {
                    result += (char)reader.Read();
                }
                else
                    break;
            }
            while (true);

            if (next == '\n' || next == ' ' || next == '\r')
                reader.Read(); // Remove whitespace

            return result.Trim();
        }

        public string ReadLine()
        {
            return reader.ReadLine();
        }

        public float ReadFloat()
        {
            return float.Parse(Read());
        }

        public double ReadDouble()
        {
            return double.Parse(Read());
        }

        public int ReadInt()
        {
            return int.Parse(Read());
        }

        public string[] Lines
        {
            get
            {
                return new string[] { };
            }
        }

        public StringTokenizer(string source)
        {
            this.source = source;
            length = source.Length;
            //stream = new StreamReader(new MemoryStream(Encoding.ASCII.GetBytes(source))); 
            reader = new StringReader(source);
        }
    }
}

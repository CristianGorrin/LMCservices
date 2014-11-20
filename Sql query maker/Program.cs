using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace Sql_query_maker
{
    class Program
    {
        static void Main(string[] args)
        {
            var postNumbers = GetPostNumbers(@"C:\Users\Cristian C. Gorrin\OneDrive\Visual Studio\C#\LMCservices\TestingValuesV1.sql");
            var test = ReplaceAtRandom("postNo.placeholder", postNumbers, @"D:\test.sql");
            SaveFill(test, @"D:\new.sql");
        }

        static string[] ReadFill(string path)
        {
            var text = new List<string>();

            using (var reader = new System.IO.StreamReader(path, System.Text.Encoding.Default))
            {
                while (!reader.EndOfStream)
                {
                    text.Add(reader.ReadLine());
                }

                reader.Close();
            } 

            return text.ToArray();
        }

        static void SaveFill(string[] text, string path)
        {
            System.IO.StreamWriter writer = new StreamWriter(path);

            foreach (string line in text)
            {
                writer.Write(line + Environment.NewLine);
                writer.Flush();
            }
        }

        static string[] ReplaceAtRandom(string replace, string[] words, string path)
        {
            var toRetrun = new List<string>();
            string[] text = ReadFill(path);
            var replaceAt = new List<int[]>();
            int atLine = 0;

            foreach (string line in text)
            {
                for (int atChar = 0; atChar < line.Length; atChar++)
                {
                    if (line[atChar] == replace[0])
                    {
                        string temp = string.Empty;

                        for (int i = 0; i < replace.Length; i++)
                        {
                            try
                            {
                                temp += line[atChar + i];
                            }
                            catch (Exception)
                            {
                                break;
                            }
                        }

                        if (temp == replace)
                        {
                            replaceAt.Add(new int[] { atLine, atChar });
                        }
                    }
                }

                atLine++;
            }

            atLine = 0;
            foreach (var item in replaceAt)
            {
                string replaceTemp = string.Empty;

                for (int i = 0; i < item[1]; i++)
                {
                    replaceTemp += text[item[0]][i];
                }

                replaceTemp += words[new Random(DateTime.Now.Millisecond).Next(0, words.Length - 1)];

                for (int i = item[1] + replace.Length; i < text[atLine].Length; i++)
                {
                    replaceTemp += text[atLine][i];
                }

                while (atLine <= item[0])
                {
                    toRetrun.Add(text[atLine]);
                    atLine++;
                }

                toRetrun.Add(replaceTemp);
                atLine++;
            }
            
            return toRetrun.ToArray();
        }

        static string[] GetPostNumbers(string path)
        {
            var toRetrun = new List<string>();

            var postNumbers = ReadFill(path);
            foreach (string line in postNumbers)
            {
                string temp = string.Empty;

                for (int i = 0; i <= 28; i++)
                {
                    try
                    {
                        temp += line[i];
                    }
                    catch (Exception)
                    {
                        break;
                    }
                }

                if (temp == "Insert into [dbo].[tblPostNo]")
                {
                    temp = string.Empty;
                    for (int i = 37; i < 41; i++)
                    {
                        temp += line[i];
                    }

                    toRetrun.Add(temp);
                }
            }

            return toRetrun.ToArray();
        }
    }
}

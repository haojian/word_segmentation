using System;
using System.Collections.Generic;
using System.Text;
using System.IO;


namespace CorpusProcess
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("{0}", Foo());
            Console.ReadLine();
        }

        public static string Foo()
        {
            string path = @"D:\2.txt";
            StringBuilder outBuffer = new StringBuilder();

            try
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                }

                Encoding fileEncoding = Encoding.GetEncoding("GB2312");
                using (StreamWriter sw = new StreamWriter(path, false, fileEncoding))
                {
                    sw.WriteLine("我");
                    sw.WriteLine("是");
                    sw.WriteLine("中国人");
                }

                using (StreamReader sr = new StreamReader(path, fileEncoding))
                {
                    while (sr.Peek() >= 0)
                    {
                        // 曾经是这样读取的：将 int 转成 byte[]，然后再解码，然后尝试了N多编码，
                        //费了九牛二虎之力，才得到正确结果，才发现无论使用何种Encoding打开Stream，返回的总是 Unicode 编码。
                        // outBuffer.Append(System.Text.Encoding.Unicode.GetString(BitConverter.GetBytes(sr.Read())));
                        // 
                        // 今天才在MSDN中发现：可以直接将 int 强制转换成 char，并且可以正确的解码。
                        // 因为，上面提到 Reader 总是返回 Unicode 编码，而 char 在 .NET 内部正是用的 Unicode 编码。
                        // 郁闷！
                        outBuffer.Append("/");
                        outBuffer.Append((char)sr.Read());
                    }
                }
            }
            catch (Exception ex)
            {
                outBuffer.AppendFormat("The process failed: {0}", ex.ToString()).AppendLine();
            }
            //
            return outBuffer.ToString();
        }
    }
}

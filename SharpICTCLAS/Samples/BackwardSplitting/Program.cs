using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using SharpICTCLAS;

namespace BackwardSplitting
{
    static class Program
    {
        
        static void Main()
        {
            Console.Write("操作进行中......");
            Foo();
            Console.Write("\n按下回车键退出......");
            Console.ReadLine();
        }
        public static string Foo()
        {
            test abc = new test();
            string biaodian = @"—（）、，！“”《》『』";

            string path = @"D:\txt2.txt";
            StringBuilder outBuffer = new StringBuilder();
            try
            {
                
                Encoding fileEncoding = Encoding.GetEncoding("GB2312");
                using (StreamReader sr = new StreamReader(path, fileEncoding))
                {
                    while (sr.Peek() >= 0)
                    {
                        char dfg = (char)sr.Read();
                        //string dfg = Convert.ToString((char)sr.Read());
                        if (biaodian.IndexOf( dfg)>=0)
                        {
                            //abc.BackSplitting(outBuffer.ToString());

                            RowFirstDynamicArray<ChainContent> de = abc.GetSegGraph(outBuffer.ToString());
                            //string hyn= abc.ForwardSplitting(de);
                            WriteToTxt( abc.BackSplitting(de));
                            WriteToTxt(Convert.ToString(dfg));
                            outBuffer.Remove(0, outBuffer.Length);
                            continue;
                        }
                        outBuffer.Append(dfg);
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
        public static void WriteToTxt(string txt)
        {
            StreamWriter sw = File.AppendText(@"D:\myText.txt");
            sw.Write(txt);
            sw.Flush();
            sw.Close();
        }
    }
}

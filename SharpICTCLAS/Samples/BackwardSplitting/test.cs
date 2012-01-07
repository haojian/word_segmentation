using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using SharpICTCLAS;

namespace BackwardSplitting
{
    public class test
    {

        public static string DictPath = "D:\\MyDocument\\Desktop\\Research\\ICT\\SharpKeyICTCLAS分词系统Test1\\bin\\Data" + Path.DirectorySeparatorChar;
        public static string coreDictFile = DictPath + "coreDict.dct";
        public static string biDictFile = DictPath + "BigramDict.dct";
        public static string contextFile = DictPath + "nr.ctx";
        public static string nrFile = DictPath + "tr.dct";


        private int nKind=1;
        private WordSegment wordSegment;

        /// <summary>
        /// 得到所有可能的分词方案
        /// </summary>
        /// <returns></returns>
        public RowFirstDynamicArray<ChainContent> GetSegGraph(string sSentence)
        {
            WordDictionary coreDict = new WordDictionary();
            if (!coreDict.Load(coreDictFile))
            {
                Console.WriteLine("字典装入错误！");
                return null;
            }

            //string sSentence = @"他说的确实实在";
            sSentence = Predefine.SENTENCE_BEGIN + sSentence + Predefine.SENTENCE_END;

            List<AtomNode> atomSegment = Segment.AtomSegment(sSentence);
            RowFirstDynamicArray<ChainContent> m_segGraph = Segment.GenerateWordNet(atomSegment, coreDict);
            return m_segGraph;
        }

        private void OnSegmentEventHandler(object sender, SegmentEventArgs e)
        {
            
        }

        //逆向最长匹配
        public string BackSplitting(RowFirstDynamicArray<ChainContent> m_segGraph)
        {
      
            string abc = "";
            int nCol = m_segGraph.ColumnCount-1;
            int nRow = m_segGraph.RowCount-1;
            //ChainItem<ChainContent> dfg = m_segGraph.GetElement(m_segGraph.RowCount-1, m_segGraph.ColumnCount-1);
            //List<ChainItem<ChainContent>> ab = new List<ChainItem<ChainContent>>();
            while (nCol > 1)
            {
                for (int i = 0; i <=nRow; i++)
                {
                    if (null != m_segGraph.GetElement(i, nCol))
                    {
                        if (abc == "") { abc = m_segGraph.GetElement(i, nCol).Content.sWord; }
                        else{
                        abc = m_segGraph.GetElement(i, nCol).Content.sWord + "/" + abc;
                        }nCol = i;
                        break;
                    }
                }
            }
            return abc;
        }
        //正向最长匹配
        public string ForwardSplitting(RowFirstDynamicArray<ChainContent> m_segGraph)
        {
            string abc = "";
            // =GetSegGraph();
            int currcol = 0;
            ChainItem<ChainContent> dfg= m_segGraph.GetElement(0, 1);
            ChainItem<ChainContent> aa = dfg.next;
            while (null != aa.next)
            {
                if (aa.next.row == aa.row)
                {
                    currcol = aa.next.col;
                    aa = aa.next;
                }
                else
                {
                    abc += aa.Content.sWord  ;
                    currcol = aa.col;
                    aa = m_segGraph.GetFirstElementOfRow(currcol);
                    break;
                }
            }

            while (null != aa.next) 
            {
                if (aa.next.row == aa.row)
                {
                    currcol = aa.next.col;
                    aa = aa.next;
                }
                else 
                {
                    abc += "/"+aa.Content.sWord ;
                    currcol = aa.col;
                    aa = m_segGraph.GetFirstElementOfRow(currcol);
                }

            }
            return abc;
        }
        //双向最长匹配
        public void dualSplitting()
        {
           // RowFirstDynamicArray<ChainContent> m_segGraph = GetSegGraph();
            
        }

    }
}

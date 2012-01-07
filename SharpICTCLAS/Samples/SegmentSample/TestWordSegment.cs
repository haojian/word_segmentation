using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using SharpICTCLAS;

public class WordSegmentSample
{
   private int nKind = 1;  //��NShortPath�������������������з�ʱ�ֳɼ��ֽ��
   private WordSegment wordSegment;

   //=======================================================
   // ���캯������û��ָ��nKind������£�nKind ȡ 1
   //=======================================================
   public WordSegmentSample(string dictPath) : this(dictPath, 1) { }

   //=======================================================
   // ���캯��
   //=======================================================
   public WordSegmentSample(string dictPath, int nKind)
   {
      this.nKind = nKind;
      this.wordSegment = new WordSegment();
      //wordSegment.PersonRecognition = false;
      //wordSegment.PlaceRecognition = false;
      //wordSegment.TransPersonRecognition = false;

      //---------- ���ķִʹ����е��¼� ----------
      wordSegment.OnSegmentEvent += new SegmentEventHandler(this.OnSegmentEventHandler);
      wordSegment.InitWordSegment(dictPath);
   }

   //=======================================================
   // ��ʼ�ִ�
   //=======================================================
   public List<WordResult[]> Segment(string sentence)
   {
      return wordSegment.Segment(sentence, nKind);
   }
   
   //=======================================================
   // ����ִʹ�����ÿһ�����м���
   //=======================================================
   private void OnSegmentEventHandler(object sender, SegmentEventArgs e)
   {
      switch (e.Stage)
      {
         case SegmentStage.BeginSegment:
            //Console.WriteLine("\r\n==== ԭʼ���ӣ�\r\n");
            //Console.WriteLine(e.Info + "\r\n");
            //WriteToTxt(e.Info + "\r\n");
            break;
         case SegmentStage.AtomSegment:
            //Console.WriteLine("\r\n==== ԭ���з֣�\r\n");
            Console.WriteLine(e.Info);
            //WriteToTxt(e.Info);
            break;
         case SegmentStage.GenSegGraph:
            //Console.WriteLine("\r\n==== ���� segGraph��\r\n");
            //Console.WriteLine(e.Info);
            //WriteToTxt(e.Info);
            break;
         case SegmentStage.GenBiSegGraph:
            //Console.WriteLine("\r\n==== ���� biSegGraph��\r\n");
            //Console.WriteLine(e.Info);
            //WriteToTxt(e.Info);
            break;
         case SegmentStage.NShortPath:
            //Console.WriteLine("\r\n==== NShortPath �����зֵĵ��� N �������\r\n");
            //Console.WriteLine(e.Info);
            //WriteToTxt(e.Info);
            break;
         case SegmentStage.BeforeOptimize:
            //Console.WriteLine("\r\n==== �������֡����ںϲ��Ȳ��Դ������ N �������\r\n");
            //Console.WriteLine(e.Info);
            //WriteToTxt(e.Info);
            break;
         case SegmentStage.OptimumSegment:
            //Console.WriteLine("\r\n==== �� N ������鲢��OptimumSegment��\r\n");
            //Console.WriteLine(e.Info);
            //WriteToTxt(e.Info);
            break;
         case SegmentStage.PersonAndPlaceRecognition:
            //Console.WriteLine("\r\n==== ��������������������Լ�������ʶ��\r\n");
            //Console.WriteLine(e.Info);
            //WriteToTxt(e.Info);
            break;
         case SegmentStage.BiOptimumSegment:
            //Console.WriteLine("\r\n==== �Լ����������������OptimumSegment����BiOptimumSegment��\r\n");
            //Console.WriteLine(e.Info); 
            //WriteToTxt(e.Info);
            break;
         case SegmentStage.FinishSegment:
            Console.WriteLine("\r\n==== ����ʶ������\r\n");
            Console.WriteLine(e.Info);
            WriteToTxt(e.Info);
            break;
          case SegmentStage.BcakwardOptimize:
            //Console.WriteLine("\r\n==== ����ƥ��ϵ����˺�\r\n");
            //Console.WriteLine(e.Info);
            //WriteToTxt(e.Info);
            break;

      }
   }

   public static void WriteToTxt(string txt) 
   {
       StreamWriter sw = File.AppendText( @"D:\myText.txt");
       sw.WriteLine(txt);
       sw.Flush();
       sw.Close(); 
   }
}
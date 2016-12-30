using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

namespace CSharp_Orneklerim.Threading_Event
{
    /// <summary>
    /// public delegate void ParameterizedThreadStart(object obj);
    /// 
    /// object Parametreli bir metodu thread üstünde koşturmak için kullanılıyor
    /// </summary>
    class Thread_Abort_04
    {
        public static void Calis()
        {
            var stopWatch = Stopwatch.StartNew();

            Thread t = new Thread(new ParameterizedThreadStart(ThreadMethod));
            t.Join();
            t.Start(10);

            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("-- Main thread: {0}", i);
                Thread.Sleep(500);
                if (i == 2)
                {
                    Console.WriteLine("Threadi iptal edeceğiz");
                    // Bu durumda Thread içinde ThreadAbortException tipinde istisna oluşacak 
                    t.Abort();
                }
            }
            Console.WriteLine("Toplam {0} milisn sürdü", stopWatch.ElapsedMilliseconds);

            /** Parameterized Thread sonucu:
             * -- Main thread: 0
             *      ThreadProc: 0
             * -- Main thread: 1
             *      ThreadProc: 1
             * -- Main thread: 2
             *      ThreadProc: 2
             * Threadi iptal edeceğiz
             * Toplam 1513 milisn sürdü
             * System.Threading.ThreadAbortException: Thread was being aborted.
             *    at System.Number.FormatInt32(Int32 value, String format, NumberFormatInfo info)
             *    at System.Int32.ToString(String format, IFormatProvider provider)
             *    at System.Text.StringBuilder.AppendFormatHelper(IFormatProvider provider, String format, ParamsArray args)
             *    at System.String.FormatHelper(IFormatProvider provider, String format, ParamsArray args)
             *    at System.IO.TextWriter.WriteLine(String format, Object arg0)
             *    at System.IO.TextWriter.SyncTextWriter.WriteLine(String format, Object arg0)
             *    at System.Console.WriteLine(String format, Object arg0)
             *    at CSharp_Orneklerim.Threading_Event.Thread_Abort_04.ThreadMethod(Object to)
             * in C:\_Projeler\csharp_orneklerim\Threading_Event\Thread_Abort_04.cs:line 62
             * Press any key to continue . . .
             */
        }

        public static void ThreadMethod(object to)
        {
            try
            {
                for (int i = 0; i < (int)to; i++)
                {
                    Console.WriteLine("     ThreadProc: {0}", i);
                    Thread.Sleep(500);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}

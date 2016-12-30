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
    class ParameterizedThread_03
    {
        public static void Calis()
        {
            var stopWatch = Stopwatch.StartNew();
            // public delegate void ParameterizedThreadStart(object obj); 
            // yapısında bir delegate olduğu için parametresi object olan bir metodu koşturabiliriz
            Thread t = new Thread(new ParameterizedThreadStart(ThreadMethod));

            // Parametremizi vererek başlatacağız
            t.Start(10);

            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("-- Main thread: {0}", i);
                Thread.Sleep(500);
            }

            Console.WriteLine("Toplam {0} milisn sürdü", stopWatch.ElapsedMilliseconds);

            /** Parameterized Thread sonucu:
             * -- Main thread: 0
             *      ThreadProc: 0
             *      ThreadProc: 1
             * -- Main thread: 1
             * -- Main thread: 2
             *      ThreadProc: 2
             *      ThreadProc: 3
             * Toplam 1504 milisn sürdü
             *      ThreadProc: 4
             *      ThreadProc: 5
             *      ThreadProc: 6
             *      ThreadProc: 7
             *      ThreadProc: 8
             *      ThreadProc: 9
             * Press any key to continue . . .
             */
        }

        public static void ThreadMethod(object to)
        {
            for (int i = 0; i < (int)to; i++)
            {
                Console.WriteLine("     ThreadProc: {0}", i);
                Thread.Sleep(500);
            }
        }
    }
}

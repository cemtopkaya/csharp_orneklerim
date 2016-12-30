using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

namespace CSharp_Orneklerim.Asenkron_Calling._1_Threading
{
    class ThreadStart_Join_01
    {
        public static void Calis()
        {
            var stopWatch = Stopwatch.StartNew();
            Thread t = new Thread(new ThreadStart(ThreadMethod));
            t.Start();

            for (int i = 0; i < 3; i++)
            {

                Console.WriteLine("-- Main thread: {0}", i);
                Thread.Sleep(500);
            }
            // t thread bitmeden Ana thread'in işleme devam etmeyeceğini Join metodu ile belirtiyoruz
            // join olmaksızın ekranda geçen süreyi gösteren aşağıdaki yazıyı göremeyiz
            //t.Join();
            Console.WriteLine("Toplam {0} milisn sürdü", stopWatch.ElapsedMilliseconds);

            /** Join'li sonuç:
             * 
             * -- Main thread: 0
             *      ThreadProc: 0
             * -- Main thread: 1
             *      ThreadProc: 1
             * -- Main thread: 2
             *      ThreadProc: 2
             *      ThreadProc: 3
             *      ThreadProc: 4
             *      ThreadProc: 5
             *      ThreadProc: 6
             *      ThreadProc: 7
             *      ThreadProc: 8
             *      ThreadProc: 9
             * Toplam 5009 milisn sürdü
             * Press any key to continue . . .
             */

            /** Join'siz sonuç:
             * -- Main thread: 0
             *      ThreadProc: 0
             * -- Main thread: 1
             *      ThreadProc: 1
             * -- Main thread: 2
             *      ThreadProc: 2
             * Toplam 1503 milisn sürdü
             *      ThreadProc: 3
             *      ThreadProc: 4
             *      ThreadProc: 5
             *      ThreadProc: 6
             *      ThreadProc: 7
             *      ThreadProc: 8
             *      ThreadProc: 9
             * Press any key to continue . . .
             */

        }

        public static void ThreadMethod()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("     ThreadProc: {0}",i);
                Thread.Sleep(500);
            }
        }


    }
}

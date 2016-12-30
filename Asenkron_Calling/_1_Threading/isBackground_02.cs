using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

namespace CSharp_Orneklerim.Asenkron_Calling._1_Threading
{
    /// <summary>
    /// BackgroundWorker does well with network accesses and other simple stuff. 
    /// </summary>
    class isBackground_02
    {
        public static void Calis()
        {
            var stopWatch = Stopwatch.StartNew();
            Thread t = new Thread(new ThreadStart(ThreadMethod));
            // isBackground özelliği default olarak false'turdiyerek bu threadin arka planda olacağını belirttik
            // isBackground = true diyerek bu threadin arka planda olacağını belirttik
            // Ana thread t threadinin bitmesini beklemeden sonlanacaktır.
            // Ana threadin işi bittikten sonra t threadinin ekrana yazdıramadığını aşağıda görebilirsiniz
            t.IsBackground = true;
            t.Start();

            for (int i = 0; i < 3; i++)
            {

                Console.WriteLine("-- Main thread: {0}", i);
                Thread.Sleep(500);
            }

            Console.WriteLine("Toplam {0} milisn sürdü", stopWatch.ElapsedMilliseconds);

            /** isBackground'lu sonuç:
             * -- Main thread: 0
             *      ThreadProc: 0
             * -- Main thread: 1
             *      ThreadProc: 1
             * -- Main thread: 2
             *      ThreadProc: 2
             *      ThreadProc: 3
             * Toplam 1503 milisn sürdü
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

namespace CSharp_Orneklerim.Asenkron_Calling._1_Threading
{
    class ThreadStatic_05
    {
        /// <summary>
        /// ThreadStatic özelliğiyle her thread bu field'ın kendine bir kopyasını oluşturacak
        /// </summary>
        [ThreadStatic]
        static int herThreadinKendiFieldi;

        public static void Calis()
        {
            var stopWatch = Stopwatch.StartNew();

            new Thread((son) =>
            {
                for (int i = 0; i < (int)son; i++)
                {
                    Console.WriteLine("     +2 ThreadProc Field: {0}", herThreadinKendiFieldi += 2);
                }
            }).Start(10);

            new Thread((son) =>
            {
                for (int i = 0; i < (int)son; i++)
                {
                    Console.WriteLine("       +3 ThreadProc Field: {0}", herThreadinKendiFieldi += 3);
                }
            }).Start(10);


            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("-- +1 Main thread: {0}", herThreadinKendiFieldi += 5);
                Thread.Sleep(500);
            }
            Console.WriteLine("Toplam {0} milisn sürdü", stopWatch.ElapsedMilliseconds);

            /** Parameterized Thread sonucu:
             * -- +1 Main thread: 5
             *      +2 ThreadProc Field: 2
             *        +3 ThreadProc Field: 3
             *        +3 ThreadProc Field: 6
             *        +3 ThreadProc Field: 9
             *        +3 ThreadProc Field: 12
             *        +3 ThreadProc Field: 15
             *        +3 ThreadProc Field: 18
             *        +3 ThreadProc Field: 21
             *        +3 ThreadProc Field: 24
             *        +3 ThreadProc Field: 27
             *        +3 ThreadProc Field: 30
             *      +2 ThreadProc Field: 4
             *      +2 ThreadProc Field: 6
             *      +2 ThreadProc Field: 8
             *      +2 ThreadProc Field: 10
             *      +2 ThreadProc Field: 12
             *      +2 ThreadProc Field: 14
             *      +2 ThreadProc Field: 16
             *      +2 ThreadProc Field: 18
             *      +2 ThreadProc Field: 20
             * -- +1 Main thread: 10
             * -- +1 Main thread: 15
             * Toplam 1503 milisn sürdü
             * Press any key to continue . . .
             */
        }
    }
}

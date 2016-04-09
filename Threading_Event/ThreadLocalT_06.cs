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
    /// ThreadLocal<T> ile her thread için YEREL VERİ kullanmak istersek, her thread için ayrıca çalıştırılmak 
    /// üzere ThreadLocal<T> tipini kullanabiliriz. ThreadLocal 4 farklı overloaded yapıcı metoda sahiptir.
    /// Generic tip olduğu için Thread'in içinde kullanmak isteyeceğimiz yerel değişkenin tipini T ile vereceğiz.
    /// Thread için oluşacak bu yerel değeri sınıfın içinde Value özelliğinde tutacak
    /// </summary>
    class ThreadLocalT_06
    {
        /// <summary>
        /// ThreadLocal içindeki T Value özelliğinde tutacak ve threadId.Value özelliğiyle bu değeri çekebileceğiz.
        /// </summary>
        static ThreadLocal<int> threadId = new ThreadLocal<int>(() =>
        {
            return Thread.CurrentThread.ManagedThreadId;
        });

        static ThreadLocal<List<string>> tlist = new ThreadLocal<List<string>>(() =>
         {
             return new List<string>();
         });

        public static void Calis()
        {


            var stopWatch = Stopwatch.StartNew();

            new Thread((son) =>
            {
                Console.WriteLine("     +2 ThreadProc Id: {0}", threadId);
                Console.WriteLine("     +2 ThreadProc Id Value: {0}", threadId.Value);
            }).Start(10);


            Console.WriteLine("-- +1 Main thread Id: {0}", threadId);

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSharp_Orneklerim.Threading_Event
{
    /// <summary>
    /// ThreadPool manages a group of threads. 
    /// We process jobs in parallel using thread pools. 
    /// ThreadPool is a static class that you can directly access. It provides us with the essential parts of thread pools.
    /// 
    /// ThreadPool.GetMaxThreads()
    /// Thread pools typically have a maximum number of threads. If all the threads are busy, additional tasks are placed in queue until they can be serviced as threads become available.
    /// 
    /// The ThreadPool type can be used on servers and in batch processing applications. 
    /// ThreadPool has internal logic that makes getting a thread much less expensive. 
    /// Threads are already made and are just "hooked up" when required.
    /// 
    /// For batch processing with many processors, you need ThreadPool.
    /// </summary>
    class ThreadPool_07
    {
        public static void Calis()
        {
            ThreadPool.QueueUserWorkItem((s) =>
            {
                Console.WriteLine(".... s:"+s);
            });

            // Basit ve parametresiz bir metodu ThreadPool içine WaitCallback ile göndermek
            WaitCallback_Example();

            

        }

        /// <summary>
        /// You can hook up methods to the ThreadPool by using QueueUserWorkItem. 
        /// You have your method you want to run on the threads. 
        /// And you must hook it up to QueueUserWorkItem. 
        /// 
        /// WaitCallback is described as a delegate callback method to be called when the ThreadPool executes. 
        /// </summary>
        static void WaitCallback_Example()
        {
            // Hook up the ProcessFile method to the ThreadPool.
            // Note: 'ProcessFile_metoduna_parametre' is an argument name. Read more on arguments.
            // Daha bilgi dolu bir nesneyi metoda göndermek için kendi sınıfımızdan bir nesne yaratır,
            // ProcessFile metodu içinde cast ederiz.
            var ProcessFile_metoduna_parametre = new object();
            ThreadPool.QueueUserWorkItem(new WaitCallback(ProcessFile), ProcessFile_metoduna_parametre);
        }

        static void ProcessFile(object a)
        {
            // I was hooked up to the ThreadPool by WaitCallback.
        }
    }
}

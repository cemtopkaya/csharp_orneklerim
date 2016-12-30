using System;
using System.Threading;
using System.Threading.Tasks;

namespace CSharp_Orneklerim.Task_Lambda_Delegate
{
    /// <summary>
    /// CancellationTokenSource parametresi nasıl kullanılıyor
    /// </summary>
    public class Task_02
    {
        static public void Calis()
        {
            /*
             * cts Task içine geçirdiğimiz ajan gibi istediğimiz zaman dışarıdan 
             * taskı durdumak için kullanıyoruz.
             */
            CancellationTokenSource cts = new CancellationTokenSource();
            var tokenSource = cts.Token;
            Task task = new Task(() =>
            {
                int a = 0;
                while (!tokenSource.IsCancellationRequested)
                {
                    Console.WriteLine("Çalışıyor " + (++a));
                    Task.Delay(1000).Wait();
                }
            }, tokenSource);
            task.Start();
            bitir(cts);
            task.Wait();
        }

        private static void bitir(CancellationTokenSource tokenSource)
        {
            Task.Run(() => {
                Task.Delay(3000).Wait();
                // Taskın dışından taskı iptal etmek için
                tokenSource.Cancel();
            });
        }
    }
}

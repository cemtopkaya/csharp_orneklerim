using System;
using System.Threading;
using System.Threading.Tasks;

namespace CSharp_Orneklerim.Asenkron_Calling._4_TPL
{
    /// <summary>
    /// Wait ile Thread.Join 
    /// </summary>
    public class Task_03
    {
        static public void Calis()
        {
            Task<int> task = new Task<int>(() =>
            {
                int a = 0;
                while (a < 10)
                {
                    Console.WriteLine("Çalışıyor " + (++a));
                    // 1 saniye delay dememize rağmen işe yaramaz
                    Task.Delay(1000)
                    // ta ki Wait() metodunu çağırana kadar
                        .Wait(); // TPL de yer alan await'in beklemesi gibidir
                }
                return a;
            });

            task.Start();
            task.Wait(); // Thread.Join ile aynı işi yapar ve taskın sonunu bekler
            Console.WriteLine("Main ve Task threadi aynı zamanda bitecek");
        }
    }
}

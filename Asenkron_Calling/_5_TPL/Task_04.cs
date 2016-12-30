using System;
using System.Threading;
using System.Threading.Tasks;

namespace CSharp_Orneklerim.Asenkron_Calling._4_TPL
{
    /// <summary>
    /// Func<TResult> ile Action, Action<object> farkı
    /// </summary>
    public class Task_04
    {
        // Senkron çalışan ve sonuç dönmeyen Task
        void SonucDonmeyenTask()
        {
            Task task = new Task(()=> { });
            task.Start();
        }

        // Senkron çalışan ve int sonuç dönen Task
        int SonucDonenTask()
        {
            Task<int> task = new Task<int>(() => 1);
            task.Start();
            int a = task.Result;
            return a;
        }


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
            // task.Result olmazsa background thread mantığında bitmesi beklenmez 
            // ve ana thread ekrana yazar çıkar
            var sonuc = task.Result; // Thread.Join ile aynı işi yapar ve taskın sonucunu bekler
            Console.WriteLine("Main ve Task threadi aynı zamanda bitecek");
        }
    }
}

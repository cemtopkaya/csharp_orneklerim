using System;
using System.IO;
using System.Threading;

namespace CSharp_Orneklerim.Asenkron_Calling._2_APM
{
    class BeginInvoke_Polling
    {
        static void Calis()
        {
            Console.WriteLine("Ana thread içinde");

            Func<int, string> fn = (a) =>
            {
                Console.WriteLine(" > fn metodu çalıştırıldı");
                var i = 0;
                while (i < 5)
                {
                    Console.WriteLine("    fn içinde dön");
                    Thread.Sleep(100);
                    i++;
                }
                return a.ToString();
            };

            var iar = fn.BeginInvoke(12, null, null);

            while (!iar.IsCompleted)
            {
                Console.WriteLine("Bekle bitmemiş"); Thread.Sleep(100);
            }

            string sonuc = fn.EndInvoke(iar);
            Console.WriteLine("fn metodunun sonucu: " + sonuc);
        }
    }
}

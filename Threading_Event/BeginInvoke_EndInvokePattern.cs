using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSharp_Orneklerim.Threading_Event
{
    class BeginInvoke_EndInvokePattern
    {
        static void Calis()
        {
            Console.WriteLine("Ana thread içinde");

            Func<int, string> temsiliMetot = (a) =>
            {
                Thread.Sleep(1500);
                return a.ToString();
            };

            IAsyncResult iar = temsiliMetot.BeginInvoke(1, null, null);
            /*
             * Arada yapılacak tonla iş olduğunu tahayyül edin
             * ve sanki bu yorum sıralarında yaptınız diye düşünün
             */
            var sonuc = temsiliMetot.EndInvoke(iar);
            Console.WriteLine("Sonuç sıcak sıcak geldi: " + sonuc);
            Console.WriteLine("Ana thread çıkıyor");
        }
    }
}

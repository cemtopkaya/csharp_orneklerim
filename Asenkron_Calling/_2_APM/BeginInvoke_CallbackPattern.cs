using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSharp_Orneklerim.Asenkron_Calling._2_APM
{
    class BeginInvoke_CallbackPattern
    {
        static void Calis()
        {
            Console.WriteLine("Ana thread içinde");

            Func<int, string> temsiliMetot = (a) =>
            {
                Thread.Sleep(1500);
                return a.ToString();
            };

            // Main thread içinde asenkron çağrımızın akibetini takip 
            IAsyncResult iar1 = temsiliMetot.BeginInvoke(123, (IAsyncResult iarCallback) =>
            {
                /**
                 *  IAsnyResult arayüzünü, AsyncResult sınıfı miras alır.
                 *    public class AsyncResult : IAsyncResult
                 *
                 * BeginInvoke metodu 
                 * 1- AsyncResult objesi yaratır. Bu objenin,
                 * 2- AsyncDelegate propertisinde temsiliMetot değişkenimizdeki metodu atar
                 * 3- AsyncState propertisinde BeginInvoke'un son parametresi olan
                 *    callback metoda geçirdiğimiz object tipine kutulanmış parametreyi atar
                 * 4- ThreadPool'dan bir thread alır ve temsiliMetot değişkenimizdeki metoda
                 *    BeginInvoke'un callback fonksiyon parametresine kadar olan parametreleri
                 *    (bu örnekte ilk parametre olan 123'ü) geçirir.
                 * 5- Thread tamamlandığında AsyncResult objesinin IsCompleted propertisini true
                 *    yapar ve callback fonksiyonuna bu objeyi geçirerek çağırır. 
                 *    
                 *   - iarCallback   : Callback'in her şeyden haberdar olması için IAsyncResult tipine indirgenmiş
                 *                     ama AsyncResult tipine upcast ederek kullanacağımız parametre
                 *   - AsyncState    : Callback metoda geçirilen parametreyi
                 *   - AsyncDelegate : Asenkron çağırılan metodu
                 *   - 
                 */
                AsyncResult ar = iarCallback as AsyncResult;

                // Callback metodumuza geçirdiğimiz parametre object tipine box edilir
                object cbMetodunaGelenParametre = ar.AsyncState;
                // User-defined bir tip oluşturmadan isimsiz tiple yarattığımız parametreyi
                // kullanmak için dynamic tipinde bir değişkene atayalım
                dynamic d = ar.AsyncState;

                Console.WriteLine("Gelen bilgi > {0} - {1}",
                    d.IsimsizTipimizin_Ilk_Propertisi,
                    d.IsimsizTipimizin_Ikinci_Propertisi);

                // Asenkron çalıştırılan metodu object tipinde ar.AsyncDelegate içinde alıyoruz
                // Metot barındırdığı için bu property, çağırdığımız metot tipine cast ediyoruz.
                // Metodun asenkron çalışmasının sonunda dönen sonucu(string tipinde)
                // alabilmek içinde EndInvoke çağrısı yapıyoruz
                string sonuc = (ar.AsyncDelegate as Func<int, string>).EndInvoke(iarCallback);
                Console.WriteLine("Asenkron çalışan metodumuzun sonucu: " + sonuc);
            },
            new
            {
                IsimsizTipimizin_Ilk_Propertisi = "Sözde parametre",
                IsimsizTipimizin_Ikinci_Propertisi = 999
            });

            // Ana thread, BACKGROUND THREAD olarak asenkron çalıştırılan metodun tamamlanmasını beklemez.
            // Doğal olarak thread tamamlandığında çalıştırılacak callback metodunun sonuçlarını göremeyiz.
            // Ama biz callback metodun DA çalıştığını görmek istiyoruz :))
            // Bu yüzden BeginInvoke ile oluşan threadin durumunu takip etmemiz için dönen IAsyncResult
            // tipindeki nesneyi kullanarak Thread tamamlanıncaya kadar Thread.Sleep ile bekleyelim
            while (iar1.IsCompleted == false) Thread.Sleep(1000);

            Console.WriteLine("Ana thread çıkıyor");
        }
    }
}

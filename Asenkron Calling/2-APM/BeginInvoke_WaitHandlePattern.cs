using System;
using System.IO;
using System.Threading;

namespace CSharp_Orneklerim.Threading_Event
{
    class BeginInvoke_WaitHandlePattern
    {
        static void Calis()
        {
            Console.WriteLine("Ana thread içinde");

            // önce varsa dosyayı silelim
            string filePath = "c:\\temp\\test.txt";
            if (File.Exists(filePath)) File.Delete(filePath);


            Func<byte[]> f = () =>
            {
                byte[] barrDonecekSonuc;
                Console.WriteLine("> Dosya yaratacağım");
                Thread.Sleep(5000);
                // FileStream cinsinden bir değer döndürecek ve işimiz bittiğinde 
                // dosya kullanımda demesin diye using ile sardık.
                using (var fs = File.Create(filePath))
                {
                    Console.WriteLine("> Dosya yaratıldı çıkıyorum");
                    // Aşağıdaki satırı açarsak hem Create ettiğimiz dosyayı fs içinde tutuyoruz
                    // hem de başka bir statik metot ile okumaya çalıştığımız için "in use" istisnası alırız
                    // barrDonecekSonuc = File.ReadAllBytes(filePath);
                    // ya da aşağıdaki kod ile aynı FileStream içinde okuyup byte dizimize yazabiliriz.
                    barrDonecekSonuc = new byte[fs.Length];
                    fs.Read(barrDonecekSonuc, 0, Convert.ToInt32(fs.Length));
                }
                // ya da aşağıdaki kod ile FileStream ile işimiz bittikten sonra dosya yolunu okuruz
                barrDonecekSonuc = File.ReadAllBytes(filePath);
                return barrDonecekSonuc;
            };

            // Callback deseniyle çalışmadığımız için ilk parametre null,
            // Bir callback yaratmadığımız için ona parametre göndermeyeceğiz. 2. parametre de null
            var iar = f.BeginInvoke(null, null);

            /*
             Bu arada çokça iş yapıyoruz mesela 100ms aralıklarla 600 kez sayalım
             */
            for (int i = 0; i < 6; i++)
            {
                Thread.Sleep(1000); // yukarıdaki işimiz 5000 de bu işe 6000 de bitecek
                Console.WriteLine(i);
            }


            Console.WriteLine("WaitOne ile bir sonraki işlemi yapmadan önce threadin tamamlanmasını bekliyoruz");
            // WaitOne dediğimizde artık sonraki satırı ancak ilgili Thread tamamlanınca çalıştıracak
            // Yani eğer threadin işi sürüyorsa ve sonraki satırımızda örneğin threadin sonucuyla ilgiliyse
            // EndInvoke ile sonucu aldıktan sonra devam edebileceğiz sonraki işleme
            iar.AsyncWaitHandle.WaitOne();


            Console.WriteLine("EndInvoke ile Thread'in sonuçlarını alıyoruz");
            byte[] barrDosya = f.EndInvoke(iar);

            // WaitOne ile başladığımız için Close dememiz ŞART!
            iar.AsyncWaitHandle.Close();

            Console.WriteLine("Ana thread çıkıyor");
        }
    }
}

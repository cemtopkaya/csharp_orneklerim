using System;
using System.Data;
using System.Runtime;

namespace CSharp_Orneklerim.Delegate_Event
{
    public class Event_01
    {
        public static void Calis()
        {
            Topcu kaleciMuslera = new Topcu() { Adi = "Muslera" };
            Topcu cenkGonen = new Topcu() { Adi = "Cenk Gönen" };

            // Olaylara abone olacak nesnelerin fonksiyonlarını ekleyelim
            cenkGonen.OnShoot += (sutuVuranOyuncu, sutunOzelligi) =>
            {
                // Muslera hiçbir topu tutamasın ve top ağlarla buluşsun.
                if (!kaleciMuslera.Yakalamayi_Dene(sutunOzelligi))
                {
                    // TopunDurumu türünden dönecek sonucu 
                    // hakem gol olarak ilan edecek
                    Console.WriteLine("Muslera > - Ah Vah ah vah...");
                    return new TopunDurumu()
                           {
                               x1 = -2,
                               x2 = -2,
                               y1 = -2,
                               y2 = -2,
                               Hizi = 0
                           };
                }
                else
                {
                    // kaleci topu tekmeyle sahanın ortasına uzaklaştırsın
                    return new TopunDurumu()
                    {
                        x1 = 0,
                        y1 = 12,
                        x2 = 20,
                        y2 = 15,
                        Hizi = 20
                    };
                }
            };

            // herkes pozisyonunu aldı ve golcümüz ceza sahasına giriyor
            cenkGonen.Ceza_Sahasina_Topla_Gir();
        }
    }

    public partial class Topcu
    {
        /*
         * Çeşitli özellikleri olabilir
         * tackle : topu ayağından almak
         * speed : hız
         * size : boyut
         */
        public string Adi;

        /// <summary>
        /// Tüm oyuncular ceza sahasına girebilir(savunma, hücum vs.)
        /// Ancak davranışları elbette farklıdır. 
        /// Örneğin rakip hücumcu girerse kaleyi görüp şut çekerken,
        /// kendi ceza sahasına giren savunmacı uzaklaştırmak isteyecektir.
        /// Bu yüzden bu metodu virtual yapalım ki, Topcu sınıfını miras alacak başka sınıflar
        /// kendi isteklerine göre ezebilsinler.
        /// </summary>
        public virtual void Ceza_Sahasina_Topla_Gir()
        {
            /** 
             * Oyuncu topu ceza sahasına sürsün ve kaleyi görsün.
             * Şut çekecek ve bizim olayımız tetiklenecek
             * this oyuncumuz şutu çekti
             */
            Shoot shoot = new Shoot()
                          {
                              Aci = 60, // Topa 60 dereceyle 
                              Hiz = 50, // 50 km hızda 
                              Zaman = DateTime.Now, // şimdi vursun
                          };

            TopunDurumu durumu = veShoooot(shoot);
            // Şut atıldığında bir olayı(event) tetiklemek istiyoruz. 
            // Böylece bu olayı dinleyen tüm abonelere şut ile ilgili bilgi verebilelim
            // Görünüşe göre veShoooot metodu bu işi yapacak.
            // veShoooot metodu Shoot türünden bir değişken alsın ve topun durumunu dönsün
            // Bu metodu birazdan tanımlayacağız 
            // Ancak önce oluşturacağımız metodun dönen tipi ve parametrelerine bakalım
            //
            // 1 Parametre geçireceğiz bu metoda Shoot tipinde bir değişken
            // ve topun son durumu dönecek: {koordinatı, yönü, hızı} 
            //
            // Demekki Shoot ve TopunDurumu diye iki sınıfımızı yazmalıyız
        }
    }

    // 1. Shoot sınıfı
    public partial class Shoot
    {
        public int Aci, Hiz;
        public DateTime Zaman;
    }

    // 2. Topun durumu
    public class TopunDurumu
    {
        // iki nokta bir vektör tanımlar. konumu ve Yönünü bulduk.
        public int x1, y1, x2, y2, Hizi;
    }

    // 3. OnShoot olayı için method handler
    // OnShoot olayı(eventi) için bir delegate tipinde metot tutucu tipine ihtiyacımız var
    public delegate TopunDurumu Del_Donussuz_Shoot_Parametreli(Topcu _topcu, Shoot _shoot);

    // 4. Topcu sinifina OnShoot olayi tanımlama vakti geldi
    // OnShoot eventini Topcu sınıfına eklemeliyiz ki, tetikleyebilelim
    public partial class Topcu
    {
        // event tanımlamaları sınıf içinde yapılır ve ancak sınıftan tetiklenebilirler
        // event değikenlerine doğrudan bir atama(eventDegiskeni = null) yapılamaz
        // ancak += ya da -= ile atama yapılabilir
        public event Del_Donussuz_Shoot_Parametreli OnShoot;
        
        // Geldik OnShoot olayımızı tetikleme işini yapacak metodumuza.
        // Metodumuz event tetikleyecek. Event ancak sınıfın içinden tetiklenebilir, nesneden değil!
        // bu yüzden public erişim belirleyicisini kullanamayız.
        // Topcu sınıfından türetilen tiplerin de bu metodu çağırması gerekecek 
        protected virtual TopunDurumu veShoooot(Shoot shoot)
        {
            // OnShoot bizim tetikleyeceğimiz olayımız olacak(event)
            // OnShoot olayının tanımlandığı yerde varsayılan bir değer yok! Null kontrolü yapalım ! 
            if (OnShoot == null)
            {
                throw new NoNullAllowedException("OnShoot olayına hiç metot eklenmiş olmaz, olamaz!");
            } 
            return OnShoot(this, shoot);
        }

        /// <summary>
        /// Shoot parametresi ve kalecinin özellikleri arasında bir fonksiyonun sonucuna göre
        /// gol ise true, değilse false dönebilir. Ama burada kaleci Muslera ise kesin gol diyelim
        /// </summary>
        /// <param name="_shoot"></param>
        /// <returns></returns>
        public bool Yakalamayi_Dene(Shoot _shoot)
        {
            if (this.Adi == "Muslera")
            {
                return false; // tutamadı, gol
            }
            return true;
        }
    }

    /**
     * ----- YAPIYI ANLAYALIM:
     * Farkettiyseniz olayı tetiklendiğinde, bu olaya abonelere
     *   olayı kimin yaptığı,
     *   nasıl bir parametreyle gerçekleştiğini dönüyor
     *   Sonuçtada abonelerin topa müdahele edeceği düşünüldüğü için TopunDurumu
     *   tipinde bir sonuç değerini bekliyoruz.
     * 
     *           TopunDurumu OnShoot(Topcu sender, Shoot e) 
     *       
     * 
     * ----- GENEL DELEGATE TİPİ TANIMLAMAYA ÇALIŞALIM:
     * Bu OLAY TANIMINI genelleyerek şu metodu olay tutucu metodun imzası olarak elde edebiliriz:
     * 
     *             void On_BirŞey_Oldu(object sender, OlayParams e)
     *             
     *        
     * ----- GENEL OLAY PARAMETRE TİPİ TANIMLAMAYA ÇALIŞALIM
     * Bu metot tutucuyu şöyle yazabiliriz:
     * 
     *        delegate void OnDelegate(object sender, EventArgs e)
     *       
     * Bu tanımala gereği bizim EventArgs diye "genel olay parametreleri" tutan bir üst sınıfa ihtiyacımız var
     * Bu sınıftan kendi parametrelerimizi taşıyan sınıflar yaratarak aynı delegate tipini kullanabiliriz
     *  
     * ----- EVENTHANDLER TİPİ İŞİMİZİ GÖRECEK SANKİ
     * Yukarıdaki delegate tip tanımına uyan genel bir tip var adı: EventHandler tipi. 
     * Hem her olaya uygun olan EventArgs tipinde parametresi de var
     * 
     *      public delegate void EventHandler(object sender, System.EventArgs e)
     *      
     * Şeklinde tanımlı ve yukarıdaki imzaya uygun(void dönen ve iki parametre alan[object, EventArgs] metotlar için)
     * EventHandler tipini kullanabiliriz artık.
     */

    // ----- GENEL OLAY PARAMETRE TİPİ olan EventArgs'dan türesin olay parametre tipimiz
    public partial class Shoot : EventArgs { }

    /** 
     * Artık olay tanımımızı değiştirebiliriz
     */

    public partial class Topcu
    {
        // Başlangıç için boş bir isimsiz metodu değer olarak atadık ki 
        // eventi tetikleyeceğimiz yerde if(OnShoot2 != null){ OnShoot2(..) } null kontrolüne gerek kalmasın
        public event EventHandler OnShoot2 = delegate(object sender, EventArgs args) { };
    }
    /**
     * OnShoot2 metoduna parametre olacak gelen args değerleri Shoot türünden olabilir.
     * Ancak içeride bilinçli(explicit) cast(tür dönüşümü) etmeliyiz
     * 
     * kaleciMuslera.OnShoot2 += (sutuVuranOyuncu, sutunOzelligi) => Console.WriteLine(((Shoot)sutunOzelligi).Hiz);
     * 
     * gibi. 
     * Yada generic tip olan EventHandler<T> ile EventArgs türünden değil, kendi parametre türümüz olan Shoot türünden
     * bir method handler yaratmasını söylemeliyiz. 
     * 
     * Aşağıdaki gibi:
     */

    public partial class Topcu
    {
        // Generic tipli EventHandler'ı kullanarak bizim arguman tipimizden değerleri geçiriyor olağız.
        // eventi tetikleyeceğimiz yerde if(OnShoot3 != null){ OnShoot3(..) } null kontrolüne gerek kalmasın
        public event EventHandler<Shoot> OnShoot3 = delegate(object sender, Shoot args) { };
    }

    /**
     * Eğer Topcu sinifindan türeyen bir SolBek tipimiz olsa ve onun da OnShoot olayını tetiklemesi gerekse
     */

    public partial class SolBek : Topcu
    {
        public override void Ceza_Sahasina_Topla_Gir()
        {
            base.Ceza_Sahasina_Topla_Gir();
        }

        /// <summary>
        /// Sol beklerin şutu azıcık farklı olsun. 
        /// Mesela sol ayağına gelirse normal golcüden 2 kaplan daha güçlü vursun ;)
        /// </summary>
        public void SolAyaklaShoot()
        {
            // Topu düzeltsin,
            // Sol ayağına alsın, 
            // hede höde yapsın ve vursun. OnShoot olayı ata sınıftan tetikleneceği için
            // base.veShoooot metodunu çağırsın
            base.veShoooot(new Shoot()
                           {
                               Hiz = 100,
                               Aci = 70, //azicik yamuk vursun
                               Zaman = DateTime.Now
                           });

            // sonra ellerini başının arasına alsın,
            // Geri koşsun vs. vs....
        }
    }

}

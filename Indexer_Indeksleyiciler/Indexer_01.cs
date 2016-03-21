using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharp_Orneklerim.Indexer_Indeksleyiciler
{
    // İndeksleyicileri interface üstünden zorla uygulatabilirsiniz.
    /// <summary>
    /// - Bir indeksleyici mutlaka bir geri dönüş tipine sahip olmalıdır. Yani bir indeksleyiciyi void olarak tanımlayamayız.
    /// - Bir indeksleyiciyi static olarakta tanımlayamayız.
    /// - Bir indeksleyici en az bir parametre almalıdır. 
    ///   Bununla birlikte, bir indeksleyici birden fazla ve çeşitte parametrede alabilmektedir.
    /// - Indeksleyicileri aşırı yükleyebiliriz (Overload). 
    ///   Ancak bir indeksleyiciyi aşırı yüklediğimizde, bu indeksleyicileri birbirlerinden ayırırken ele aldığımız imzalar sadece parametreler ile belirlenir. 
    ///   Indeksleyicinin geri dönüş değeri bu imzada ele alınmaz.
    /// - Indeksleyici parametrelerine, normal değişkenlermiş gibi davranamayız. 
    ///   Bu nedenle bu parametreleri ref ve out anahtar sözcükleri ile yönlendiremeyiz.
    /// - Bir indeksleyici her zaman this anahtar sözcüğü ile tanımlamalıyız. 
    ///   Nitekim this anahtar sözcüğü, indeksleyicinin kullanıldığı sınıf nesnelerini temsil etmektedir. 
    ///   Böylece sınıfın kendisi bir dizi olarak kullanılabilir.
    /// </summary>
    public interface IndexerKelimeSayar
    {
        // Indeksleyiciler "this" ile çağırıldığına göre ancak nesneden çağırılabiliyorlar !(kapiş?)
        // [...] ile çağırıldığına göre belliki indeks üstünden çalışıyor
        int this[string sKelime]
        {
            get;
        }
        int this[int iHarfliKelimeSayisi]
        {
            get;
        }
    }

    abstract class AbsClassIndexer
    {
        abstract public string this[int idx, bool b, string adi]
        {
            get;
        }
    }

    class KelimeSayar : AbsClassIndexer, IndexerKelimeSayar
    {
        private Dictionary<string, int> dicKelimeler = new Dictionary<string, int>();
        // Bilinçsizçe(kapalı, implicit) olarak interface'den gelen indexer. Tüm nesnelerden erişilebilir
        public int this[string sKelime]
        {
            get { return dicKelimeler.FirstOrDefault(d => d.Key == sKelime.ToLower()).Value; }
        }
        // Açık ifade edilmiş (interface adıyla) bu indexer ancak IndexerKelimeSayar tipinden çağırılabilir. 
        // Bu yüzden public tanımlanamaz. Ancak kenti tipine cast edilmiş nesneler tarafından erişilebilir
        int IndexerKelimeSayar.this[int iHarfliKelimeSayisi]
        {
            get
            {
                return dicKelimeler.Keys.Count(key => key.Length == iHarfliKelimeSayisi);
            }
        }
        // Çok parametreli indexer. Abstract ata sınıfında tanımlı 
        public override string this[int idx, bool b, string adi]
        {
            get { return idx + "-" + b.ToString() + "-" + adi; }
        }

        // Sınıf içinde tanımlı, inherit edilMEYEN indekleyici. Yavru sınıflar tarafından ezilebilir olarak işaretlendi.
        public virtual bool this[int index]
        {
            get { return index%2 == 0; }
        }

        //public static void this[int a,bool b] { get; }
        public KelimeSayar(string _metin)
        {
            string[] tumKelimeler = _metin.Split("".ToCharArray());
            for (int i = 0; i < tumKelimeler.Length; i++)
            {
                var kelime = tumKelimeler[i].ToLower();
                if (dicKelimeler.FirstOrDefault(d => d.Key == kelime).Key != null)
                {
                    dicKelimeler[kelime] += 1;
                }
                else
                {
                    dicKelimeler.Add(kelime, 1);
                }
            }
        }
    }

    class YavruSinif:KelimeSayar
    {
        public YavruSinif(string _metin) : base(_metin)
        {
        }
    }
    public class Indexer_01
    {
        public static void Calis()
        {
            KelimeSayar ks = new KelimeSayar("Kırmızı başlıklı kız, kırmızı elmalarını sepetine takmış gidiyordu.");
            string sArananKelime = "Kırmızı";
            //((IndexerKelimeSayar) ks)[];
            Console.WriteLine("{0} kelimesi {1} kez geçiyor. {2} Harfli kelime sayısı {3}'dır/dar/dur...", sArananKelime, ks[sArananKelime], sArananKelime.Length, ks[sArananKelime.Length]);

            YavruSinif kss = new YavruSinif("aasd as d as sdsds");
            Console.WriteLine(kss[5]);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharp_Orneklerim.Indexer_Indeksleyiciler
{
    // İndeksleyicileri interface üstünden zorla uygulatabilirsiniz.
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

    class KelimeSayar:IndexerKelimeSayar
    {
        private Dictionary<string, int> dicKelimeler = new Dictionary<string, int>();
        public int this[string sKelime]
        {
            get { return dicKelimeler.FirstOrDefault(d => d.Key == sKelime.ToLower()).Value; }
        }
        public int this[int iHarfliKelimeSayisi]
        {
            get
            {
                return dicKelimeler.Keys.Count(key => key.Length == iHarfliKelimeSayisi);
            }
        }

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
    public class Indexer_01
    {
        public static void Calis()
        {
            KelimeSayar ks = new KelimeSayar("Kırmızı başlıklı kız, kırmızı elmalarını sepetine takmış gidiyordu.");
            string sArananKelime = "Kırmızı";
            Console.WriteLine("{0} kelimesi {1} kez geçiyor. {2} Harfli kelime sayısı {3}'dır/dar/dur...", sArananKelime, ks[sArananKelime], sArananKelime.Length, ks[sArananKelime.Length]);
        }
    }
}

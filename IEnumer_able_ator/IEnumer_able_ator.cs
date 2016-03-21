using System;
using System.Collections;
using System.Collections.Generic;

namespace CSharp_Orneklerim.IEnumer_able_ator
{
    class Diploma : IEnumerable
    {
        public string Adi;

        private string[] okullar = new[]
        {
            "Öğretmenler evi",
            "Orhangazi Ortaokulu",
            "Orhangazi Lise",
            "İTÜ",
            "Haliç",
            "USÜ",
        };
        public Diploma(string adi)
        {
            Adi = adi;
        }

        public override string ToString()
        {
            return this.Adi;
        }

        public IEnumerator GetEnumerator()
        {
            return new DiplomaEnumerator(okullar);
        }

        public class DiplomaEnumerator : IEnumerator
        {
            private int idx = -1;
            private string[] dizi;
            public DiplomaEnumerator(string[] strings)
            {
                dizi = strings;
            }

            public bool MoveNext()
            {
                return ++idx < dizi.Length;
            }

            public void Reset()
            {
                idx = 0;
            }

            public object Current
            {
                get { return dizi[idx]; }
            }
        }
    }

    class Kisi : IEnumerable, IEnumerable<Diploma>
    {
        public string Adi;

        private Diploma[] diplomas = new[]
        {
            new Diploma("İlkokul"),
            new Diploma("Ortaokul"),
            new Diploma("Lise"),
            new Diploma("Üniversite"),
            new Diploma("Yüksek Lisans"),
            new Diploma("Doktora"),
        };

        IEnumerator<Diploma> IEnumerable<Diploma>.GetEnumerator()
        {
            for (int i = 0; i < diplomas.Length; i++)
            {
                yield return diplomas[i];
            }
        }

        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < diplomas.Length; i++)
            {
                yield return diplomas[i];
            }
        }
    }

    internal class IEnumer_able_ator
    {
        public static void Calis()
        {
            Kisi[] karr = new[]
            {
                new Kisi() {Adi = "Cenk"},
                new Kisi() {Adi = "Cem"},
                new Kisi() {Adi = "Canan"}
            };
            var bb = karr.GetEnumerator();

            var fevzi = new Kisi() {Adi = "Fevzi"};
            foreach (var o in fevzi)
            {
                Console.WriteLine(o);
            }

            foreach (object okul in new Diploma("MYO"))
            {
                Console.WriteLine("Okul: " + okul);
            }

            foreach (Diploma diploma in fevzi)
            {
                Console.WriteLine("diploma: " + diploma);
            }
        }
    }
}

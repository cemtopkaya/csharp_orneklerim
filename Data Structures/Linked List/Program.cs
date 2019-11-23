using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using System.Runtime.CompilerServices;

namespace cop_console_app
{
    class Program
    {
        static void Main(string[] args)
        {
            var cbl = new CiftliBagliListe<int>();
            var n1 = Dugum<int>.DugumYarat(1);
            var n2 = Dugum<int>.DugumYarat(2);
            var n3 = Dugum<int>.DugumYarat(3);
            var n4 = Dugum<int>.DugumYarat(4);
            cbl.SonaEkle(n1);
            cbl.SonaEkle(n2);
            cbl.SonaEkle(n3);
            cbl.SonaEkle(n4);

            var n5 = Dugum<int>.DugumYarat(5);
            cbl.ArayaEkle(n5, 2);
            Console.ReadLine();
        }
    }
    [DebuggerDisplay("{DebuggerDisplay}")]
    public class Dugum<T>
    {
        private string DebuggerDisplay {
            get {
                T sonrakiDeger = default(T);
                sonrakiDeger = Sonraki != null ? Sonraki.Degeri : default(T);

                return string.Format("Degeri: {0}, Sonraki: {1}", Degeri, sonrakiDeger);
            }
        }
        public T Degeri;
        public Dugum<T> Onceki, Sonraki;
        private Dugum() { }

        /// <summary>
        /// Jenerik tiplerin parametreli yapıcı metotları olamayacağı için
        /// aşağıdaki statik metodu kullanacağım.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_veri"></param>
        /// <returns></returns>
        public static Dugum<T> DugumYarat<T>(T _veri)
        {
            return new Dugum<T>
            {
                Degeri = _veri
            };
        }
    }
    class CiftliBagliListe<T>
    {
        private Dugum<T> Bastaki;
        private int ToplamDugumSayisi;

        public void ArayaEkle(Dugum<T> node, int sira)
        {
            if (ToplamDugumSayisi < sira)
                throw new ArgumentOutOfRangeException("düğüm sayısından büyük index girilemez");

            if (sira < 0)
                throw new ArgumentOutOfRangeException("0 dan küçük index girilemez");

            if (ToplamDugumSayisi == sira)
                SonaEkle(node);

            if (sira == 0)
                BasaEkle(node);

            Dugum<T> onceki = Bastaki,
            sonraki = Bastaki.Sonraki;

            bool basaDondu = sonraki == Bastaki;
            var index = 1;
            while(sira >= index && !basaDondu)
            {
                onceki = sonraki;
                sonraki = sonraki.Sonraki;
                basaDondu = sonraki == Bastaki;
                index++;
            }
            // Cift yönlü ise
            onceki.Sonraki = node;
            node.Onceki = onceki;
            // sonraki == Bastaki olduysa dairesel bağlı liste olacak.
            node.Sonraki = sonraki;
            ToplamDugumSayisi++;
        }

        public void BasaEkle(Dugum<T> node)
        {
            if (node == null)
                throw new ArgumentNullException("boş olamaz");

            if (Bastaki == null)
                Bastaki = node;
            else
            {
                node.Sonraki = Bastaki;
                /* Çift yönlü değilse Onceki düğüm ile işimiz olmayacak
                  *    node.Onceki = null
                  */
                node.Onceki = Bastaki.Onceki;
                Bastaki = node;
            }
            ToplamDugumSayisi++;
        }
        public void SonaEkle(Dugum<T> node)
        {
            if (node == null)
                throw new ArgumentNullException("olamaz");

            /* dairesel bağlantılı listede en sona eklenen
             * daima SONRAKİ özelliğinde ilk düğümü refere etmeli
             * Eğer listede tek bir düğüm varsa Sonraki diye kendini
              * işaret etmeli.
              * Bkz. https://www.quora.com/Why-does-the-first-node-of-circular-linked-list-point-to-itself
             * node.Sonraki = Bastaki ?? node;
             *
             * Dairesel değilse
             *    node.Sonraki = null
             * bırakılabilir
             * */

            if (Bastaki == null)
            {
                Bastaki = node;
            }
            else
            {
                var sonDugum = SonDugum();
                sonDugum.Sonraki = node;

                /* Eğer Çift yönlü dairesel bağlı liste olsaydı
                 * Bastaki.Onceki = node;
                 * node.Onceki = sonDugum;
                 **/
            }

            /* dairesel bağlı liste için */
            node.Sonraki = Bastaki;
            ToplamDugumSayisi++;
        }

        private Dugum<T> SonDugum()
        {
            Dugum<T> current = Bastaki;
            if (Bastaki == Bastaki.Sonraki)
                return Bastaki;

            while (Bastaki != current.Sonraki)
                current = current.Sonraki;
            return current;
        }
    }
}

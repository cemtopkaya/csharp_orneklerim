using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Orneklerim.Operator_Overloading
{
    /// <summary>
    /// 122 Derece 45 Dakika Kuzey Enlemi = 122.75 Derece Kuzey
    /// 122 Derece 45 Dakika 45 Saniye Kuzey enlem = 122.7625 Derece Kuzey
    /// Ondalık Değer = Derece + (Dakika/60) + (Saniye/3600)
    /// Ondalık Değer =    122 + (    45/60) + (    45/3600) = 122.7625
    /// </summary>
    class OperatorOverloading
    {
        private int _dakika;
        private int _saniye;
        public int Derece;

        public int Dakika { get; set; }

        public int Saniye { get; set; }


        public OperatorOverloading(int _derece, int _dakika, int _saniye)
        {
            this.Derece = _derece;
            this.Dakika = _dakika;
            this.Saniye = _saniye;
        }

        public override string ToString()
        {
            return Derece + "° " + Dakika + "′ " + Saniye + "″";
        }

        public string OndalikGosterimi()
        {
            return Derece + (Dakika / 60.0) + (Saniye / 3600.0) + "";
        }

        public static OperatorOverloading operator +(OperatorOverloading _ol1, OperatorOverloading _ol2)
        {
            var olSonuc = new OperatorOverloading((_ol1.Derece + _ol2.Derece), (_ol1.Dakika + _ol2.Dakika), (_ol1.Saniye + _ol2.Saniye));
            return olSonuc;
        }

        public static OperatorOverloading operator +(OperatorOverloading _ol1, int _saniye)
        {
            var olSonuc = new OperatorOverloading((_ol1.Derece), (_ol1.Dakika), (_ol1.Saniye + _saniye));
            return olSonuc;
        }


        /**
         * İlişkisel operatörlerin aşırı yüklenmesi
         */

        public static bool operator ==(OperatorOverloading _ol1, OperatorOverloading _ol2)
        {
            return _ol1.OndalikGosterimi() == _ol2.OndalikGosterimi();
        }

        /// <summary>
        /// İlişkisel operatörlerdeki ana kural ilişkisel operatörlerin birbirlerinin zıtlarının sınıf içinde ve aynı türde olmasının zorunlu olmasıdır. 
        /// Yani biz burada yukarıdaki metotların yalnızca bir tanesini yazıp bırakamazdık ya da birininin geri dönüş tipini bool, birinin int yapamazdık. 
        /// Ayrıca return !(a==b); satırı son derece mümkündür.
        /// </summary>
        /// <param name="_ol1"></param>
        /// <param name="_ol2"></param>
        /// <returns></returns>
        public static bool operator !=(OperatorOverloading _ol1, OperatorOverloading _ol2)
        {
            return !(_ol1 == _ol2);
        }

        /**
         * true ve false operatörlerinin aşırı yüklenmesi
         */
        public static bool operator true(OperatorOverloading _ol)
        {
            return (_ol.Derece > 0);
        }

        public static bool operator false(OperatorOverloading _ol)
        {
            return _ol.Derece < 0;
        }

        /**
         * Mantıksal operatörlerin aşırı yüklenmesi
         */

        public static bool operator |(OperatorOverloading _ol1, OperatorOverloading _ol2)
        {
            return (_ol1.Derece > 0 || _ol2.Derece < 180);
        }

        public static bool operator &(OperatorOverloading _ol1, OperatorOverloading _ol2)
        {
            return (_ol1.Derece < 0 || _ol2.Derece > 180);
        }

        /**
         * Tür dönüşümleri
         */

        public static implicit operator double(OperatorOverloading _ol)
        {
            string str = _ol.OndalikGosterimi();
            Console.WriteLine(str);
            return Convert.ToDouble(str);
        }

        public static implicit operator OperatorOverloading (int _derece)
        {
            return new OperatorOverloading(_derece, 0, 0);
        }
    }

    class Program
    {
        public static void Calis()
        {
            var d1 = new OperatorOverloading(122, 45, 45);
            var d2 = new OperatorOverloading(10, 60, 45);
            var d3 = d1 + d2;
            double duble = d3;
            OperatorOverloading o = 24;
            // double dönüşümü bilinçli olarak yapıldığı için int, byte gibi dönüşümler için yeniden tanımlamaya gerek yok ama tanımlanabilir.
            int i = (int) o;
            Console.WriteLine(d3.OndalikGosterimi());
            Console.WriteLine(duble);
        }
        /*
         * Atama operatörünü (=) aşırı yükleyemeyiz. Çünkü zaten gerek yoktur. 
         * Biz atama operatörünü aşırı yükleyelim veya yüklemeyelim zaten kullanabiliriz. 
         * İşlemli atama operatörlerinde ise şöyle bir kural vardır: Örneğin + operatörünü aşırı yüklemişsek += operatörünü de kullanabiliriz. 
         * Bu durum bütün işlemli atama operatörlerinde geçerlidir. 
         * Hiçbir operatörün öncelik sırasını değiştiremeyeceğimiz gibi temel veri türleri (string, int, vb.) arasındaki operatörlere de müdahale edemeyiz. 
         * Bu konuda işlenenler dışında hiçbir operatörü aşırı yükleyemeyiz. 
         * Örneğin dizilerin elemanlarına erişmek için kullanılan [] operatörünü, 
         *   yeni bir nesneye bellekte yer açmak için kullanılan new operatörünü, 
         *   ?: operatörünü, 
         *   vb. aşırı yükleyemeyiz.
         */
    }
}

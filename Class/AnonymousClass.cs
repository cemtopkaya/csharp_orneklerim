using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Orneklerim.Class
{
    internal delegate int DelDeneme(int a);
    class AnonymousClass
    {
        public static void Calis()
        {
            var anonymousClass = new { Adi = "Cem", Soyadi = "Topkaya" };
            var anotherAnonymousClass = new { Adi = "Cenk", Soyadi = "Topkaya" };
            Console.WriteLine(anonymousClass.GetType());
            // iki nesnenin tipinin propertyleri aynı sıra ve add olduğu için aynı anonymous tipten yaratılıyorlar
            Console.WriteLine(anotherAnonymousClass.GetType());
            // iki nesnenin sirasi, adi aynı olduğu için birbirine atanabilir
            anotherAnonymousClass = anonymousClass;

            Console.WriteLine("\n".PadLeft(80, '*'));

            // Aynı isim ve sırada olmasına rağmen
            var ayniIsimde = new { Soyadi = "Topkaya", Adi = "Canan" };
            var farkliTipte = new { Soyadi = 32, Adi = "Canan" };
            // iki nesnenin tipinin propertyleri aynı sırada olmasına rağmen aynı tipte değiller
            Console.WriteLine("Aynı İsimde\t:" + ayniIsimde.GetType());
            Console.WriteLine("Farklı Tipte\t:" + farkliTipte.GetType());

            Console.WriteLine("\n".PadLeft(80, '*'));

            // Aynı tipte olacakları için aynı dizinin içinde yer alacaklardır.
            var anonArray = new [] { new { name = "apple", diam = 4 }, new { name = "grape", diam = 1 } };
            // Aşağıdaki kodu açarak sıraları farklı anonymous sınıf nesnelerini anonArray'i Shift+F9 ile inceleyebilirsiniz
            // var anonArray = new object[] { new { diam = 4, name = "apple" }, new { name = "grape", diam = 1 } };
            foreach (var aaa in anonArray)
            {
                Console.WriteLine(aaa.GetType());
            }

            Console.WriteLine("\n".PadLeft(80, '*'));

            DelDeneme d = delegate(int a) { return a; };
            var a1 = new {calis = d};
            a1.calis(12);
            Console.WriteLine(a1.GetType());
            /**
             * Anonim sınıflar sadece public alanlar içerebilir.
             * Başlangıç değerleri atanmış olamlıdır
             * Static olmazlar ve bir yöntem belirlenemez
             */
        }
    }
}

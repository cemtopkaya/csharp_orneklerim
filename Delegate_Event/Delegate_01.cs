using System;
using System.IO;

namespace CSharp_Orneklerim.Delegate_Event
{
    /*
     *  delegate Bir tip tanımlamasıdır, değişken deklarasyonu değildir. 
     *  Hiçbir tip tanımlaması metot içinde tanımlanmaya müsade edilmez. 
     *  Tip tanımlamaları sadece sınıf(class) ve isim uzayı kapsamında yapılabilir
     */

    partial class Delegate_01
    {
        static void Calis()
        {
            // 1. Kısımda DELEGATE nasıl çalışır
            Birinci_Kisim();

            // 2. Kısımda COvariance ve CONTRAvariance örneği olacak
            Ikinci_Kisim();

            // 3. Kısımda lambda ifadeleriyle delegate tanımlanması
            Ucuncu_Kisim();

            // 4. Kısımda Action ve Func tanımlamalarıyla delegate tanımlanması
            Dorduncu_Kisim();

            // 5. Kısımda delegate tiplerin GENERIC hallerini yazalım
            Besinci_Kisim();
        }


        #region *********************** 1. KISIM - DELEGATE Nasıl Çalışır? *******
        // Önce bir delegate tanımlayacağız(Tıpkı sınıf tanımladığımız gibi). 
        // delegate'imizin adı DelegateTipi olacak(tıpkı Kisi adında class tanımlar gibi). 
        // Void dönecek ve parametre almayacak(tıpkı property, field, metotlar tanımladığımız classlar gibi)
        public delegate void DelegateTipi();
        private static void Birinci_Kisim()
        {
            // Yukarıda tanımladığımız DelegateTipi'nden f örneğini(tıpkı cenkGonen nesnesini Oyuncu sınıfından türetir gibi) tanımlayalım.
            // f nesnemize(Delegate tipi referans tiptir)
            // A delegate is a reference type that can be used to encapsulate a named or an anonymous method.
            DelegateTipi f = null;
            DelegateTipi f1 = null;
            f1 = delegate { Console.WriteLine("f1 yaz bişi"); };
            f1 += delegate { Console.WriteLine("f1 yaz bişi daha"); };

            f += delegate { Console.WriteLine("bu da yaz"); };
            f += f1;
            Console.WriteLine(f.GetInvocationList().Length);
            f -= (DelegateTipi)f1.GetInvocationList()[1];
            Console.WriteLine(f.GetInvocationList().Length);
            f();


            //--------------------------------- STATIC tip olarak Delegate tanımlama --------------------------------------
            // STATIC_VAR:
            // 
            // TekParametreliIntDonuslu tipini tanımlıyoruz
            // static TekParametreliIntDonuslu static_delegate_degisken : ile sınıfa bağlı bir field tanımlıyoruz
            // ve hemen aşağıdaki satırlarda bir metodu bu değişkene değer olarak atayıp delegate üstünden metodu ateşliyoruz.
            Delegate_01.static_delegate_degisken = new TekParametreliIntDonuslu(delegate(int a) { return a += 1; });
            Delegate_01.static_delegate_degisken(3); // 4 dönecektir
        }
        // STATIC değişken olarak deklare edeceğimiz değişkenin türetileceği tip olan delegate tanımlaması
        public delegate int TekParametreliIntDonuslu(int _i);
        // delegate tipler static tanımlanamaz ancak delegate tipten static bir değişken tanımlanabilir (STATIC_VAR)
        public static TekParametreliIntDonuslu static_delegate_degisken;

        // Hatta delegate tiplerinin get ve set metotları olabildiği için property olarak da tanımlayabiliyoruz ***
        public DelegateTipi budaPropertyOlsun { get; set; }
        #endregion

        #region *********************** 2. KISIM - CO-CONTRA_variance ************
        // delegate nesneleri, delegate'in imzasındaki dönüş tipinden türeyen nesneleri döndüren metotları işaret ederse
        public delegate Stream Delegate_Covariance_tipi();

        // delegate nesneleri, delegate'in imzasındaki parametrelerin türeyen nesnelerini parametre olarak alan metotları işaret ederse
        public delegate void Delegate_Contravariance_tipi(StreamWriter _sw);
        private static void Ikinci_Kisim()
        {
            // Metodumuz FileStream(Stream'den türemiş), TextReader parametresi aldığı için
            // Covariance delegate kullanılmış diyebiliriz
            Delegate_Covariance_tipi del_co = delegate()
                                                        {
                                                            // Dönüş nesnesi de Stream sayılır ;)
                                                            return new FileStream("dosyaYolu.txt", FileMode.OpenOrCreate);
                                                        };

            // f_contra metodu TextWriter türünden alıyor. Bu da StreamWriter'da türetildiği için contravariance oluyor
            Delegate_Contravariance_tipi del_contra = f_contra;
        }

        static void f_contra(TextWriter tw) { }
        #endregion

        #region *********************** 3. KISIM - LAMBA EXPRESSIONS *************

        public delegate void Del_Donussuz_Parametreli(int _a, byte _b);
        public delegate TextWriter Del_DonusTipli_Parametreli(string _dosyaYolu);
        private static void Ucuncu_Kisim()
        {
            //----------------------- DÖNÜŞSÜZ(void)
            // İsimsiz fonksiyon olarak delegate ile şöyle yazılır:
            Del_Donussuz_Parametreli delegate1 = delegate(int a, byte b)
                                               {
                                                   Console.WriteLine("Ben dönüşsüz bir metodum");
                                                   Console.WriteLine("{0} ve {1} değerinde 2parametre aldım", a, b);
                                               };

            // Lambda ifadesiyle şöyle yazılır:
            Del_Donussuz_Parametreli lambda1 = (a, b) =>
                                               {
                                                   Console.WriteLine("Ben dönüşsüz bir metodum");
                                                   Console.WriteLine("{0} ve {1} değerinde 2parametre aldım", a, b);
                                               };

            //----------------------- DÖNÜŞLÜ

            // İsimsiz fonksiyon olarak delegate ile şöyle yazılır:
            Del_DonusTipli_Parametreli delegate2 = delegate(string dosyaAdresi)
                                                   {
                                                       TextWriter tw = File.CreateText(dosyaAdresi);
                                                       return tw;
                                                   };

            // Lambda ifadesiyle şöyle yazılır:
            // Tek parametre olduğu için parantez içine almaya gerek yok "_dosyaninAdresi" parametresini
            Del_DonusTipli_Parametreli lambda2 = _dosyaninAdresi =>
                                                 {
                                                     // Parametreyi kullanmadık ama StreamWriter türünde bir nesne dönmeden edemeyeceğim
                                                     TextWriter tw = Console.Out;
                                                     tw.WriteLine("Ekrana gitmeden merhaba diyeyim");
                                                     return tw;
                                                 };
        }
        #endregion

        #region *********************** 4. KISIM - Action & Func *****************
        private static void Dorduncu_Kisim()
        {
            //----------------------- DÖNÜŞSÜZ (void)
            /* Action ve Func zaten Delegate'ten türetilmiş oldukları için 
             * Action ya da Func tipinden bir nesne yaratmak 
             * Delegate tipinde bir nesne yaratmakla aynı anlama geliyor.
             */
            Action<int, byte> donussuz_2_parametreli_metot = delegate(int a, byte b)
                                                        {
                                                            Console.WriteLine("Dönüşsüz demiştik. Parametreler:");
                                                            Console.WriteLine("{0} ile {1} değerlerini içeriyor", a, b);
                                                        };
            // Doğrudan örnek türettiğimiz bir tip tanımı olarak kullandık Action'ı
            donussuz_2_parametreli_metot(1, 2);

            // Başka bir Action'a atayabiliriz
            Action<int, byte> donussuz_parametreli_metod = new Action<int, byte>(donussuz_2_parametreli_metot);
            donussuz_parametreli_metod(3, 4);

            // delegate ile sınıf veya namespace kapsamında tip oluşturmadan, 
            // void dönen metodu doğrudan nesne yaratarak çağırmak için 
            // delegate(){} kısmını Action<>(...) içine alıyoruz.
            Action<int, byte> tip_olusturmadan_delegate_ile_donussuz_metot_tanimlamak = new Action<int, byte>(delegate(int a, byte b) { });

            //*********** DÖNÜŞLÜ
            // delegate ile sınıf veya namespace kapsamında tip oluşturmadan, 
            // değer dönen metodu doğrudan nesne yaratarak çağırmak için 
            // delegate(){} kısmını Func<>(...) içine alıyoruz.
            Func<int, string> tip_olusturmadan_delegate_ile_metot_yazmak = new Func<int, string>(delegate(int a) { return a.ToString(); });
            tip_olusturmadan_delegate_ile_metot_yazmak(211);

            // Func<int,string> : int tipinde 1 parametre alıyor string dönüyor
            // tek parametreyi paranteze sarmaya gerek yok
            // birden çok satırda işlem yapmadığı için gövdeyi {...} sarmaya da gerek yok
            Func<int, string> string_donuslu_int_parametreli = i => i.ToString();
            string_donuslu_int_parametreli(114); // 114 yazacak ekrana
        }

        #endregion

        #region *********************** 5. KISIM - Generic Delegates *************

        public delegate S jenerikFunc<T, K, S>(T t, K k);
        public delegate void jenerikAction<T, K>(T t, K k);
        private static void Besinci_Kisim()
        {
            jenerikFunc<int, byte, string> jenerikFuncNesnesi = (i, b) => (i + b).ToString();
            string toplam = jenerikFuncNesnesi(3, 4);// toplam = "7" 

            jenerikAction<int,byte> jenerikActionNesnesi = delegate(int i, byte b)
                                                           {
                                                               Console.WriteLine("dönüşsüz metot!");
                                                               Console.WriteLine(i+b);
                                                           };
            jenerikActionNesnesi(4, 5);// Ekrana iki satır yazacak > 
            // 1. satır: "dönüşsüz metot!", 
            // 2. satır: "9"
        }
        #endregion
    }
}

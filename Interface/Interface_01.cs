namespace CSharp_Orneklerim.Interface
{
    partial class Interface_01
    {
        public static void Calis(string[] args)
        {
            ISahipOlmaYontemi o = new Leasing();
            IKiralama o1 = new Leasing(); o = o1;
            Leasing o2 = new YatirimBank(); //o2 = o1;
        }
    }

    interface ISahipOlmaYontemi
    { // Nesne yaratılamaz, Kontrattırlar, Miras alınırlar,
        void KurumBul();
        
        // Bir arabirim(arayüz-interface) içinde alan(field) tanımlanamaz.
        // Alan(field) ancak SINIF(class) ya da YAPI(struct) içine tanımlanabilir. 
        // Örn. >>> private int a = 10;  >>> Interface cannot contain field

        // Bir arabirim(arayüz-interface) içinde kurucu metot tanımlanamaz.
        // Yapıcı metotlar(constructor) ancak SINIF(class) ya da YAPI(struct) içine tanımlanabilir.
        // Örn. >>> public ISahipOlmaYontemi(){ } >>> Interface cannot contain constructor

        // Bir arabirim(arayüz-interface) içinde yıkıcı metot(destructor) tanımlanamaz.
        // Bir Yıkıcı, oluşturulan nesneyi yıkmak için kullanılır
        // Yıkıcı metotlar(constructor) ancak SINIF(class) içine tanımlanabilir.
        // Örn. >>> ~ISahipOlmaYontemi(){ }  >>> Only class types can contain destructor

        // Bir arabirimdeki tüm yöntemler(metotlar) ortaktır ve erişim belirleyici(public, private...) tayin edilemez
        // Örn. >>> public void Calisin(); >>> Sadece new operatorünü, bilinçli gizleme yaptığınızı derleyiciye bildirmek için kullanabilirsiniz.
        
        // Bir arabirim içinde hiç bir türü(enum, class, struct, interface) tanımlayamazsınız
        // Örn. >>> enum Arabirim_Icine_Yuvalanmis_Bir_Tip_Tanimlamasi { }

        // Bir arabirimi, kendisinden türetilmiş struct(YAPI) ya da class(SINIF)'tan miras alamazsınız.
        // Ancak kendisini miras almış başka bir arabirimden miras alabilirsiniz
        // Örn. >>> IAta arayüzünü IMirasAlmisBaskaArayuz tipi miras alsın
        //      >>> interface IMirasAlmis: IAta {...}   IAta sınıfını miras alabilmesi ancak kendisi ya da kendisini miras almıs bir arayüzden mümkündür
        //      >>> class Sinif : IMirasAlmis           Sinif tipimiz IAta sınıfını dolaylı ya da 
        //      >>> class Sinif : IAta                                              dogrudan miras alabilir. 
        //      >>> class Sinif2: Sinif                 Başka bir sınıf üstünden miras alamaz!
    }
    interface ISatinAlma : ISahipOlmaYontemi { }
    interface IKiralama : ISahipOlmaYontemi { }

    class Leasing : IKiralama
    { // Nesne yaratılabilirler(yapıcı metotları olur), miras alırlar, 
        public void KurumBul() { } // kontrat gereği miras aldığı arayüzlerin metotlarını gerçekleştirirler
        public byte KurumunYildizi() { return 5; }
    }

    class YatirimBank : Leasing
    {
        public void Hehe() { }
    }
}

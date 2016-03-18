using System;

namespace CSharp_Orneklerim.Interface
{
    /// <summary>
    /// Açık(bilinçli) arabirim uygulaması (Explicit interface implementation).
    /// Bir Araba sınıfı ABS, ASR ESP, EDL fren sistemlerine sahip olsun.
    /// Bu tiplerin hepsinde FrenYap metodu tanımlanmak zorunda bırakılsın.
    /// Nesne üstünden bu metodu çalıştırmak için bu tiplerden birine, cast 
    /// ederek ilgili metodu çalıştırabiliriz. 
    /// 
    /// </summary>
    public class Interface_03
    {
        public static void Calis()
        {
           Araba a = new Araba();
           //a.FrenYap metodunu göremeyiz taki FrenYap metoduna sahip bir tipe cast edinceye dek
            ((IASR)a).FrenYap();
            ((IABS)a).FrenYap();
            ((IESP)a).FrenYap();
            ((IEDL)a).FrenYap();
        }
    }

    class Araba : IABS, IASR, IESP, IEDL
    {
        // Bilinçli olarak bir implementation yaparsak erişim belirleyici (public, private...) tayin edemeyiz
        // Örn. >>> public void IABS.FrenYap()
        void IABS.FrenYap()
        {
            Console.WriteLine("ABS freni yaptı");
        }

        void IASR.FrenYap()
        {
            Console.WriteLine("ASR freni yaptı");
        }

        void IESP.FrenYap()
        {
            Console.WriteLine("ESP freni yaptı");
        }

        void IEDL.FrenYap()
        {
            Console.WriteLine("EDL freni yaptı");
        }
    }

    interface IASR
    {
        void FrenYap();
    }
    interface IABS
    {
        void FrenYap();
    }
    interface IESP
    {
        void FrenYap();
    }
    interface IEDL
    {
        void FrenYap();
    }

    
}

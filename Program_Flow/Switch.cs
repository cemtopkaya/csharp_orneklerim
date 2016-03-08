using System;

namespace CSharp_Orneklerim.Program_Flow
{
    enum Cinsiyet
    {
        Kadin, Erkek
    }

    partial class Switch
    {

        internal enum Turist
        {
            Populasyon, Millet, Din, Cins, Dil
        }

        public static void Calis()
        {
            Console.Write("Alfabe Notunuzu giriniz: ");
            // Girdi
            string sNot = Console.ReadLine();
            // Hesaplama
            string sNotAraligi = null;
            switch (sNot)
            {
                case "A":
                    sNotAraligi = "80'den büyük"; break;
                case "B":
                    sNotAraligi = "50-80"; break;
                case "C":
                    sNotAraligi = "30-50"; break;
                case "F":
                    sNotAraligi = "30'dan küçük"; break;
                default:
                    Console.WriteLine("A,B,C,F harflerinden birini girmiş olmalıydınız!");
                    break;
            }
            // Çıktı
            Console.WriteLine("Notunuzun rakam aralığı: "+sNotAraligi);
        }

        public static void GetirIstatistik(char _turist)
        {
            if (_turist == 'c') { }
            else if (_turist == 'd') { }
            
        }
        public static void GetirIstatistik(string _turist)
        {
            if (_turist == "Din") { }
            else if (_turist == "Dil") { }
            else if (_turist == "Cins") { }
        }
        public static void GetirIstatistik(Program.Turist _turist)
        {
            switch (_turist)
            {
                case Turist.Cins:
                    //.......
                    break;
                    case Turist.Dil:
                    //......
                    break;
                    
            }
            
        }
        enum Notlar
        {
            A,B,C,F
        }
        public static void CalisEnum()
        {
            GetirIstatistik('m');
            GetirIstatistik(Turist.Millet);

            Notlar notum = Notlar.A;
            Console.Write("Alfabe Notunuzu giriniz: ");
            // Girdi
            string sNot = Console.ReadLine();
            Notlar o = (Notlar) Enum.Parse(typeof (Notlar), sNot);
            // Hesaplama		o	C	object {CSharp_Orneklerim.Switch.Program.Notlar}

            string sNotAraligi = null;
            switch (sNot)
            {
                case "A":
                    sNotAraligi = "80'den büyük"; break;
                case "B":
                    sNotAraligi = "50-80"; break;
                case "C":
                    sNotAraligi = "30-50"; break;
                case "F":
                    sNotAraligi = "30'dan küçük"; break;
                default:
                    Console.WriteLine("A,B,C,F harflerinden birini girmiş olmalıydınız!");
                    break;
            }
            // Çıktı
            Console.WriteLine("Notunuzun rakam aralığı: "+sNotAraligi);
        }
    }

}
using System;

namespace CSharp_Orneklerim.Program_Flow
{
    partial class If
    {
        public static void Calis()
        {
            Console.Write("Notunuzu giriniz: ");
            // Girdi
            string sNot = Console.ReadLine();
            int iNot = Convert.ToInt32(sNot);
            // Hesaplama
            string sAlfabeNot = null;
            if (iNot > 80)
            { sAlfabeNot = "A"; }
            else if (iNot > 50)
            { sAlfabeNot = "B"; }
            else if (iNot > 30)
            { sAlfabeNot = "C"; }
            else
            { sAlfabeNot = "F"; }

            // Çıktı
            Console.WriteLine("Notunuzun alfabe karşılığı: "+sAlfabeNot);
        }
    }

}
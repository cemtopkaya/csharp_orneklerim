using System;

namespace CSharp_Orneklerim.Program_Flow
{
    partial class While
    {
        public static void Calis()
        {
            // Girdi
            SayiGir:
            Console.Write("Hangi sayının faktöriyelini bulmak istersiniz: "); 
            int iFaktoriyeliArananSayi = Convert.ToInt32(Console.ReadLine());

            // Doğrulama
            if (iFaktoriyeliArananSayi < 0)
            {
                Console.WriteLine("Bunu bana neden yapıyorsun? POZİTİF sayı dururken!");
                goto SayiGir;
            }

            // Hesaplama
            int j = 1,
                iFaktoriyel = 1;
            while (iFaktoriyeliArananSayi >= j)
            {
                iFaktoriyel *= j;
                j++;
            } 

            // Çıktı
            Console.WriteLine("Evreka: "+iFaktoriyel);
        }
    }

}
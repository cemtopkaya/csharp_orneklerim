using System;

namespace CSharp_Orneklerim.Program_Flow
{
    partial class For
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
            for (int iFaktoriyel = 1, j = 1; j <= iFaktoriyeliArananSayi; j++)
            {
                Console.WriteLine("{0} x {1} = {2}", iFaktoriyel, j, iFaktoriyel * j);
                iFaktoriyel *= j;
            }
        }
    }

}
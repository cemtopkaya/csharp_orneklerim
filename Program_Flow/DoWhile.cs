using System;

namespace CSharp_Orneklerim.Program_Flow
{
    partial class DoWhile
    {
        public static void Calis()
        {
            // Girdi
            Console.Write("Hangi sayının faktöriyelini bulmak istersiniz: "); 
            int iFaktoriyeliArananSayi = Convert.ToInt32(Console.ReadLine());

            // Hesaplama
            int j = 1,
                iFaktoriyel = 1;
            do
            {
                // Doğrulama
                if (iFaktoriyeliArananSayi < 0)
                {
                    Console.WriteLine("Bunu bana neden yapıyorsun? POZİTİF sayı dururken!");
                    break;
                }

                iFaktoriyel *= j;
                j++;
            } while (iFaktoriyeliArananSayi>=j);

            // Çıktı
            Console.WriteLine("Evreka: "+iFaktoriyel);
        }
    }

}
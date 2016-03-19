using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Orneklerim.Pointers
{
    class Pointer
    {
        public static void Calis()
        {
            Pointer_2();
        }

        public static void Pointer_1()
        {
            unsafe
            {
                // int tipinde i adında bir değişken oluşturuyoruz ve içine 100 yazıyoruz
                // i=100 ile değerini atıyoruz. Yani i diyerek çağırdığımızda değerini alacağız
                // &i diyerek çağırdığımızdaysa adresini elde edebiliyoruz
                int i = 100;

                // int tipinde a adında bir işaretçi tanımlıyoruz.
                // a içinde adres bilgisi tutacak. 
                // a = ...degisken_adresi... ataması yapabiliriz
                // tipi kadar byte okuyarak değerini alabiliriz
                //  a = &i  atamasıyla i'nin adresini a işaretçisine atayabiliyoruz
                // *a = i   atamasıyla i'nin değerini a işaretiçisinin adresine yazıyoruz
                int* a;
                a = &i;  // işaretçi adres tuttuğu için değişkenin adresini atayabiliyoruz &i ile.

                int* b;
                int j = 200;
                b = &j;

                Console.WriteLine("Önce  > i:{0} , j:{1}", i, j);
                UnsafeDegistir(a, b); // i ve j değişkenlerinin adreslerini işaretçilere atayarak gönderiyoruz
                Console.WriteLine("Sonra > i:{0} , j:{1}", i, j);
                UnsafeDegistir(&i, &j); // i ve j nin adreslerini doğrudan işaretçilere parametre olarak atıyoruz
                Console.WriteLine("Sonra > i:{0} , j:{1}", i, j);
            }   
        }

        public static unsafe void UnsafeDegistir(int *a, int *b)
        {
            int gecici;
            gecici = *a;
            *a = *b;
            *b = gecici;
        }


        public static unsafe void Pointer_2()
        {
            // short   |   -32.768 ile 32,767  |  16 Bitlik imzalı tamsayı  |  System.Int16
            // short   |   0 ile 65535         |  16 Bitlik imzalı tamsayı  |  System.Int16
            int i = int.MaxValue-10;
            ushort* bp;
            bp = (ushort*)&i;
        }

        public static unsafe void Pointer_3()
        {
            string s = "Cem Topkaya";
            fixed (char* cp = s) // diziyi sabitliyoruz GC uçurmasın diye
            {
                Console.WriteLine("Birer birer yazalım");
                for (int j = 0; j < s.Length; j++)
                {
                    Console.WriteLine(*(cp+j));
                }
                Console.WriteLine("\nAnneeee... bitti...");
            }
        }
    }
}

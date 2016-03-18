using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharp_Orneklerim.Operator_Overloading
{
    /// <summary>
    /// İşleçler > AND, OR, NOT, XOR, <<(SOLA KAYDIRMA)
    /// </summary>
    class Operators_Islecler
    {
        public static void Calis()
        {
            byte a = 32;
            byte b = 240;//272-256=16
            byte c = (byte)(a + b); // byte'a cast edilmese int aralığında olacaktı bu yüzden XOR, NOT, OR, AND opreatorleri(işleçleri) int'lere uygulanır
            Console.WriteLine(c); // 16 yazacak
            //-------------------------------------------------

            // 204 : 11001100
            byte num = 204;
            // Ekrana yazdırmak çeşitli formatlarla mümkün. https://msdn.microsoft.com/tr-tr/library/586y06yf(v=vs.110).aspx
            Console.WriteLine(num.ToString("X")); //CC
            Console.WriteLine("{0:x8}", num); //000000cc
            Console.WriteLine(Convert.ToString(num, 2)); //11001100

            Console.WriteLine("\n******   ~ (Değilleme NOT) İşleci(operator)  *******");
            // NOT(~) İşleci(operator): 
            // Tekil bir işleçtir ve sayının önüne ~ karakteri getirilerek çalıştırılır.
            // Bitlerini tersine çevirir. 1->0'a, 0->1'e dönüşür
            //  204 : 11001100
            // ~204 : 00110011 : 51
            // Ancak 204 operator için int değerdir yani:
            //  204 : 00000000000000000000000011001100 : 8 bit değerinin öncesinde 24bit(3 byte) 0 olacak
            // ~204 : 11111111111111111111111100110011 : Tabiki bu 0'lar not'landığında 1'e dönüşecek
            Console.WriteLine(num);                       //                              204
            Console.WriteLine(Convert.ToString(num, 2));  //                         11001100
            Console.WriteLine(Convert.ToString(~num, 2)); // 11111111111111111111111100110011
            Console.WriteLine((byte)(~num));              //                               51


            Console.WriteLine("\n******  << (sola kaydırma) İşleci(operator)  *******");
            // <<(sola kaydırma) İşleci(operator): 
            // İkili bir işleçtir(operator'dür)
            //   Sola_kaydırılacak_değer << kac_bit_kaydirilacagi_bilgisi
            // Bitlerini << den sonraki değer kadar sola kaydırır
            //  204      :   11001100
            //  204 << 2 : 1100110000   (Tipi artık System.Int32) // Console.WriteLine((num << 2).GetType());
            //  204 << 2 :   00110000   (sonucu byte'a cast ettiğimizde soldaki gibi olacaktır)
            Console.WriteLine(num);                             //      204
            Console.WriteLine(Convert.ToString(num, 2));        // 11001100
            Console.WriteLine(Convert.ToString((num << 2), 2)); //   110011
            Console.WriteLine((byte)(num << 2));                //       51


            Console.WriteLine("\n******  >> (sağa kaydırma) İşleci(operator)  *******");
            // >>(sağa kaydırma) İşleci(operator): 
            // İkili bir işleçtir(operator'dür)
            //   Sağa_kaydırılacak_değer >> kac_bit_kaydirilacagi_bilgisi
            // Bitlerini >> den sonraki değer kadar sağa kaydırır
            //  204      :   11001100
            //  204 >> 2 :     110011   (Tipi artık System.Int32) // Console.WriteLine((num >> 2).GetType());
            Console.WriteLine(num);                             //      204
            Console.WriteLine(Convert.ToString(num, 2));        // 11001100
            Console.WriteLine(Convert.ToString((num >> 2), 2)); //   110011
            Console.WriteLine((byte)(num >> 2));                //       51


            Console.WriteLine("\n********  | ( VEYA - OR ) İşleci(operator)  *********");
            // | (OR) İşleci(operator): 
            // İkili bir işleçtir(operator'dür)
            //   veyalanacak_sayı | veyaLAYACAK_sayı
            // Operatörün solundaki sayının bitlerini, sağındaki sayının bitleriyle veyalar
            //  204      :   11001100
            //   24      :   00011000  
            //  204 | 24 :   11011100 
            Console.WriteLine(num);                             //      204
            Console.WriteLine(Convert.ToString(num, 2));        // 11001100
            Console.WriteLine(Convert.ToString(24, 2));         // 00011000
            Console.WriteLine(Convert.ToString((num | 24), 2)); // 11011100
            Console.WriteLine((byte)(num | 24));                //      220


            Console.WriteLine("\n********  & ( VE - AND ) İşleci(operator)  *********");
            // & (AND) İşleci(operator): 
            // İkili bir işleçtir(operator'dür)
            //   velenecek_sayı | veLEYECEK_sayı
            // Operatörün solundaki sayının bitlerini, sağındaki sayının bitleriyle VEler
            //  204      :   11001100
            //   24      :   00011000  
            //  204 & 24 :   00001000 
            Console.WriteLine(num);                             //      204
            Console.WriteLine(Convert.ToString(num, 2));        // 11001100
            Console.WriteLine(Convert.ToString(24, 2));         // 00011000
            Console.WriteLine(Convert.ToString((num & 24), 2)); // 00001000
            Console.WriteLine((byte)(num & 24));                //        8


            Console.WriteLine("\n****** ^ ( ÖZEL YA DA - XOR ) İşleci(operator)  ******");
            // ^ (XOR) İşleci(operator): 
            // İkili bir işleçtir(operator'dür)
            //   XOR'lanacak_sayı | XORLAYACAK_sayı
            // XOR Tablosunda aynı iki bit 0 iken, farklı değerdeki iki bit 1 değerini verecektir
            //   1 0 : 1
            //   0 1 : 1
            //   0 0 : 0
            //   1 1 : 0
            //  204      :   11001100
            //   24      :   00011000  
            //  204 ^ 24 :   11010100 
            Console.WriteLine(num);                             //      204
            Console.WriteLine(Convert.ToString(num, 2));        // 11001100
            Console.WriteLine(Convert.ToString(24, 2));         // 00011000
            Console.WriteLine(Convert.ToString((num ^ 24), 2)); // 11010100
            Console.WriteLine((byte)(num ^ 24));                //      212

        }
    }
}

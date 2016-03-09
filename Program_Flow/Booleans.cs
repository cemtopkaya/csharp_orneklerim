using System;

namespace CSharp_Orneklerim.Program_Flow
{
    partial class Booleans
    {
        public static void Calis()
        {
            bool b1 = 3 + 2 == 5;
            bool b2 = 3 + 2 > 5;
            bool bVe = b1 && b2;
            bool bVeya = b1 || b2;
            bool bDegil = !(b1 || b2);
            Console.WriteLine("bVe: {0}\nbVeya: {1}\nbDegil: {2}",bVe,bVeya,bDegil);
        }
    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Orneklerim.Array_List_Dictionary
{
    public class Diziler
    {
        public static void Calis()
        {
            // a dizisindeki x. indexten y kadar eleman, b'nin z. indeksine copyalanacak(tabii b bu kadar elemanı alacak genişliğe sahipse)
            int[] a = { 1, 2, 3, 4, 5 };
            int[] b = new int[2];
            Array.Copy(a, 1, b, 0, 3);
        }
    }
}

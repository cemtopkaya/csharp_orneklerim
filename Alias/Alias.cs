using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sitring = System.String;

namespace CSharp_Orneklerim.Alias
{
    class Alias
    {
        public static void Calis()
        {
            sitring bir = "bir";
            string iki = "bir";
            String uc = "bir";
            if ((bir == iki) && (iki == uc))
            {
                Console.WriteLine("Eşitler");
            }
            else
            {
                Console.WriteLine("Değiller");
            }
        }
    }
}

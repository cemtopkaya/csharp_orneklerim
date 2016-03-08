using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Orneklerim.Delegate_Event
{
    class Delegate_Temsilci
    {
        public static void Calis()
        {
            FuncPointer fp = c => c*=c;
            Console.WriteLine(fp(3));
        }

        private delegate int FuncPointer(int a);
    }
}

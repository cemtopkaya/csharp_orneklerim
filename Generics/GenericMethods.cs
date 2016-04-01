using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Orneklerim.Generics
{
    class GenericMethods
    {
        public void calis(object a)
        {
            Console.WriteLine("objectli olandayım");
        }

        public void calis<T>(T a)
        {
            Console.WriteLine("T li olan calis");
            this.calis(a as object);
        }

        void calis(string a)
        {
            Console.WriteLine("string içinde");
        }
    }
}

using System;
using System.IO;
using CSharp_Orneklerim.Delegate_Event;
using CSharp_Orneklerim.Generics;
using CSharp_Orneklerim.Indexer_Indeksleyiciler;
using CSharp_Orneklerim.Interface;
using CSharp_Orneklerim.Linq;
using CSharp_Orneklerim.Operator_Overloading;
using CSharp_Orneklerim.Pointers;
using CSharp_Orneklerim.Socket_Client_Server;

namespace CSharp_Orneklerim
{
    partial class Program
    {
        static void Main(string[] args)
        {
            //Pointer.Calis();
            //Linq_01_Select.Calis();
            //Indexer_01.Calis();
            //GenericClass.Calis();
            //Alias.Alias.Calis();
            Socket_01.Calis();

            Action<int> c = (a) => { Console.WriteLine("A: " + a); };
            c(12);
        }
    }

}

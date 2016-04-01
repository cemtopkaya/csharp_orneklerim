using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharp_Orneklerim.Interface;

namespace CSharp_Orneklerim.Generics
{
    public class GenericClass
    {
        public static void Calis()
        {
            BMW b = new BMW();
            b.Yavasla();
        }
    }

    interface IFren
    {
        void Dur();
    }
    
    class ABS:IFren
    {
        public void Dur()
        {
            Console.WriteLine("ABS Fren yapılıyor");
        }
    }

    interface IVites
    {
        void VitesBuyut();
    }

    class ManuelVites:IVites
    {
        public void VitesBuyut()
        {
            Console.WriteLine("Vites arttırıyor");
        }
    }

    class Araba<Fren,Vites> where Fren: IFren where Vites: IVites
    {
        protected Fren frenMek;
        protected Vites vitesMek;

    }

    class BMW : Araba<ABS, ManuelVites>
    {
        public void Yavasla()
        {
            frenMek.Dur();
        }

        public BMW()
        {
            this.frenMek = new ABS();
            this.vitesMek = new ManuelVites();
        }
    }
}

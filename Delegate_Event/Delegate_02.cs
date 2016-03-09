using System;
using System.Threading;

namespace CSharp_Orneklerim.Delegate_Event
{
    class Delegate_02
    {
        public static void Calis()
        {
            FuncPointer fp = c => c *= c;
            Console.WriteLine(fp(3));

            Func<int> f = () => 1;
            Console.WriteLine(f());
            Golcu g = new Golcu();

            g.OnPozisyon += () =>
            {
                Console.WriteLine("- Kale midir o be ya?");
                Console.WriteLine("- Vuram be ya");
                return g.Sut(10);
            };

            g.OnSut += (_golmu) =>
            {
                Console.WriteLine(_golmu
                    ? "- Abe goollll oldu goooolll :)"
                    : "- Gidem buralardan köyüme ben :(");
            };

            g.PozisyonaGir();
        }

        private delegate int FuncPointer(int a);
    }

    class Golcu
    {
        public Func<bool> OnPozisyon;
        public Action<bool> OnSut;
        private int KaleyeMesafe;

        public void PozisyonaGir()
        {
            Random rnd = new Random();
            bool golOldu = false;
            while (!golOldu)
            {
                this.KaleyeMesafe = rnd.Next(35);
                Console.WriteLine(this.KaleyeMesafe);
                if (this.KaleyeMesafe < 20)
                {
                    golOldu = KaleyiGordu();
                }
                Thread.Sleep(1000);
            }
        }

        private bool KaleyiGordu()
        {
            Console.WriteLine("Kaleyi yaklaşık " + this.KaleyeMesafe + " metre uzaktan görüyor....");
            Console.WriteLine("Vurursa gol olurrr...!");
            return OnPozisyon();
        }

        public bool Sut(byte sutunHizi)
        {
            Func<bool> fnGol = () =>
            {
                Console.WriteLine("Ve gooooollll.....");
                OnSut(true);
                return true;
            };

            Func<bool> fnAut = delegate
            {
                Console.WriteLine("Direğin üstünden dışarı... :(");
                OnSut(false);
                return false;
            };

            return (
                ((sutunHizi / (KaleyeMesafe + 0.0001)) > 0.7)
                    ? fnGol
                    : fnAut
                )();
        }
    }
}

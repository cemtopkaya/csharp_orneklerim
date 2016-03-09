using System;
using System.Threading;

namespace CSharp_Orneklerim.Delegate_Event
{
    public class Event_02
    {
        public static void Calis()
        {
            Console.WriteLine("Çay yapmaya başlıyoruz...");
            Console.WriteLine();

            CayMakinasi cm = new CayMakinasi();
            cm.eventDemlendi += new OnDemlendi(cm_OnDemlendi);
            // Lambda ile delegate fonksiyon
            cm.eventDemlendi += () => Console.WriteLine("Demlenmiş işte kap getir 2 davşan kanı...");

            cm.Demle();

            Console.WriteLine();
            Console.WriteLine("Tavşan kanı bunlaaarrr...");
        }

        static void cm_OnDemlendi()
        {
            Console.WriteLine("**************************************************************");
            Console.WriteLine("* Çayın olduğunda, makine metot işaretçisini tetikler.");
            Console.WriteLine("* ");
            Console.WriteLine("* O işaretçiyede kendi metodumuzu iliştiririzki ");
            Console.WriteLine("* tam o sıra bizde işlemler yapabilelim.");
            Console.WriteLine("* ");
            Console.WriteLine("* Buna en güzel örnek, düğmeye tıklandığında işlem yapmamız");
            Console.WriteLine("*  ya da GridView'a her satır eklendiğinde (RowDataBound) ");
            Console.WriteLine("* satıra müdahale etme isteğimiz verilebilir.");
            Console.WriteLine("**************************************************************");

        }
    }

    public delegate void OnDemlendi();
    class CayMakinasi
    {
        public CayMakinasi()
        {
            Console.WriteLine("Çay koyuldu...");
        }

        public event OnDemlendi eventDemlendi;

        public void Demle()
        {
            Console.WriteLine();
            Console.Write("Çay demleniyor ");
            for (int i = 3; i > 0; i--)
            {
                Console.Write(".");
                Thread.Sleep(1000);
            }
            Console.WriteLine();
            Demlendi();
        }

        private void Demlendi()
        {
            if (eventDemlendi != null)
            {
                Console.WriteLine("  ---       DEMLİK İÇİNDE İŞLEMLER YAPILIYOR       --- ");
                Console.WriteLine("  --- Demlikte işlem yapılıyor. Bitince haber verilecek");
                Console.WriteLine("  --- ");
                Console.Write("  --- Demleniyor");
                for (int i = 3; i > 0; i--)
                {
                    Thread.Sleep(3000);
                    Console.Write(".");
                }
                Console.WriteLine("  --- ");
                Console.WriteLine("  --- Demlikten Haber VAR: <Çay demlendi>");
                Console.WriteLine();
                Console.WriteLine();
                eventDemlendi();
            }
        }
    }
}

using System;
using System.Runtime.InteropServices;

namespace CSharp_Orneklerim.Program_Flow
{
    partial class ThisBase
    {
        public static void Calis()
        {
            Doktor Ozgur = new Doktor("Özgür Sümer");
            var Mehmet = new Kisi("Mehmet", "Tomak");
            var Ali = new Kisi("Ali", "Özkardeşler", new DateTime(1985,1,1));
        }
    }

    class Kisi
    {
        private string adi;
        private string soyadi;
        private DateTime dogumTarihi;
        public DateTime DogumTarihi
        {
            get { return this.dogumTarihi; }
            private set
            {
                /* Uye yaşı 18 den küçük
                 100 den büyük olamaz */
                if (DateTime.Now.Year - value.Year < 18 && DateTime.Now.Year - value.Year > 100)
                {
                    Console.WriteLine("Amca olmaz, olamaz!");
                    return;
                }
                dogumTarihi = value;
            }
        }

        public Kisi(string _adi, string _soyadi)
        {
            adi = _adi;
            soyadi = _soyadi;
        }

        public Kisi(string _adi, string _soyadi, DateTime _dtarihi)
            : this(_adi, _soyadi)
        {
            this.DogumTarihi = _dtarihi;
        }

        public void KendiniTanit()
        {
            Console.WriteLine("Merhaba ben {0} {1}. Doğum tarihim {2}", this.adi, this.soyadi, this.dogumTarihi);
        }
    }

    class Calisan
    {
        public string adi;
        public string soyadi;

        public Calisan(string _adiSoyadi)
        {
            this.adi = _adiSoyadi.Substring(0, _adiSoyadi.IndexOf(' '));
            this.soyadi = _adiSoyadi.Substring(_adiSoyadi.IndexOf(' ') + 1);   
        }
        public Calisan(string _adi, string _soyadi)
        {
            this.adi = _adi;
            this.soyadi = _soyadi;
        }
    }
    class Doktor:Calisan
    {
        public Doktor(string _adiSoyadi) : base(_adiSoyadi)
        {
        }
        public Doktor(string _adi, string _soyadi) : base(_adi, _soyadi)
        {
        }
    }
}
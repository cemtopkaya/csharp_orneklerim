using System;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading;

namespace CSharp_Orneklerim.Indexer_Indeksleyiciler
{
    public class Program
    {
        public static void Calis()
        {
            Kisi cem = new Kisi()
                       {
                           Adi = "Cem",
                           TCNo = 44740473564,
                           Meslegi = new Meslek() { MeslekAdi = "Uzay Müh.", OrtalamaYillikGeliri = 100 }
                       };
            Kisi cenk = (Kisi)cem.Clone();
            cenk.Adi = "Cenk";
            cenk.TCNo = 88740473564;
            cenk.Meslegi.MeslekEkle(new Meslek()
                                    {
                                        MeslekAdi = "Tüccar",
                                        OrtalamaYillikGeliri = 200
                                    });
            cenk.Meslegi.MeslekEkle(new Meslek()
                                    {
                                        MeslekAdi = "Uzay Müh",
                                        OrtalamaYillikGeliri = 100
                                    });
            cenk.Meslegi.MeslekEkle(new Meslek()
                                    {
                                        MeslekAdi = "Öğretmen",
                                        OrtalamaYillikGeliri = 300
                                    });
            cenk.Meslegi.MeslekEkle(new Meslek()
                                    {
                                        MeslekAdi = "Çiftçi",
                                        OrtalamaYillikGeliri = 250
                                    });
            cenk.Meslegi["Çiftçi"] = cenk.Meslegi[1];
        }
    }


    class Meslek
    {
        public string MeslekAdi;
        public int OrtalamaYillikGeliri;

        private Meslek[] _mesleks = new Meslek[3];
        public Meslek this[int idx]
        {
            get { return _mesleks[idx]; }
            set { _mesleks[idx] = value; }
        }
        public Meslek this[string meslekAdi]
        {
            get { return _mesleks.FirstOrDefault(m => m.MeslekAdi == meslekAdi); }
            set
            {
                var guncellenecekMeslek = _mesleks.FirstOrDefault(m => m.MeslekAdi == meslekAdi);
                if (guncellenecekMeslek != null)
                {
                    guncellenecekMeslek = value;
                }
            }
        }


        public void MeslekEkle(Meslek _yeniMeslek)
        {
            int i = 0;
            for (i = 0; i < _mesleks.Length; i++)
            {
                if (_mesleks[i] == null)
                {
                    _mesleks[i] = _yeniMeslek;
                    break;
                }
            }
            // Eğer 3 mesleğide varsa sonuncusunu değiştiriyoruz gelenle
            if (i == _mesleks.Length)
            {
                _mesleks[_mesleks.Length - 1] = _yeniMeslek;
            }
        }
    }

    interface IKisi : ICloneable, IComparable
    {
        Meslek Meslegi { get; set; }
    }

    class Kisi : IKisi
    {
        public string Adi;
        public long TCNo;

        public Meslek Meslegi { get; set; } // IKisi arayüzünden gelir


        public object Clone() // ICloneable arayüzünden gelir
        {// Kopyası oluşturulacak objenin hangi bilgilerinin dönecek nesnede olacağına siz karar verebilirsiniz.

            /*
             * MemberwiseClone() metodu sığ, yüzeysel kopya(shallow copy) oluşturur.
             * Referans tipli bilgileri(prop,field) referanslarıyla(kopyalanan nesnesnin yeni bir örneğiyle değil, adresiyle) kopyalarken,
             * değer tipli bilgilerin değerlerini yeni kopyanın içine yazarak oluşturacaktır.
             * 
             * The MemberwiseClone method creates a shallow copy by creating a new object, and then copying the nonstatic fields of the current object to the new object.
             * If a field is a value type, a bit-by-bit copy of the field is performed.
             * If a field is a reference type, the reference is copied but the referred object is not; therefore, the original object and its clone refer to the same object.
             */
            return this.MemberwiseClone();

            // ya da kendi özgün nesnenizi dönebilirsiniz
            Kisi k = new Kisi()
                     {
                         Adi = this.Adi,
                         Meslegi = new Meslek()
                                   {
                                       MeslekAdi = this.Meslegi.MeslekAdi
                                   }
                     };
            return k;
        }

        public int CompareTo(object obj) // IComparable arayüzünden gelir
        {
            // Gelirleri denk ise 0 dön, ben(this) büyüksem 1, o(obj) büyükse -1 dön
            return this.Meslegi.OrtalamaYillikGeliri == ((Kisi)obj).Meslegi.OrtalamaYillikGeliri
                ? 0
                : this.Meslegi.OrtalamaYillikGeliri > ((Kisi)obj).Meslegi.OrtalamaYillikGeliri
                    ? 1
                    : -1;
        }
    }
}

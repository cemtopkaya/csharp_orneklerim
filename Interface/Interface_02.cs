using System;
using System.Collections;
using System.Collections.Generic;

namespace CSharp_Orneklerim.Interface
{
    public class Interface_02
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
            cenk.Meslegi = new Meslek() {MeslekAdi = "Tüccar", OrtalamaYillikGeliri = 120};


            List<Kisi> lst = new List<Kisi>()
            {
                cenk,cem
            };
            // Sort metodu IComparable arayüzünden gelen CompareTo metodunu çalıştırarak sıralayacak
            lst.Sort();
        }
    }


    class Meslek
    {
        public string MeslekAdi;
        public int OrtalamaYillikGeliri;
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

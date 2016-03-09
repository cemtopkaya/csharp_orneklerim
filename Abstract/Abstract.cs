using System;

namespace CSharp_Orneklerim.Abstract
{
    partial class Abstract
    {
        public static void Calis(string[] args)
        {
            DortIslem d = new Cem();
            Console.WriteLine(d.Cikart(4, 3));
        }
    }
    abstract class DortIslem {  // Abstract sınıflarda interface'ler gibi örneklendirilemez
        // Gövdeli metotlar hem abstract hem de class'larda tanımlanabilir
        public int Topla(int a, int b) { return a + b; }
        // Gövdesiz metotlar hem abstract hem de interface'lerde tanımlanabilir
        public abstract int Cikart(int a, int b);
    }

    class Cem : DortIslem
    {
        public int Carp(int a, int b) { return a*b; }
        public override int Cikart(int a, int b)
        {
            Console.WriteLine("Toplam sonucu: "+Topla(a,b));
            return a - b;
        }
    }

    interface ISahipOlmaYontemi
    { // Nesne yaratılamaz, Kontrattırlar, Miras alınırlar,
        void KurumBul();
    }
    interface ISatinAlma : ISahipOlmaYontemi { }
    interface IKiralama : ISahipOlmaYontemi { }

    class Leasing : IKiralama
    { // Nesne yaratılabilirler(yapıcı metotları olur), miras alırlar, 
        public void KurumBul() { } // kontrat gereği miras aldığı arayüzlerin metotlarını gerçekleştirirler
        public byte KurumunYildizi() { return 5; }
    }

    class Albaraka : Leasing
    {
        public void Hehe() { }
    }
}
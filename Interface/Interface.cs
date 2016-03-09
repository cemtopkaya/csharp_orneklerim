namespace CSharp_Orneklerim.Interface
{
    partial class Interface
    {
        public static void Calis(string[] args)
        {
            ISahipOlmaYontemi o = new Leasing();
            IKiralama o1 = new Leasing(); o = o1;
            Leasing o2 = new YatirimBank(); //o2 = o1;
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

    class YatirimBank : Leasing
    {
        public void Hehe() { }
    }
}

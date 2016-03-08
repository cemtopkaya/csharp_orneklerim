namespace CSharp_Orneklerim.Abstract
{
    partial class Abstract2
    {
        static public void Calis()
        {
            var h = new HemsireIseAl(new HemsireAdayi("Nazlı Taş"));
            h.BolumGorusmesiYap().IseAl();
        }
    }

    abstract class CalisanIseAl
    {  // Abstract sınıflar da interface'ler gibi örneklendirilemez
        // Gövdeli metotlar hem abstract hem de concrete class'larda tanımlanabilir
        public virtual void IseAl()
        {
            // İnsan Kaynakları işlemleri yapılsın(sicilNo, giriş kartı, yemek kartı vs. verilsin)
            // Bilgi Teknolojileri işlemleri yapılsın(eposta açılsın, bilgisayar verilsin vs.)
        }
        // Aday nesnesini tutacak field ile ihtiyaç duyulan seviyedeki tüm aday bilgileri sınıf içinde global bir değişkene atanıyor
        public IAday Aday;
        protected CalisanIseAl(IAday aday)
        { // IAday ile Open/Close prensibine göre başka tiplerin entegrasyonu sağlanıyor
            Aday = aday; // Dependency injection ile IAday tipinde nesneyi alıyor, HAS-A iliişkisi(aggregation) kuruyoruz
            IKGorusmesi();
        }
        // Gövdesiz metotlar hem abstract hem de interface'lerde tanımlanabilir
        public void IKGorusmesi()
        {
            Aday.BasvuruBelgesiDoldur();
            Aday.AdliSicilKaydiGetir();
        }
        public abstract HemsireIseAl BolumGorusmesiYap();
    }

    interface IAday
    { // Temelde her aday tipi bu metotları içermeli
        string AdiSoyadi { get; }
        void BasvuruBelgesiDoldur();
        void AdliSicilKaydiGetir();
    }

    class HemsireAdayi : IAday
    {
        private string adiSoyadi; // AdiSoyadi property'si için back field olarak kullanılacak
        public string AdiSoyadi { get { return adiSoyadi; } private set { adiSoyadi = value; } }

        public HemsireAdayi(string _adiSoyadi) { AdiSoyadi = _adiSoyadi; }
        public void BasvuruBelgesiDoldur() { }
        public void AdliSicilKaydiGetir() { }
        public void IsYeriHemsirelikSertifikasiniGetir() { }
    }
    class HemsireIseAl : CalisanIseAl
    {
        public override HemsireIseAl BolumGorusmesiYap()
        {
            //Olumlu görüşmeyse
            return this;
        }

        public override void IseAl()
        {
            base.IseAl(); // üst sınıf içindeki virtual metodu çalıştır sonra aşağıdakilerini yap
            // Dolabını göster, diğer çalışanlara tanıt, kalite yönetim sistemini anlat vs.
        }

        public HemsireIseAl(IAday aday) : base(aday) { }
    }
}
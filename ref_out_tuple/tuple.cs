using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1 {

    class Program {
        static void Main(string[] args) {
            // ilk versiyonda Item1,2.. isimleriyle property bilgilerinde bilgileri aldık.
            var tupleOgrenci = BulTupleV1(1);
            Console.WriteLine($"No: {tupleOgrenci.Item1}, Adı:{tupleOgrenci.Item2}"); // No: 1, Adı: Güldali

            // Okunması güç olan Item1,2.. yerine ad, no gibi okunabilir dönsün diye yeni Tuple geliştirildi
            var tupleOgrenci2 = BulTupleV2(1);
            Console.WriteLine($"No: {tupleOgrenci2.no}, Adı:{tupleOgrenci2.adi}"); // No: 1, Adı: Güldali
        }

        static Tuple<int, string> BulTupleV1(int id) {
            var bulunanOgrenci = Ogrenci.BulOgrenci(id);
            Tuple<int, string> tupleOgrenci = new Tuple<int, string>(bulunanOgrenci.No, bulunanOgrenci.Adi);
            return tupleOgrenci;
        }

        static (int no, string adi) BulTupleV2(int id) {
            var bulunanOgrenci = Ogrenci.BulOgrenci(id);
            return (bulunanOgrenci.Id, bulunanOgrenci.Adi);
        }
    }

    class Ogrenci {
        public int Id { get; set; }
        public int No { get; set; }
        public string Adi { get; set; }

        public static List<Ogrenci> Ogrenciler() {
            return new List<Ogrenci>
            {
                new Ogrenci(){Adi="Güldali", Id = 1, No = 1714},
                new Ogrenci(){Adi="Memoli", Id = 2, No = 2714}

            };
        }

        public static Ogrenci BulOgrenci(int id) {
            var ogrenciler = Ogrenciler();
            int len = ogrenciler.Count();
            for (var i = 0; i < len; i++) {
                if (id != ogrenciler[i].Id) continue;
                return ogrenciler[i];
            }
            // Hiç bulamadıysan null dön
            return null;
        }
    }

}

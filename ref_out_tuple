using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Quartz;

namespace ConsoleApp1 {
    class Program {
        static void Main(string[] args) {
            Ogrenci refBulunan = null; // REF olunca önce başlatmalı
            Ogrenci outBulunan; // OUT olunca sonra başlatılmalı

            // Bir metot en fazla 1 değer dönebilir, bu yüzden ref ve out var
            int ogrenciNo;
            ogrenciNo = BulRef(1, ref refBulunan);
            ogrenciNo = BulOut(1, out outBulunan);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">
        ///   ID'li öğrenciyi bulmak için kullanılır
        /// </param>
        /// <param name="bulunanOgrenci">
        ///   Metodun içinde değer atanır böylece geldiği yerde kullanılabilir
        /// </param>
        /// <returns></returns>
        static int BulOut(int id, out Ogrenci bulunanOgrenci) {
            // İpucu: out > dışarı demek. 
            // parametrenin değeri dışarıda başlatılacak diye hatırlayın
            bulunanOgrenci = Ogrenci.BulOgrenci(id);
            return bulunanOgrenci != null ? bulunanOgrenci.Id : -1;
            // Daha kestirme yazımı:
            // bulunanOgrenci ?(null değilse).Id(değeri) ??(null ise) -1(değeri)
            // return bulunanOgrenci?.Id ?? -1;
        }
        static int BulRef(int id, ref Ogrenci bulunanOgrenci) {
            bulunanOgrenci = Ogrenci.BulOgrenci(id);
            return -1;
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

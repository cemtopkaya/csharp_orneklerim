using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSharp_Orneklerim.Asenkron_Calling._4_TPL
{
    class Task_01
    {
        /// <summary>
        /// Örneklerde çağırmak için kullanacağız
        /// </summary>
        static private void PrintMessage()
        {
            Console.WriteLine("Hello Task library!");
        }

        // Önce Task sınıfının isim uzayını ekleyeceğiz
        // using System.Threading.Tasks;
        static void Calis(string[] args)
        {
            // 01. Doğrudan çalıştırmanın en basit yolu
            Task.Factory.StartNew(() => { Console.WriteLine("Hello Task library!"); });

            // 02. Delegate kullanarak
            Task task = new Task(delegate { PrintMessage(); });
            task.Start();

            // 03. Lambda ve isimli metot kullanarak
            Task task1 = new Task(() => PrintMessage());
            task.Start();

            // 04. Lambda ve İSİMSİZ metot kullanarak
            Task task2 = new Task(() => { PrintMessage(); });
            task.Start();
            // 05. Task.Run kullanarak (.Net 4.5)
            DoWork();

            // 06. Task.FromResult kullanarak sonuç dönüyoruz (.Net 4.5)
            DoWork1();
        }

        #region 05 - Kod kısmı
        static public async Task DoWork()
        {
            await Task.Run(() => PrintMessage());
        }
        #endregion

        #region 06 - Kod kısmı
        static public async Task DoWork1()
        {
            int res = await Task.FromResult<int>(GetSum(4, 5));
        }

        static private int GetSum(int a, int b)
        {
            return a + b;
        }
        #endregion
    }
}

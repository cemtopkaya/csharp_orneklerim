using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSharp_Orneklerim.Socket_Client_Server
{
    public class Socket_01
    {
        static public void Calis()
        {
            // 01. En basit haliyle istemci sunucu örneği
            //Basit_Client_Server();

            // 02. Çok istemcili sunucu örneği
            List<Task> taskList = new List<Task>();

            Task taskSunucu = Task.Factory.StartNew(Sunucu.Sunucu2);
            taskList.Add(taskSunucu);
            taskList.AddRange(new[]
            {
                Task.Factory.StartNew(Istemci.Istemci2),
                Task.Factory.StartNew(Istemci.Istemci2),
                Task.Factory.StartNew(Istemci.Istemci2),
            });

            Task.WaitAll(taskList.ToArray());
        }

        /// <summary>
        /// 01. En basit haliyle istemci sunucu örneği
        /// </summary>
        static public void Basit_Client_Server()
        {
            var t1 = Task.Factory.StartNew(Istemci.Istemci1);
            var t2 = Task.Factory.StartNew(Sunucu.Sunucu1);
            // Tüm tasklar tamamlanıncaya kadar bekleyeceğiz (bir yerde readline varsa epey bekleriz)
            Task.WaitAll(t1, t2);
        }
    }

    class Sunucu
    {
        /// <summary>
        /// En basit haliyle TCP Listener(sunucu)
        /// </summary>
        static public void Sunucu1()
        {
            var ipAddress = IPAddress.Parse("127.0.0.1");
            var ipEndPoint = new IPEndPoint(ipAddress, 3001);

            // IP:Port dinleyici
            TcpListener listener = new TcpListener(ipEndPoint);

            listener.Start();
            Console.WriteLine("Sunucu > Dinlemeye başladık :{0}", listener.Server.LocalEndPoint);

            // Dışarıdan bir bağlantı talebi gelirse kabul edip gelen değişkeninde tutacağız
            Socket gelen = listener.AcceptSocket();
            Console.WriteLine("Sunucu > Bir bağlantı talebi kabul edildi : {0}", gelen.RemoteEndPoint);

            // Veri geliyor gelenByteBuffer içinde tutalım
            byte[] gelenByteBuffer = new byte[100];
            int toplamByteMiktari = gelen.Receive(gelenByteBuffer);
            Console.WriteLine("Sunucu > Toplam gelen byte : {0}", toplamByteMiktari);

            //----------- Mesaj Gönderen Sunucu Olsun --------------\\
            Console.WriteLine("--------------------------------------------------------------------");
            Console.WriteLine("Sunucu > 2 Sn bekle ben de mesaj göndereceğim istemciye...");
            Thread.Sleep(2000);

            // Sunucuya gelen mesaj
            var gelenBilgininStringHali = Encoding.ASCII.GetString(gelenByteBuffer);
            Console.WriteLine("Sunucu > Alınan bilgi: {0}", gelenBilgininStringHali);

            // Sunucu gelen mesajı tersine çevirip gönderecek
            IEnumerable<char> tersineMesaj = gelenBilgininStringHali.Reverse();
            var byteDizisiTersineMsaj = tersineMesaj.Select(karakter => (byte)karakter).ToArray<byte>();
            gelen.Send(byteDizisiTersineMsaj);

        }

        /// <summary>
        ///  Çok threadli sunucu ile birden fazla bağlantı kabul edebileceğiz
        /// </summary>
        static public void Sunucu2()
        {
            var ipAddress = IPAddress.Parse("127.0.0.1");
            var ipEndPoint = new IPEndPoint(ipAddress, 3001);
            int istemciSirasi = 1;
            TcpListener listener = new TcpListener(ipEndPoint);
            List<Task> listTasks = new List<Task>();

            // Sunucuyu başlatalım
            listener.Start();

            while (true)
            {
                Console.WriteLine(istemciSirasi + ". İstemciyi bekliyorum");
                Socket istemciSocketi = listener.AcceptSocket();

                int iSocketId = istemciSirasi++;
                Console.WriteLine("Socket ID: " + iSocketId);
                Task taskYeniIstemci = Task.Factory.StartNew(() =>
                {
                    Console.WriteLine(iSocketId + ". Socket > için mesaj gönderip alacağız");
                    string hosgeldinMesaji = iSocketId + " numaralı istemci Hoşgeldin";
                    istemciSocketi.Send(Encoding.ASCII.GetBytes(hosgeldinMesaji));

                    var bConnected = true;
                    while (bConnected)
                    {
                        byte[] barrMesaj = new byte[100];
                        int len = istemciSocketi.Receive(barrMesaj);

                        string sMesaj = Encoding.ASCII.GetString(barrMesaj);
                        Console.WriteLine(sMesaj);

                        bConnected = Convert.ToInt32(sMesaj[2]) > 0;//1-
                    }
                });

                listTasks.Add(taskYeniIstemci);

                Thread.Sleep(1500);
            }
        }
    }

    public class Istemci
    {
        static public void Istemci2()
        {
            TcpClient client = new TcpClient();
            byte[] barrHosgeldin = new byte[100];
            int ID;            
            bool bBagli = true;
            string sHosgeldin;
            Random rnd = new Random();


            Console.WriteLine("Istemci > 1 Sn veriyorum sunucuya...");
            Thread.Sleep(2000);


            client.Connect("localhost", 3001);
            client.GetStream().Read(barrHosgeldin, 0, 100);
            sHosgeldin = Encoding.ASCII.GetString(barrHosgeldin);
            ID = Convert.ToInt32(sHosgeldin[0].ToString());
            Console.WriteLine("Istemci > Bağlantı kuruldu. ID: {0}", ID);

            while (bBagli)
            {
                NetworkStream agAkisi = client.GetStream();
                // Akışa yazalım mesajımızı
                int iMesaj = rnd.Next(0, 10);
                bBagli = iMesaj != 0;
                string sMesaj = ID + "-" + iMesaj.ToString();
                Console.WriteLine("Istemci > Mesaj gönderildi...: {0}", sMesaj);
                agAkisi.Write(ASCIIEncoding.ASCII.GetBytes(sMesaj), 0, 3);

                Thread.Sleep(1500);
                if (!bBagli)
                {
                    Console.WriteLine(ID + " Numaralı istemcinin bağlantısı kesiliyor..");
                    client.Close();
                }
            }
        }

        static public void Istemci1()
        {

            Console.WriteLine("Istemci > 2 Sn veriyorum sunucuya...");
            Thread.Sleep(2000);

            TcpClient client = new TcpClient();
            client.Connect("localhost", 3001);

            Console.WriteLine("Istemci > Bağlantı kuruldu.");

            /* 
             * Dosyadan geliyor olsaydı bilgiler(byte byte akarak) FileStream, 
             * ağ üstünden geliyor olsaydı NetworkStream
             * bellek(RAM) üstünden gelselerdi MemoryStream tipinde bir AKIŞIMIZ(STREAM) olacaktı.
             * Ve bu akışın(nehrin) üstüne oyuncak ördeklerimizi(bilgilerimizi byte byte) koyacak 
             * ve diğer taraftan alacaktık
             */

            NetworkStream agAkisi = client.GetStream();
            string gidecekMesaj = "Merhaba kanki";
            // Akışa yazalım mesajımızı
            agAkisi.Write(ASCIIEncoding.ASCII.GetBytes(gidecekMesaj), 0, gidecekMesaj.Length);
            Console.WriteLine("Istemci > Mesaj gönderildi...");


            //----------- Mesaj Gönderen Sunucu Olsun --------------\\
            Console.WriteLine("--------------------------------------------------------------------");
            Console.WriteLine("İstemci > Sunucudan geleni dinlemeye koyulayım...");

            byte[] gelenMesajByteDizisi = new byte[100];

            #region 01. senkron okuma için 02'yi kapat
            // Akıştan okuyalım mesajımızı
            int gelenByteDizisininUzunlugu = agAkisi.Read(gelenMesajByteDizisi, 0, 100);
            Console.WriteLine("İstemci > Toplam gelen byte : {0}", gelenByteDizisininUzunlugu);
            Console.WriteLine("İstemci > Gelen mesaj : {0}", Encoding.ASCII.GetString(gelenMesajByteDizisi));
            #endregion

            #region 02. asenkron okuma için 01'i kapat
            // Akıştan asenkron olarak okuyalım mesajımızı ve bitmeden devam etmemek için taskimiz.Wait diyelim 
            Task<int> taskGelenOkumaSonucu = agAkisi.ReadAsync(gelenMesajByteDizisi, 0, 100);
            taskGelenOkumaSonucu.Wait();
            Console.WriteLine("İstemci > Toplam gelen byte : {0}", taskGelenOkumaSonucu.Result);
            Console.WriteLine("İstemci > Gelen mesaj : {0}", Encoding.ASCII.GetString(gelenMesajByteDizisi));
            #endregion

            // İstemci için çalışan Task ekrandan bir satır okuyuncaya kadar tamamlanmış olmayacağı için
            // Task.WaitAll ile konsol ekranı gitmeyecek.
            Console.ReadLine();
        }
    }

}

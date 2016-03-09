using System;

namespace CSharp_Orneklerim.Delegate_Event
{
    /*
     *  delegate Bir tip tanımlamasıdır, değişken deklarasyonu değildir. 
     *  Hiçbir tip tanımlaması metot içinde tanımlanmaya müsade edilmez. 
     *  Tip tanımlamaları sadece sınıf(class) ve isim uzayı kapsamında yapılabilir
     */
    public delegate int TekParametreliIntDonuslu(int a);
    public delegate void TekParametreliDonussuz(int a);
    public delegate void ParametresizDonussuz();

    class Lambda
    {
        // delegate tipler static tanımlanamaz ancak delegate tipten static bir değişken tanımlanabilir (STATIC_VAR)
        public static TekParametreliIntDonuslu static_delegate_degisken;

        public static void Calis()
        {
            //------------------------------------------------------------------------------------------
            // Tek parametre alan ve int dönen delegate tipinden örneklenmiş l1 değişkeni 
            // ve l1 değişkenine değer olarak atanan TekParametreIntDonuslu tipinden türetilmiş bir nesne
            TekParametreliIntDonuslu l1 = new TekParametreliIntDonuslu(f_tekParametreIntDonuslu);
            TekParametreliIntDonuslu l2 = f_tekParametreIntDonuslu;

            // Tek parametre alan ve int dönen delegate tipinden örneklenmiş l3 değişkeni 
            // ve l3 değikenine değer olarak atanan lambda ifadeli metot tanımlaması
            TekParametreliIntDonuslu l3 = (c) =>
                                        {
                                            Console.WriteLine("lambda ifadesi içinde parametre: " + c);
                                            return c *= c;
                                        }; // l3(3); > 9

            //------------------------------------------------------------------------------------------
            // Bir temsilci nesnesi aynı anda birden fazla metodu temsil edebilir.
            l1 += l2;
            l1 += l3;
            Console.WriteLine("Temsilci değişkenin içindeki son metoddan dönen sonuç: " + l1(12));
            object state = null;
            l1.BeginInvoke(5, (a) =>
                              {
                                  if(a.IsCompleted)
                                  Console.WriteLine("sdfsf> " + a.IsCompleted);
                              }, state);

            TekParametreliDonussuz v = (a)=>Console.WriteLine("dönüşsüz");
            ParametresizDonussuz p = () => { Console.WriteLine("parametresiz dönüşsüz lambda exp"); };
            //AsyncCallback asy = new AsyncCallback();
            p.BeginInvoke((a) => { }, null);

            //var b = Delegate.CreateDelegate(,);

            //------------------------------------------------------------------------------------------
            // STATIC_VAR:
            static_delegate_degisken = (a) => a += 1;
            static_delegate_degisken(3); // 4 dönecektir
        }

        static int f_tekParametreIntDonuslu(int _param)
        {
            Console.WriteLine("Statik f_tekParametreIntDonuslu metodu içinde gelen _param: " + _param);
            return _param *= _param;
        }
    }

}

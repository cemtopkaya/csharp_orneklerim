using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CSharp_Orneklerim.Reflection.DenemeAlani
{
    class GizliSinif
    {
        private string gizliString = "String yazoor be";
        public string Adi;


        private GizliSinif(string _adi)
        {
            this.Adi = _adi;
        }
        
        public GizliSinif() { }
    }
}

namespace CSharp_Orneklerim.Reflection
{
    public class Reflection
    {
        public static void Calis()
        {
            // 1. Kısım - Assembly_Giris --------------
            //Assembly_Giris();

            // 2. Kısım - Nesne Oluşturma -------------
            Yansimayla_Nesne_Yaratma();

            // 2. Kısım - Nesnenin Özelliğini Çekme ---
            Yansimayla_Nesnenin_Fieldini_Cagirma();
        }

        #region ---------- 1. Kısım - Assembly_Giris --------------
        public static Assembly Assembly_Giris()
        {
            Assembly assembly;
            assembly = Assembly.GetExecutingAssembly();
            assembly = typeof(CSharp_Orneklerim.Reflection.Reflection).Assembly;
            // Assembly'i elde etmek
            // Assembly adını/yolunu biliyorsak
            assembly = Assembly.Load("CSharp_Orneklerim");
            assembly = Assembly.LoadFrom("file:///C:/Users/cem.topkaya/Source/Repos/csharp_orneklerim/bin/Debug/CSharp_Orneklerim.EXE");

            string assembly_name;
            // Assembly'nin adını(Name'ini, FullyQualyfiedName'i değil) öğrenmek
            assembly_name = assembly.GetName().Name;
            assembly_name = AssemblyName.GetAssemblyName(assembly.Location).Name;

            // Name            : "CSharp_Orneklerim.exe"	
            // FullName	       : "CSharp_Orneklerim, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"
            // Location	       : "C:\\Users\\cem.topkaya\\Source\\Repos\\csharp_orneklerim\\bin\\Debug\\CSharp_Orneklerim.exe"
            // EscapedCodeBase : "file:///C:/Users/cem.topkaya/Source/Repos/csharp_orneklerim/bin/Debug/CSharp_Orneklerim.EXE"

            return assembly;
        }
        #endregion
        
        #region ---------- 2. Kısım - Nesne Oluşturma -------------
        public static object Yansimayla_Nesne_Yaratma()
        {
            object ornekNesne = null;
            Type typeGizliSinif = Assembly_Giris().GetTypes().First(t => t.FullName.Equals("CSharp_Orneklerim.Reflection.DenemeAlani.GizliSinif"));
            // Default Constructor üstünden nesne oluşturmak için Activator.CreateInstance(tipi) metodunu kullanırız
            // ya varsayılan yapıcı metodu yoksa !
            // Eğer default constructor yoksa "No parameterless constructor defined for this object." hatasını alırız
            // O zaman var mı yok kontrolü yapmalıyız
            // BindingFlags ile ilgili okunabilir > http://stackoverflow.com/a/1544992/104085
            BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic;
            ConstructorInfo parametreliCons = typeGizliSinif.GetConstructors(bindingFlags)
                .OrderByDescending(c => c.GetParameters().Length)
                .FirstOrDefault();

            var bVarmi =
                // Public default constructor varsa
                (typeGizliSinif.GetConstructor(Type.EmptyTypes) != null)
                || // veya
                // Private 0 Parametreli constructor varsa
                typeGizliSinif.GetConstructors(bindingFlags).Any(c => c.GetParameters().Length == 0);
            ornekNesne = bVarmi
                ? Activator.CreateInstance(typeGizliSinif, bindingFlags,null,null,null)
                // demek ki default constructor yok biz de diğer yapıcılardan birini çağıracağız
                // Public|Private, en çok parametre alanı en üste getirerek nesne yaratmaya çalışalım
                : new Func<ConstructorInfo, object>((ConstructorInfo _constructorInfo) =>
                                                    {
                                                        if (_constructorInfo == null)
                                                        { // Constructor falan yok demektir
                                                            return null;
                                                        }

                                                        // değişken, dönüşkenler
                                                        object result = null;
                                                        object[] arrParam = new object[0];

                                                        // Şimdi parametreli bir yapıcı metot çalıştıracağız o halde parameterlerini bulup bu parametre tiplerine göre ilk değerlerini oluşturalım
                                                        // Böylece parametre listesini hazır etmiş oluruz ve CreateInstance metoduyla nesne yaratımına geçebiliriz
                                                        arrParam = _constructorInfo.GetParameters().Select(pi =>
                                                                                                           {
                                                                                                               Type parameterType = pi.ParameterType;

                                                                                                               object deger = Nullable.GetUnderlyingType(parameterType) == null && parameterType.IsValueType
                                                                                                                   // değer tipiyse CreateInstance ile bir örneğini yaratalım
                                                                                                                   ? Activator.CreateInstance(parameterType)
                                                                                                                   // Bu parametre tipi referans tipiyse direk null diyebiliriz 
                                                                                                                   : null;

                                                                                                               return deger;
                                                                                                           }).ToArray();

                                                        // result = Activator.CreateInstance(typeGizliSinif, new object[0]) // "public default constructor(parametresiz)" calistirir
                                                        // Artık bu yapıcı metot public|private olabilir ama Instance
                                                        result = Activator.CreateInstance(typeGizliSinif, bindingFlags, null, arrParam, null);

                                                        // dönen değer
                                                        return result;
                                                    })(parametreliCons);

            return ornekNesne;
        } 
        #endregion

        #region ---------- 3. Kısım - Nesnenin özelliğini çekme ---
        public static void Yansimayla_Nesnenin_Fieldini_Cagirma()
        {
            var ornekNesne = Yansimayla_Nesne_Yaratma();
            Type typeGizliSinif = ornekNesne.GetType();

            FieldInfo[] arrFieldInfos = typeGizliSinif.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
            foreach (FieldInfo fieldInfo in arrFieldInfos.Where(f => f.IsPrivate))
            {
                Console.WriteLine(fieldInfo.GetValue(ornekNesne)); // String yazoor be 
                fieldInfo.SetValue(ornekNesne, "Integer olmasın !");
                Console.WriteLine(fieldInfo.GetValue(ornekNesne)); // Integer olmasın !
            }
        } 
        #endregion
    }
}

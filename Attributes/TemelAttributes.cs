using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Orneklerim.Attributes
{
    class TemelAttributes
    {
        static void Calis()
        {
            Test(); // Method 'CSharp_Orneklerim.Attributes.Test' is obsolote yazacak.
        }

        [Obsolete]
        static void Test() { }
    }

    /*
     * Bir Attribute tipi tanımladığımızda kimler için geçerli olacağına karar vermeliyiz aksi halde derlerken hata alırız.
     *    public enum AttributeTargets
     *    {
     *       Assembly    = 0x0001,
     *       Module      = 0x0002,
     *       Class       = 0x0004,
     *       Struct      = 0x0008,
     *       Enum        = 0x0010,
     *       Constructor = 0x0020,
     *       Method      = 0x0040,
     *       Property    = 0x0080,
     *       Field       = 0x0100,
     *       Event       = 0x0200,
     *       Interface   = 0x0400,
     *       Parameter   = 0x0800,
     *       Delegate    = 0x1000,
     *       ReturnValue = 0x2000,
     *       All = Assembly | Module | Class | Struct | Enum | Constructor | 
     *          Method | Property | Field | Event | Interface | Parameter | 
     *          Delegate | ReturnValue
     *    }
     *    
     *    Assembly : (Ref: http://www.emrahuslu.com/post/2011/11/09/NETde-Assembly-Kavram%C4%B1-ve-CIL.aspx | http://stackoverflow.com/a/9407429/104085)
     *    - Bir assembly'den azını dağıtamazsınız. 
     *    - En küçük yüklenebilir birimdir. İçinde c#,vb,c++... dillerinde yazdığınız kodun CIL halini barındırır. CIL Kodu, assembly ilgili platformda çalıştırılıncaya kadar derlenmez!
     *    - METADATA(üst veri) ile assembly içindeki tüm tipleri, tiplerin üyeleri gibi üst bilgileri tutar. Bir kitabın içindekiler kısmı gibidir. 
     *    - MANIFEST ise Versiyon, kültür bilgisi, kısa açıklama, başka assembly'lere referanslar gibi bilgilerin bulunduğu yerdir. 
     *        Çok dosyalı assembly, birden fazla assembly içerirler(bu durumda artık herbirine modul denir) ve birinci modül(primary module) diğer modüllerin referans bilgilerini de tutar. 
     *        Birincilin dışında kalan tüm modüller normal olarak kendi manifestolarını tutmaya devam ederler.
     *        Manifest works at the higher level ie on assembly level to check their strong Name,versions and all., and either to accept the assembly or to refuse it as per the hash code generated. Metadata is all about the information of classes,interface etc as i suggested in the answer.
     *    - Assembly bir veya daha fazla dosya(fiziksel dosya) içerebilir. Bu dosyalar kaynak dosyalar(resource), netmodule veya native dll'ler olabilir. 
     *    - Daima assembly manifest içerir. 
     *    * Assembly Manifest:
     *      - Assembly hakkında bir üst veridir(metadata)
     *    - Bir tip assembly dışında tanımlanamaz! 
     */


    [AttributeUsage(AttributeTargets.Assembly)]
    class Benim_Attribute
    {

    }
}

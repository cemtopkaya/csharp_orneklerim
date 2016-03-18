using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharp_Orneklerim.Indexer_Indeksleyiciler
{
    /// <summary>
    /// Neden struct olarak tanımlıyoruz?
    /// Çünkü oluşturacağımız tip, oldukça hafif bir tip yaratmak istiyoruz. Yani class'la tanımlamak için fazlaca hafif.
    /// </summary>
    public struct IntBits
    {
        /// <summary>
        /// Bir integer sayıyı tutacağız bu backfield içinde 
        /// </summary>
        int bits;

        bool this[int index]
        {
            get
            {
                return (bits & (1 << index)) != 0;
            }
            set
            {
                if (value) // Eğer true ise
                {
                    bits |= (1 << index);
                }
                else
                {
                    bits &= (1 << index);
                }
            }
        }
    }

    class Indexer02
    {
        public static void Calis()
        {
        }
    }
}

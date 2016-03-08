using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Xml.Serialization;

namespace CSharp_Orneklerim.De_Serialization
{
    [Serializable]
    public class Basit
    {
        public char c = 'a';
        public byte b = (byte)'a';
        private char privateAmaBinaryFormatterSerilestirir_SoapFormatterSerilestirmez = (char)65;

        [XmlElement(DataType = "string", ElementName = "ADI")] // XSD tanımlamaları
        public string Adi = "isimsiz";

        [NonSerialized] // BinaryFormatter private'a bakmaksızın serileştirdiği için [NonSerialized] ile işaretliyoruzki, serileştirmesin
        private byte NonSerialized_isaretli_BinaryFormatter_Serilestirmez = 1;
    }

    public class DeSerialization
    {
        public static void Calis()
        {
            var b = BinaryFormatla_seri_deseri_lestir();
            var s = SoapFormatla_seri_deseri_lestir();
            var x = XML_seri_deseri_lestir();
            /*
             * Binary Serialization
             * Serialization can be defined as the process of storing the state of an object to a storage medium. 
             * During this process, 
             *   the public and private fields of the object 
             *   and the name of the class, 
             *   including the assembly containing the class, 
             * are converted to a stream of bytes, which is then written to a data stream. 
             * When the object is subsequently deserialized, an exact clone of the original object is created.
             * When implementing a serialization mechanism in an object-oriented environment, you have to make a number of tradeoffs between ease of use and flexibility. 
             * The process can be automated to a large extent, provided you are given sufficient control over the process. 
             */

            /*
             * XML and SOAP Serialization
             * XML serialization converts (serializes) the public fields and properties of an object, 
             * or the parameters and return values of methods, 
             * into an XML stream that conforms to a specific XML Schema definition language (XSD) document. 
             * XML serialization results in strongly typed classes with public properties and fields that are converted to a serial format (in this case, XML) for storage or transport.
             */
        }

        private static Basit XML_seri_deseri_lestir()
        {
            Type t = Type.GetType("ConsoleApplication7.Basit");
            XmlSerializer ser = new XmlSerializer(t);
            FileStream fs = new FileStream("XmlSer.xml", FileMode.OpenOrCreate);
            ser.Serialize(fs, new Basit());
            fs.Close();

            Basit x = (Basit)ser.Deserialize(new FileStream("XmlSer.xml", FileMode.Open));
            return x;
        }

        private static Basit SoapFormatla_seri_deseri_lestir()
        {
            SoapFormatter soapFmt = new SoapFormatter();
            FileStream fs = new FileStream("basit.xml", FileMode.OpenOrCreate);
            soapFmt.Serialize(fs, new Basit());
            fs.Close();

            FileStream fs1 = new FileStream("basit.xml", FileMode.Open);
            Basit s = (Basit)soapFmt.Deserialize(fs1);
            return s;
        }

        private static Basit BinaryFormatla_seri_deseri_lestir()
        {
            BinaryFormatter binaryFmt = new BinaryFormatter();
            FileStream fs = new FileStream("basit.bin", FileMode.OpenOrCreate);
            binaryFmt.Serialize(fs, new Basit());
            fs.Close();

            FileStream fs1 = new FileStream("basit.bin", FileMode.Open);
            Basit b = (Basit)binaryFmt.Deserialize(fs1);
            return b;
        }
    }
}

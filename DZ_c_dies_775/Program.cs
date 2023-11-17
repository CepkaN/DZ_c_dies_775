using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using System.IO;

namespace DZ_c_dies_775
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string str = "Вот дом, Который построил Джек." +
                " А это пшеница, Которая в тёмном чулане" +
                " хранится В доме, Который построил Джек. " +
                "А это веселая птица-синица, " +
                "Которая часто ворует пшеницу, " +
                "Которая в тёмном чулане хранится В доме, Который построил Джек.";
            str = str.ToLower();
            string[] stroka1 = str.Split(new char[] { ' ',',','.' }, StringSplitOptions.RemoveEmptyEntries);
            SortedSet<string>SEET= new SortedSet<string>();
            foreach(var s in stroka1)
            {
                SEET.Add(s);
            }
            string Lala = " ";

            Console.WriteLine("Количество каждого слова : ");
            foreach (var s in SEET)
            {
                //Console.WriteLine("Количество слова " + s + " = " +
                 //   (from f in stroka1 where f== s select f).Count());
                Lala = Lala + "Количество слова " + s + " = " + (from f in stroka1 where f == s select f).Count() + "\n";
            }
            string via1 = "C:\\Users\\User\\Desktop\\C#\\DZ_c_dies_775\\DZ_c_dies_775\\teeee.json";
            string via2 = "C:\\Users\\User\\Desktop\\C#\\DZ_c_dies_775\\DZ_c_dies_775\\teeex.xml";
            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true
            };

            string json = JsonSerializer.Serialize(Lala, options);

            using (FileStream fstream = new FileStream(via1, FileMode.Append, FileAccess.Write))
            {
                byte[] writeText = Encoding.Unicode.GetBytes(json);
                fstream.Write(writeText, 0, writeText.Length);
            }
            XmlSerializer xml = new XmlSerializer(typeof(string));
            using (FileStream fstream = new FileStream(via2, FileMode.Create))
            {
                xml.Serialize(fstream, Lala);
            }
            UnicodeEncoding UNI = new UnicodeEncoding();

            using (StreamReader reader = new StreamReader(via1, UNI))
            {
                Console.WriteLine(reader.ReadLine());
            }

            XmlDocument doc = new XmlDocument();
            doc.Load(via2);
            XmlElement root = doc.DocumentElement;
            if (root != null)
            {
                foreach (XmlText node in root)
                {
                    Console.WriteLine(node?.Value);
                }
            }
        }
    }
}
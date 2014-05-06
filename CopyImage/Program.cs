using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopyImage
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            while (true)
            {
                if (!Directory.Exists("auswahl")) Directory.CreateDirectory("auswahl");
                Console.Write("Bitte Bildnummern (mit Komma getrennt, leer für Ende) eingeben: ");
                var snumbers = Console.ReadLine();
                if (String.IsNullOrWhiteSpace(snumbers))
                    break;

                var numbers = snumbers.Split(',');
                Console.WriteLine("Auswahl treffen:");
                Console.WriteLine("[1] CR2");
                Console.WriteLine("[2] JPG");
                Console.WriteLine("[3] CR2 und JPG");
                Console.Write(">> ");
                
                var auswahl = Console.ReadKey();
                Console.WriteLine();

                var patterns = new List<String>();
                switch (auswahl.Key)
                {
                    case ConsoleKey.D1:
                        {
                            patterns.Add("CR2");
                            break;
                        }
                    case ConsoleKey.D2:
                        {
                            patterns.Add("JPG");
                            break;
                        }
                    case ConsoleKey.D3:
                        {
                            patterns.AddRange(new[] { "JPG", "CR2" });
                            break;
                        }
                }
                
                foreach (var file in numbers.SelectMany(num => patterns, (num, p) => string.Format("*MG_{0}.{1}", num, p)))
                {
                    CopyFile(file, "auswahl");
                }
            }
        }

        private static void CopyFile(string pattern, string to)
        {
            var files = Directory.GetFiles(".", pattern);
            foreach (var file in files)
            {
                var dest = Path.Combine(to, file);
                try
                {
                    Console.WriteLine("Kopiere Datei {0}", file);
                    File.Copy(file, dest);
                }
                catch (Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Fehler: {0}", e.Message);
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }
    }
}

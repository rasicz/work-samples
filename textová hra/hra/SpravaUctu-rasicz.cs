using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hra
{
    class SpravaUctu
    {
        
        string cesta = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        string cestaKUctum = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\War Age offline\ucty";
        public SpravaUctu()
        {
            Directory.CreateDirectory(cesta + @"\War Age offline");
            Directory.CreateDirectory(cestaKUctum);
            Console.WriteLine("poloha: " + cesta);
        }
        public void NovyUcet()
        {
            string jmeno = "";
            bool jmenoVybrano = false;
            bool hesloVybrano = false;
            string vstup;
            string heslo = "";
            Console.WriteLine("Zadejte název účtu");
            while (jmenoVybrano == false)
            {
                vstup = Console.ReadLine();
                if (File.Exists(cestaKUctum + "\\" + vstup + ".txt")) {
                    Console.WriteLine("jméno je již zabrané");
                }
                else
                {
                    var soubor = File.Create(cestaKUctum + "\\" + vstup + ".txt");
                    soubor.Close();
                    Console.WriteLine("jmeno bylo vybrano");
                    jmeno = vstup;
                    jmenoVybrano = true;
                    
                }
            }
            Console.Clear();
            Console.WriteLine("zadejte přihlašovací heslo");
            while (hesloVybrano == false)
            {
                vstup = Console.ReadLine();
                if(vstup.Length > 4 && vstup.Length < 60)
                {
                    heslo = vstup;
                    Console.WriteLine();
                    hesloVybrano = true;
                }
            }
            using(StreamWriter zapisovac = new StreamWriter(cestaKUctum + "\\" + jmeno + ".txt"))
            {
                zapisovac.WriteLine("Heslo: " + heslo);
                zapisovac.WriteLine("testování nigga");
                zapisovac.WriteLine("ještě jedno");
            }
            
        }
    }
}

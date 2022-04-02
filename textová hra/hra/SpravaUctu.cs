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
        public string hrac1;
        public string hrac2;
        public bool bHrac1;
        string cesta = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        string cestaKUctum = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\War Age offline\ucty";
        public SpravaUctu()
        {
            Directory.CreateDirectory(cesta + @"\War Age offline");
            Directory.CreateDirectory(cestaKUctum);
        }
        public void NovyUcet()
        {
            string jmeno = "";
            bool jmenoVybrano = false;
            bool hesloVybrano = false;
            string vstup;
            string heslo = "";
            Console.Clear();
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
                    hesloVybrano = true;
                }
                else if(vstup.Length <= 4 )
                {
                    Console.WriteLine("zvolte si delší heslo");
                }
            }
            using(StreamWriter zapisovac = new StreamWriter(cestaKUctum + "\\" + jmeno + ".txt"))
            {
                zapisovac.WriteLine("Heslo: " + heslo);
                zapisovac.WriteLine("Level: 0");
                zapisovac.WriteLine("PH: 0");
                zapisovac.WriteLine("PV: 0");
                zapisovac.WriteLine("PP: 0");
                zapisovac.WriteLine("");
                zapisovac.WriteLine("BaS: 0");
            }
            Console.Clear();
            Console.WriteLine("účet byl úspěšně vytvořen");
            Console.ReadKey();
            Console.Clear();
        }
        public void PrihlasitSe()
        {
            string jmeno = "";
            string heslo = "";
            string hesloVstup = "";
            byte cislo = 0;
            bool cisloVybrano = false;
            bool jmenoVybrano = false;
            bool hesloVybrano = false;
            bool zruseno = false;
            string vstup;
            Console.Clear();
            Console.WriteLine("vyberte číslo hráče za kterého chcete hrát:\n1: první hráč\n2: druhý hráč\n0: zrušit");
            while (cisloVybrano == false)
            {
                vstup = Console.ReadLine();
                if (vstup == "1") { bHrac1 = true; cisloVybrano = true; }
                if (vstup == "2") { bHrac1 = false; cisloVybrano = true; }
                if (vstup == "0")
                {
                    zruseno = true;
                    hesloVybrano = true;
                    jmenoVybrano = true;
                    cisloVybrano = true;
                    //zrušení
                }
            }
            Console.Clear();
            while (jmenoVybrano == false){
                Console.WriteLine("zadejte přihlašovací jméno\nzadejte 0 pro zrušení přihlášení");
                vstup = Console.ReadLine();
                if (vstup == "0")
                {

                    zruseno = true;
                    hesloVybrano = true;
                    jmenoVybrano = true;
                    //zrušení
                }
                if (File.Exists(cestaKUctum + "\\" + vstup + ".txt"))
                {
                    jmeno = vstup;
                    jmenoVybrano = true;
                }
                else if(vstup != "0")
                {
                    Console.Clear();
                    Console.WriteLine("neplatné uživatelské jméno");
                    Console.ReadKey();
                }
                Console.Clear();
            } 
            vstup = "";
            Console.WriteLine("zadejte přihlašovací heslo");
            while (hesloVybrano == false)
            {
                vstup = Console.ReadLine();
                if (vstup == "0")
                {
                    zruseno = true;
                    hesloVybrano = true;
                    //zrušení
                }
                using (StreamReader ctecka = new StreamReader(cestaKUctum + "\\" + jmeno + ".txt"))
                {
                    hesloVstup = ctecka.ReadLine();
                    hesloVstup = hesloVstup.Remove(0,7);
                    
                }
                if (vstup == hesloVstup)
                {
                    hesloVybrano = true;
                    zruseno = false;
                }
                
            }
            Console.Clear();
            if (zruseno == false)
            {
                if (bHrac1) hrac1 = jmeno; else hrac2 = jmeno;
                Console.WriteLine("úspěšně jste se přihlásily");
                Console.ReadKey();
                MenuUctu();
            }
        }
        public void MenuUctu()
        {
            string vstup = "";
            while (vstup != "5")
            {
                Console.WriteLine("1: zobrazit statisktiky\n2: zobrazit inventář\n3: změnit heslo\n4: odhlásit se\n5: zpátky do menu");
                vstup = Console.ReadLine();
                switch (vstup)
                {
                    case "1":
                        Vlastnosti();
                        break;
                    case "2":
                        break;
                    case "3":
                        break;
                    case "4":
                        break;
                }
            }
        }
        public void Vlastnosti()
        {
            string hrac;
            string meziPamet = "";
            if (bHrac1) hrac = hrac1; else hrac = hrac2;
            Console.Clear();
            using (StreamReader ctecka = new StreamReader(cestaKUctum + "\\" + hrac1 + ".txt"))
            {
                ctecka.ReadLine();
                Console.WriteLine(ctecka.ReadLine());

                meziPamet = ctecka.ReadLine();
                meziPamet = meziPamet.Remove(0, 4);
                Console.WriteLine("Počet her: " + meziPamet);

                meziPamet = ctecka.ReadLine();
                meziPamet = meziPamet.Remove(0, 4);
                Console.WriteLine("Počet výher: " + meziPamet);

                meziPamet = ctecka.ReadLine();
                meziPamet = meziPamet.Remove(0, 4);
                Console.WriteLine("Počet proher: " + meziPamet);

            }
            Console.ReadKey();
        }
        public void KonecHry(short vyherce)
        {
            string vstup = "";
            short level = 0;
            short pocetHer = 0;
            short pocetVyher = 0;
            short pocetProher = 0;
            using (StreamReader ctecka = new StreamReader(cestaKUctum + "\\" + hrac1 + ".txt"))
            {
                ctecka.ReadLine();
                vstup = ctecka.ReadLine();
                vstup.Remove(0, 7);
                Int16.TryParse(vstup, out level);

                vstup = ctecka.ReadLine();
                vstup.Remove(0, 4);
                Int16.TryParse(vstup, out pocetHer);

                vstup = ctecka.ReadLine();
                vstup.Remove(0, 4);
                Int16.TryParse(vstup, out pocetVyher);

                vstup = ctecka.ReadLine();
                vstup.Remove(0, 4);
                Int16.TryParse(vstup, out pocetProher);
            }
            using (StreamWriter zapisovac = new StreamWriter(cestaKUctum + "\\" + hrac1 + ".txt"))
            {

            }
            
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hra
{
    class Menu
    {
        bool konecKola = false;
        short pocetKol = 0;
        short pocetBojovniku = 5;
        short pocetTrid = 10;
        byte vyhra = 0;
        SpravaUctu spravaUctu = new SpravaUctu();
        Jednotka[] jednotky = new Jednotka[15];
        Jednotka[] team1 = new Jednotka[7];
        Jednotka[] team2 = new Jednotka[7];
        
        bool hraVPrubehu = false;
        Random gen = new Random(Guid.NewGuid().GetHashCode());
        public string[] listJmen = new string[] {
            "Milva"
            , "Regis"
            , "Jarre"
            , "Essi"
            , "Zelest"
            , "Dainty"
            , "Brick"
            , "Rusty"
            , "Sigfried"
            , "Reuwen"
            , "Boreas"
            , "Kalkstein"
            , "Istredd"
            , "Yurga"
            , "Braenn"
        };
        public string jList()
        {
            return listJmen[gen.Next(0, listJmen.Length)];
            
        }
        public void GenJednotky(int pocetOpakovani)
        {
            int i = 0;
            while (pocetOpakovani > 0)
            {
                switch (gen.Next(1, (pocetTrid + 1))) //9
                {
                    case 1:
                        jednotky[i] = new Rytir();
                        break;
                    case 2:
                        jednotky[i] = new Doktor();
                        break;
                    case 3:
                        jednotky[i] = new Lucistnik();
                        break;
                    case 4:
                        jednotky[i] = new Alchymista(pocetBojovniku);
                        break;
                    case 5:
                        jednotky[i] = new Stitar();
                        break;
                    case 6:
                        jednotky[i] = new Zoldner();
                        break;
                    case 7:
                        jednotky[i] = new Sekernik();
                        break;
                    case 8:
                        jednotky[i] = new Pikenyr();
                        break;
                    case 9:
                        jednotky[i] = new Spion();
                        break;
                    case 10:
                        jednotky[i] = new General();
                        break;
                }
                pocetOpakovani--;
                i++;
            }


        }

        public void Nastaveni()
        {
            string vstup = "";
            while (vstup != "3")
            {

                Console.Clear();
                Console.WriteLine("Nastaveni:");
                Console.WriteLine("1: počet bojovníků\n2: složitost hry");
                Console.WriteLine("3: zpátky");
                vstup = Console.ReadLine();
                switch (vstup)
                {
                    case "1":
                        PocetBojovniku();
                        break;
                    case "2":
                        SlozitostHry();
                        break;
                }
            }

            Console.Clear();
        }
        public void PocetBojovniku()
        {

            Console.Clear();
            Console.WriteLine("Nastaveni:\nnastavení počtu bojovníků:");
            Console.WriteLine("1: 3\n2: 5   (původní)\n3: 7");
            Console.WriteLine("4: zpátky");
            string vstup = "";
            while (vstup != "4")
            {
                vstup = Console.ReadLine();
                switch (vstup)
                {
                    case "1":
                        pocetBojovniku = 3;
                        Console.WriteLine("počet bojovníků byl nastaven na 3");
                        break;
                    case "2":
                        pocetBojovniku = 5;
                        Console.WriteLine("počet bojovníků byl nastaven na 5");
                        break;
                    case "3":
                        pocetBojovniku = 7;
                        Console.WriteLine("počet bojovníků byl nastaven na 7");
                        break;
                }
            }

        }
        public void SlozitostHry()
        {
            Console.Clear();
            Console.WriteLine("nastavení složitosti hry:");
            Console.WriteLine("1: základní hra  ()\n2: rozšířená hra    (8 tříd)");
            Console.WriteLine("3: zpátky");
            string vstup = "";
            while (vstup != "3")
            {
                vstup = Console.ReadLine();
                
                switch (vstup)
                {
                    case "1":
                        pocetTrid = 4;
                        Console.WriteLine("typ hry byl změněn na základní hru");
                        break;
                    case "2":
                        pocetTrid = 10;
                        Console.WriteLine("typ hry byl změněn na rozšířenou hru");
                        break;
                }
            }

        }
        public void MenuUctu()
        {
            string vstup = "";
            while (vstup != "3")
            {
                Console.Clear();
                Console.WriteLine("1: přihlásit se\n2: vytvořit nový účet\n3: zpátky");
                vstup = Console.ReadLine();
                switch (vstup)
                {
                    case "1":
                        spravaUctu.PrihlasitSe();
                        break;
                    case "2":
                        spravaUctu.NovyUcet();
                        break;
                }

            }
        }

        public void HlavniMenu()
        {
            while (hraVPrubehu == false)
            {

                Console.WriteLine("1: hrát");
                Console.WriteLine("2: účet");
                Console.WriteLine("3: jak hrát?");
                Console.WriteLine("4: nastavení");
                Console.WriteLine("5: konec hry");
                string vstup = Console.ReadLine();
                switch (vstup)
                {
                    case "1":
                        hraVPrubehu = true;
                        Hra();
                        break;
                    case "2":
                        MenuUctu();
                        break;
                    case "3":
                        Console.Clear();
                        Console.WriteLine("princip hry:");
                        Console.WriteLine("ve hře bojuje tým bojovníku jednoho hráče proti druhému týmu.\nna začátku hry si hráči vybírají jednotky do svých teamu,");
                        Console.WriteLine("poté hráči střídavě vybírají jednotky k boji.\nhra končí zabitím všech členů jednoho týmu\n");
                        Console.WriteLine("ovládání:");
                        Console.WriteLine("hra se ovládá psaním čísel do konzole");
                        Console.WriteLine("\nstiskněte libovolné tlačítko pro pokračování");
                        Console.ReadKey();
                        break;
                    case "4":
                        Nastaveni();
                        break;
                    case "5":
                        Environment.Exit(0);
                        break;
                }
                Console.Clear();
            }
        }

        public void VypocetUtoku(byte utocnik, byte cil, bool team1, byte typUtoku)
        {
            object vstup;
            if (team1)
            {
                if (typUtoku == 1)          { vstup = this.team1[utocnik].Utok(cil, team1); }
                else if (typUtoku == 2)     { vstup = this.team1[utocnik].Utok2(cil, team1); }
                else                        { vstup = this.team1[utocnik].Utok3(cil, team1); }
            }
            else
            {
                if (typUtoku == 1)          { vstup = this.team2[utocnik].Utok(cil, team1); }
                else if (typUtoku == 2)     { vstup = this.team2[utocnik].Utok2(cil, team1); }
                else                        { vstup = this.team2[utocnik].Utok3(cil, team1); }
            }
            //vyhodnocení
            Tuple<short[], short[], short[], short[], short[], short[], short> tuple = (Tuple<short[], short[], short[], short[], short[], short[], short>)vstup;
            //prvni útok, druhý útok, třetí útok, čtvrtý útok, první tým, druhý tým
            if (tuple.Item1.Length > 0)
            { //1
                PodVypocetUtoku(tuple.Item1);
            } //1
            if (tuple.Item2.Length > 0)
            { //2
                
            } //2
            PodVypocetUtoku(tuple.Item2);
            if (tuple.Item3.Length > 0)
            { //3
                
            } //3
            PodVypocetUtoku(tuple.Item3);
            if (tuple.Item4.Length > 0)
            { //4
                PodVypocetUtoku(tuple.Item4);
            } //4


            if (tuple.Item5.Length > 0) //5
            {
                PodVypocetUtokuTym(tuple.Item5);
                if (tuple.Item6.Length > 0) //6
                {
                    PodVypocetUtokuTym(tuple.Item6);

                }//6
            }//5

            if(tuple.Item7 > 0)
            {
                if (team1)
                {
                    this.team1[utocnik].Obrana(tuple.Item7, false);
                }else{
                    this.team2[utocnik].Obrana(tuple.Item7, false);
                }
            }

        }
        public void PodVypocetUtoku(short[] vstup)  //team, cil, dmg, otrava, nasobicDMG, oziveni, armor
        {
            if (vstup.Length > 0)
            {
                if (vstup[0] == 1)
                {

                    if (vstup[2] != 0) { this.team1[vstup[1]].Obrana(vstup[2], true); }
                    try { if (vstup[3] >= 0) { this.team1[vstup[1]].otrava = vstup[3]; } } catch (Exception) {  }
                    try { if (vstup[4] > 0) { this.team1[vstup[1]].nasobicDMG = (float)vstup[4] / 100; } } catch (Exception) { }
                    try { if (vstup[5] == 1) { this.team1[vstup[1]].zivi = true; } } catch (Exception) { }
                    try { if (vstup[6] != 0) { this.team1[vstup[1]].armor += vstup[6]; } } catch (Exception) { }
                    try { if (vstup[7] != 0) { this.team1[vstup[1]].en += vstup[7]; } } catch (Exception) { }
                }
                else if (vstup[0] == 2)
                {
                    if (vstup[2] != 0) { team2[vstup[1]].Obrana(vstup[2], true); }
                    try { if (vstup[3] >= 0) { team2[vstup[1]].otrava = vstup[3]; } } catch (Exception) { }
                    try { if (vstup[4] > 0) { team2[vstup[1]].nasobicDMG = (float)vstup[4] / 100; } } catch (Exception) { }
                    try { if (vstup[5] == 1) { team2[vstup[1]].zivi = true; } } catch (Exception) { }
                    try { if (vstup[6] != 0) { team2[vstup[1]].armor += vstup[6]; } } catch (Exception) { }
                    try { if (vstup[7] != 0) { team2[vstup[1]].en += vstup[7]; } } catch (Exception) { }
                }
                
            }
        }

        public void PodVypocetUtokuTym(short[] vstup)
        {

            if (vstup[0] == 1)
            {
                for (int i = 0; i <= pocetBojovniku - 1; i++)
                {
                    if (vstup[1] != 0) this.team1[i].Obrana(vstup[1], true);
                    try { if (vstup[2] >= 0) { this.team1[i].otrava = vstup[2]; } } catch (Exception) { }
                    try { if (vstup[3] > 0) { this.team1[i].nasobicDMG *= (float)vstup[3] / 100; } } catch (Exception) { }
                    try { if (vstup[4] == 1) { this.team1[i].zivi = true; } } catch (Exception) { }
                    try { if (vstup[5] != 0) { this.team1[i].armor += vstup[5]; } } catch (Exception) { }
                    try { if (vstup[6] != 0) { this.team1[i].en += vstup[6]; } } catch (Exception) { }
                }
            }
            else
            {
                for (int i = 0; i <= pocetBojovniku - 1; i++)
                {
                    if (vstup[1] != 0)
                        team2[i].Obrana(vstup[1], true);
                    try { if (vstup[2] >= 0) { team2[i].otrava = vstup[2]; } } catch (Exception) { }
                    try { if (vstup[3] > 0) { team2[i].nasobicDMG *= (float)vstup[3] / 100; } } catch (Exception) { }
                    try { if (vstup[4] == 1) { team2[i].zivi = true; } } catch (Exception) { }
                    try { if (vstup[6] != 0) { team2[i].en += vstup[6]; } } catch (Exception) { }


                }
            }
        }
        public void VyberJednotek()
        {
            int pocitadlo = 0;
            string vstup;
            short cislo;
            bool prvniHrac = true;
            Console.Clear();
            Console.WriteLine("První hráč vybíra jednotky\nstiskněte libovolné tlačítko pro pokračování");
            Console.ForegroundColor = ConsoleColor.White;

            for (int i = 0; i < 2; i++)
            {
                Console.ReadKey();
                Console.Clear();
                GenJednotky(15);
                VyberJednotekVypis();
                Console.WriteLine();
                Console.WriteLine("vyberte si {0} jednotek", pocetBojovniku);
                Console.WriteLine("jednotky si vyberete zadáním jejich čísel do konzole");
                while (pocitadlo < pocetBojovniku) // tady byl změna
                { //while
                    vstup = Console.ReadLine();
                    try
                    {
                        cislo = short.Parse(vstup);
                        cislo -= 1;

                        if (cislo < jednotky.Length && cislo >= 0)
                        {
                            Console.Clear();
                            if (jednotky[cislo].team != 0) { Console.WriteLine("tuto jednotku jste si už vybrali"); }
                            else if (prvniHrac)
                            {
                                team1[pocitadlo] = jednotky[cislo];
                                team1[pocitadlo].team = 1;
                                pocitadlo++;
                            }
                            else
                            {
                                team2[pocitadlo] = jednotky[cislo];
                                team2[pocitadlo].team = 2;
                                pocitadlo++;
                            }
                            if (pocitadlo < pocetBojovniku)
                            {
                                VyberJednotekVypis();
                                Console.WriteLine();
                                Console.WriteLine("vyberte si ještě {0} jednotek", (pocetBojovniku - pocitadlo));
                                Console.WriteLine("jednotky si vyberete zadáním jejich čísel do konzole");
                            }
                        }
                        else { Console.WriteLine("neplatné číslo"); }
                    }
                    catch { Console.WriteLine("neplatná volba"); }
                } //while
                if (i == 0)
                {
                    Console.Clear();
                    Console.WriteLine("Druhý hráč vybíra jednotky\nstiskněte libovolné tlačítko pro pokračování");
                    prvniHrac = false;
                }
                pocitadlo = 0;
            }

        }
        public void VyberJednotekVypis()
        {
            for (int i = 0; i < jednotky.Length;)
            {
                if (jednotky[i].team == 0)
                {
                    Console.Write(i + 1 + ". ");
                    if (i + 1 < 10) Console.Write(" ");
                    jednotky[i].Info(false);
                    Console.WriteLine();
                }
                i++;
            }
        }
        public void VyberUtoku(bool team1)
        {
            if (Console.WindowWidth < 127 && Console.LargestWindowWidth >= 127) Console.WindowWidth = 127;
                
            if (team1) {
                Console.WriteLine("                            Hráč 1                            VS                               hráč 2");
            }else {
                Console.WriteLine("                            Hráč 2                            VS                               hráč 1");
            }

                for (int i = 0; i < pocetBojovniku; i++)
                {
                    Console.Write((i + 1) + ". ");
                    if (team1)
                    {
                        this.team1[i].Info(true);
                        Console.Write("        " + (i + 1) + ". ");
                        team2[i].Info(true);
                }
                    else
                    {
                        team2[i].Info(true);
                        Console.Write("        " + (i + 1) + ". ");
                        this.team1[i].Info(true);
                }
                    Console.WriteLine();
                }
                if (team1) { Console.WriteLine("hraje první hráč"); } else { Console.WriteLine("hraje druhý hráč"); }
                
                bool jednotkaVybrana = false;
                bool cilVybran = false;
                string vstup;
                int cisloUtocnika = -1;
                int cisloCile = 1;
                int cisloUtoku = 0;
                bool cisloVybrane = false;
                bool[] povoleneUtoky;
                bool utokPotrebujeCil = true;
                Console.WriteLine("vyberte útočící jednotku, nebo napište 0 pro přeskočení tahu");
                while (jednotkaVybrana == false)
                {
                    vstup = Console.ReadLine();
                if (int.TryParse(vstup, out cisloUtocnika) && cisloUtocnika <= pocetBojovniku && cisloUtocnika > 0)
                {
                    if ((team1 && this.team1[cisloUtocnika - 1].zivi) || (team1 == false && team2[cisloUtocnika - 1].zivi)) {
                        cisloUtocnika -= 1;
                        if (team1)
                        {
                            povoleneUtoky = this.team1[cisloUtocnika].UtokInfo();
                        }
                        else
                        {
                            povoleneUtoky = this.team2[cisloUtocnika].UtokInfo();
                        }

                        Console.WriteLine("4. zobrazit informace o jednotce");
                        Console.WriteLine("5. zrušit útok");
                        vstup = "";
                        while (cisloVybrane == false)
                        {
                            vstup = Console.ReadLine();
                            int.TryParse(vstup, out cisloUtoku);
                            if (cisloUtoku > 0 && cisloUtoku < 6) {
                                if (cisloUtoku == 1 && povoleneUtoky[0]) { cisloVybrane = true; jednotkaVybrana = true; } else if (cisloUtoku == 1) { Console.WriteLine("tento ůtok není dostupný"); }
                                if (cisloUtoku == 2 && povoleneUtoky[1]) { cisloVybrane = true; jednotkaVybrana = true; } else if (cisloUtoku == 2) { Console.WriteLine("tento ůtok není dostupný"); }
                                if (cisloUtoku == 3 && povoleneUtoky[2]) { cisloVybrane = true; jednotkaVybrana = true; } else if (cisloUtoku == 3) { Console.WriteLine("tento ůtok není dostupný"); }
                                if (cisloUtoku == 4) { 
                                    if (team1) this.team1[cisloUtocnika].FullInfo(); else team2[cisloUtocnika].FullInfo();
                                    Console.WriteLine("");
                                }
                                if (cisloUtoku == 5) { cisloVybrane = true; break; }
                            }
                        }
                        if (cisloUtoku == 5) { cisloVybrane = false; Console.WriteLine("vyberte útočící jednotku, nebo napište 0 pro přeskočení tahu"); }
                    }
                } else if (cisloUtocnika == 0) { 
                    if (team1) { Console.WriteLine("hráč 1 se vzdává svého tahu"); } else { Console.WriteLine("hráč 2 se vzdává svého tahu"); }
                    jednotkaVybrana = true;
                    cilVybran = true;
                    break;
                }
                }       //konec while

                if (team1) {
                if (this.team1[cisloUtocnika].PotrebujeCil((Int16)cisloUtoku) == false) utokPotrebujeCil = false;
                } else {
                if (team2[cisloUtocnika].PotrebujeCil((Int16)cisloUtoku) == false) utokPotrebujeCil = false;
                }

                if (cilVybran == false)
                {
                    if (utokPotrebujeCil)
                    {
                        if (cilVybran == false) Console.WriteLine("\nvyberte cíl útoku");
                        while (cilVybran == false)
                        {
                            vstup = Console.ReadLine();
                            if (int.TryParse(vstup, out cisloCile) && cisloCile > 0 && cisloCile <= pocetBojovniku)
                            {
                                cisloCile -= 1;

                                VypocetUtoku(Byte.Parse(cisloUtocnika.ToString()), Byte.Parse(cisloCile.ToString()), team1, (byte)cisloUtoku);
                                cilVybran = true;
                            }
                        }
                    }
                    else
                    {
                        VypocetUtoku(Byte.Parse(cisloUtocnika.ToString()), Byte.Parse(cisloCile.ToString()), team1, (byte)cisloUtoku);
                    } 
                }

        }
        public void Hra()
        {
            if (Console.WindowWidth < 55 && Console.LargestWindowWidth >= 55) Console.WindowWidth = 55;
            VyberJednotek();
            while (hraVPrubehu)
            {
                VyberUtoku(true);
                Console.ReadLine();
                PolovinaKola();
                if (hraVPrubehu == false) break;
                VyberUtoku(false);
                Console.ReadLine();
                PolovinaKola();
                
            }
            KonecHry();
        }
        public void PolovinaKola() {
            Console.Clear();
            bool team1Zije = false;
            bool team2Zije = false;
            for (byte i = 0; i < pocetBojovniku;)
            {
                if(konecKola) this.team1[i].DalsiKolo(pocetKol);
                if (this.team1[i].zivi) team1Zije = true;
                if(konecKola) team2[i].DalsiKolo(pocetKol);
                if (this.team2[i].zivi) team2Zije = true;
                i++;
            }
            if (konecKola) { konecKola = false; } else { konecKola = true; pocetKol++; }
            if (team1Zije == false || team2Zije == false)
            {
                Console.WriteLine("konec hry");
                if (team1Zije == true && team2Zije == false) { Console.WriteLine("Hráč 1 vyhrál"); vyhra = 1; }
                if (team1Zije == false && team2Zije == true) { Console.WriteLine("Hráč 2 vyhrál"); vyhra = 2; }
                if (team1Zije && team2Zije) { Console.WriteLine("Remíza - všichni prohrály"); vyhra = 3; }
                Console.ReadKey();
                hraVPrubehu = false;
            }
        }
        public void KonecHry()
        {
            spravaUctu.KonecHry(vyhra);
        }
    }
}

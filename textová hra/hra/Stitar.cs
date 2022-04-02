using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hra
{
    class Stitar:Jednotka
    {   
        byte aktivniStit = 0;
        public Stitar()
        {
            trida = "štítař    ";
        }
        public override void Obrana(short silaUtoku, bool brneni)
        {
            short zraneni = 0;
            Console.ForegroundColor = ConsoleColor.Red;
            if (aktivniStit == 0 || gen.Next(0,3) !=2)
            {
                if (silaUtoku >= 0)
                {
                    if (brneni) { zraneni = (Int16)(silaUtoku - armor); } else { zraneni = silaUtoku; }
                    if (zraneni >= 0) hp -= zraneni; else zraneni = 0;
                    if (zivi) Console.WriteLine(jmeno + " ztrácí " + zraneni + " životů"); else Console.WriteLine(jmeno + " je stále mrtvý");
                }
                else
                {
                    silaUtoku *= -1;
                    hp += silaUtoku;
                    Console.WriteLine(jmeno + " získal " + silaUtoku + " životů");
                }     
            }
            else
            {
                Console.WriteLine(jmeno + " vykryl/a útok");
            }
            if (hp <= 0) { hp = 0; zivi = false; }
            Console.ForegroundColor = ConsoleColor.White;
        }
        public override bool[] UtokInfo()
        {
            bool[] moznyUtok = new bool[3];
            if (en >= 1) { moznyUtok[0] = true; Console.ForegroundColor = ConsoleColor.White; } else { Console.ForegroundColor = ConsoleColor.DarkGray; }
            Console.WriteLine("1. seknutí mečem (1 energie)");
            if (en >= 4 && aktivniStit == 0) { moznyUtok[1] = true; Console.ForegroundColor = ConsoleColor.White; } else { Console.ForegroundColor = ConsoleColor.DarkGray; }
            Console.WriteLine("2. vykrývání (4 energie)");
            if (su == 1) { moznyUtok[2] = true; Console.ForegroundColor = ConsoleColor.White; Console.WriteLine("3. speciální útok"); }

            Console.ForegroundColor = ConsoleColor.White;
            return moznyUtok;
        }
        public override Tuple<short[], short[], short[], short[], short[], short[], short> Utok(byte cil, bool team)       //dmg, otrava, team, 2cil, 3cil, vsechny cile; self-dmg
        {
            short uDmg = (Int16)(dmg * nasobicDMG * 0.8);

            short sTeam = 1;
            if (team) { sTeam = 2; }
            short[] prvniCil = { sTeam, cil, uDmg };
            short[] druhyCil = { };                  //team, cil, dmg, otrava, nasobicDMG, oziveni, armor
            short[] tretiCil = { };
            short[] ctvrtyCil = { };
            short[] celyTeam = { };
            short[] celyTeam2 = { };                  //team, dmg, otrava, nasobicDMG, oziveni, armor
            if (team) { team = false; } else { team = true; }
            Console.WriteLine(jmeno + " útočí se silou " + uDmg);
            en -= 1;
            return Tuple.Create(prvniCil, druhyCil, tretiCil, ctvrtyCil, celyTeam, celyTeam2, (Int16)0);

        }
        public override Tuple<short[], short[], short[], short[], short[], short[], short> Utok2(byte cil, bool team)       //dmg, otrava, team, 2cil, 3cil, vsechny cile; self-dmg
        {
            short[] prvniCil = { };
            short[] druhyCil = { };
            short[] tretiCil = { };
            short[] ctvrtyCil = { };
            short[] celyTeam = { };
            short[] celyTeam2 = { };
            Console.WriteLine(jmeno + " používá schopnost vykrývání ");
            aktivniStit = 3;
            en -= 4;
            return Tuple.Create(prvniCil, druhyCil, tretiCil, ctvrtyCil, celyTeam, celyTeam2, (Int16)0);
        }
        public override Tuple<short[], short[], short[], short[], short[], short[], short> Utok3(byte cil, bool team)       //dmg, otrava, team, 2cil, 3cil, vsechny cile; self-dmg
        {
            su = 2;
            short sTeam = 2;
            if (team) { sTeam = 1; }
            short[] prvniCil = { };
            short[] druhyCil = { };
            short[] tretiCil = { };
            short[] ctvrtyCil = { };
            short[] celyTeam2 = { };
            short[] celyTeam = {sTeam, 0, -1, 0, 0, 5 };//team, dmg, otrava, nasobicDMG, oziveni, armor
            if (team) { team = false; } else { team = true; }
            Console.WriteLine(jmeno + " používá schopnost obrnění");
            Console.WriteLine("brnění teamu {0} bylo zvýšeno o 5", sTeam);
            return Tuple.Create(prvniCil, druhyCil, tretiCil, ctvrtyCil, celyTeam, celyTeam2, (Int16)0);
        }
        public override void Su(short pocetKol)
        {
            if (pocetKol == 2) su = 1;      //pak to změň na 6
            if (aktivniStit > 0) aktivniStit--;
        }
        public override bool PotrebujeCil(short cisloUtoku)
        {
            if (cisloUtoku == 2 || cisloUtoku == 3) return false; else return true;

        }   
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hra
{
    class Spion:Jednotka
    {
        public Spion()
        {
            trida = "špion     ";
        }
        public override bool[] UtokInfo()
        {
            bool[] moznyUtok = new bool[3];
            if (en >= 5) { moznyUtok[0] = true; Console.ForegroundColor = ConsoleColor.White; } else { Console.ForegroundColor = ConsoleColor.DarkGray; }
            Console.WriteLine("1. oslabění (5 energie)");
            if (en >= 5) { moznyUtok[1] = true; Console.ForegroundColor = ConsoleColor.White; } else { Console.ForegroundColor = ConsoleColor.DarkGray; }
            Console.WriteLine("2. otrávení (5 energie)");
            if (su == 1) { moznyUtok[2] = true; Console.ForegroundColor = ConsoleColor.White; Console.WriteLine("3. speciální útok"); }

            Console.ForegroundColor = ConsoleColor.White;
            return moznyUtok;
        }
        public override Tuple<short[], short[], short[], short[], short[], short[], short> Utok(byte cil, bool team)       //1cil, 2cil, 3cil, cely tym 1, cely tym 2; self-dmg
        {
            short sTeam = 1;
            if (team) { sTeam = 2; }
            short[] prvniCil = {  };
            short[] druhyCil = { };                  //team, cil, dmg, otrava, nasobicDMG, oziveni, armor
            short[] tretiCil = { };
            short[] ctvrtyCil = { };
            short[] celyTeam = { sTeam, 0, -1, 95 };
            short[] celyTeam2 = { };    //team, dmg, otrava, nasobicDMG, oziveni, armor
            short selfDmg = 0;
            Console.WriteLine(jmeno + " používá schopnost oslabení ");
            Console.WriteLine(jmeno + " snižuje útok teamu " + sTeam + " o 5%");
            en -= 5;
            return Tuple.Create(prvniCil, druhyCil, tretiCil, ctvrtyCil, celyTeam, celyTeam2, selfDmg);

        }
        public override Tuple<short[], short[], short[], short[], short[], short[], short> Utok2(byte cil, bool team)       //1cil, 2cil, 3cil, cely tym 1, cely tym 2; self-dmg
        {
            short sTeam = 1;
            if (team) { sTeam = 2; }
            short[] prvniCil = { };
            short[] druhyCil = { };                  //team, cil, dmg, otrava, nasobicDMG, oziveni, armor
            short[] tretiCil = { };
            short[] ctvrtyCil = { };
            short[] celyTeam = { sTeam, 0, 3};
            short[] celyTeam2 = { };    //team, dmg, otrava, nasobicDMG, oziveni, armor
            short selfDmg = 0;
            Console.WriteLine(jmeno + " používá schopnost otrávení ");
            en -= 5;
            return Tuple.Create(prvniCil, druhyCil, tretiCil, ctvrtyCil, celyTeam, celyTeam2, selfDmg);

        }
        public override Tuple<short[], short[], short[], short[], short[], short[], short> Utok3(byte cil, bool team)       //dmg, otrava, team, 2cil, 3cil, vsechny cile; self-dmg
        {
            su = 2;
            short sTeam = 1;
            if (team) { sTeam = 2; }
            short[] prvniCil = { };
            short[] druhyCil = { };                  //team, cil, dmg, otrava, nasobicDMG, oziveni, armor
            short[] tretiCil = { };
            short[] ctvrtyCil = { };
            short[] celyTeam = { sTeam, 0, 2, 95, 0, 0, -2 };
            short[] celyTeam2 = { };    //team, dmg, otrava, nasobicDMG, oziveni, armor, energie
            if (team) { team = false; } else { team = true; }
            Console.WriteLine(jmeno + " používá speciální schopnost: zrada ");
            Console.WriteLine(jmeno + " snižuje útok teamu " + sTeam + " o 5%");
            Console.WriteLine(jmeno + " snižuje energii teamu " + sTeam + " o 5%");
            return Tuple.Create(prvniCil, druhyCil, tretiCil, ctvrtyCil, celyTeam, celyTeam2, (Int16)0);
        }
        public override void Su(short pocetKol)
        {
            if (pocetKol == 7) su = 1;
        }

        public override bool PotrebujeCil(short cisloUtoku)
        {
            return false;
        }
    }
}

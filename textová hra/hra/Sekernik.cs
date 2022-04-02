using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hra
{
    class Sekernik:Jednotka
    {
        public Sekernik()
        {
            trida = "Sekerník  ";
        }
        public override bool[] UtokInfo()
        {
            bool[] moznyUtok = new bool[3];
            if (en >= 2) { moznyUtok[0] = true; Console.ForegroundColor = ConsoleColor.White; } else { Console.ForegroundColor = ConsoleColor.DarkGray; }
            Console.WriteLine("1. seknutí (2 energie)");
            if (en >= 4) { moznyUtok[1] = true; Console.ForegroundColor = ConsoleColor.White; } else { Console.ForegroundColor = ConsoleColor.DarkGray; }
            Console.WriteLine("2. odkrytý útok (4 energie)");
            if (su == 1) { moznyUtok[2] = true; Console.ForegroundColor = ConsoleColor.White; Console.WriteLine("3. speciální útok"); }

            Console.ForegroundColor = ConsoleColor.White;
            return moznyUtok;
        }
        public override Tuple<short[], short[], short[], short[], short[], short[], short> Utok2(byte cil, bool team)       //dmg, otrava, team, 2cil, 3cil, vsechny cile; self-dmg
        {
            short uDmg = (Int16)(dmg * nasobicDMG * 2.5);
            short sTeam = 1;
            if (team) { sTeam = 2; }
            short[] prvniCil = { sTeam, cil, uDmg };
            short[] druhyCil = { };
            short[] tretiCil = { };
            short[] ctvrtyCil = { };
            short[] celyTeam = { };
            short[] celyTeam2 = { };
            short selfDmg = (Int16)(uDmg * 0.2);
            if (team) { team = false; } else { team = true; }
            Console.WriteLine(jmeno + " útočí se silou " + uDmg);
            en -= 4;
            return Tuple.Create(prvniCil, druhyCil, tretiCil, ctvrtyCil, celyTeam, celyTeam2, selfDmg);

        }
        public override Tuple<short[], short[], short[], short[], short[], short[], short> Utok3(byte cil, bool team)       //dmg, otrava, team, 2cil, 3cil, vsechny cile; self-dmg
        {
            su = 2;
            short uDmg = (Int16)(dmg * nasobicDMG);
            short sTeam = 1;
            if (team) { sTeam = 2; }
            short[] prvniCil = { sTeam, cil, 0, -1, -1, 0, -10 };
            short[] druhyCil = { };     //team, cil, dmg, otrava, nasobicDMG, oziveni, armor
            short[] tretiCil = { };
            short[] ctvrtyCil = { };
            short[] celyTeam = { };
            short[] celyTeam2 = { };
            if (team) { team = false; } else { team = true; }
            Console.WriteLine(jmeno + " používá speciální schopnost: prolomení zbroje " + uDmg);
            Console.WriteLine(jmeno + " útočí se silou " + uDmg);
            return Tuple.Create(prvniCil, druhyCil, tretiCil, ctvrtyCil, celyTeam, celyTeam2, (Int16)0);

        }
        public override void Su(short pocetKol)
        {
            if (pocetKol == 4) su = 1;
        }

    }
}

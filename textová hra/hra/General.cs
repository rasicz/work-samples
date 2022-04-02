using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hra
{
    class General:Jednotka
    {
        public General()
        {
            trida = "generál   ";
        }
        public override bool[] UtokInfo()
        {
            bool[] moznyUtok = new bool[3];
            if (en >= 3) { moznyUtok[0] = true; Console.ForegroundColor = ConsoleColor.White; } else { Console.ForegroundColor = ConsoleColor.DarkGray; }
            Console.WriteLine("1. útok (3 energie)");
            if (en >= 5) { moznyUtok[1] = true; Console.ForegroundColor = ConsoleColor.White; } else { Console.ForegroundColor = ConsoleColor.DarkGray; }
            Console.WriteLine("2. motivace (5 energie)");
            if (su == 1) { moznyUtok[2] = true; Console.ForegroundColor = ConsoleColor.White; Console.WriteLine("3. speciální útok"); }

            Console.ForegroundColor = ConsoleColor.White;
            return moznyUtok;
        }
        public override Tuple<short[], short[], short[], short[], short[], short[], short> Utok(byte cil, bool team)       //dmg, otrava, team, 2cil, 3cil, vsechny cile; self-dmg
        {
            short uDmg = (Int16)(dmg * nasobicDMG);

            short sTeam = 1;
            if (team) { sTeam = 2; }
            short[] prvniCil = { sTeam, cil, uDmg };
            short[] druhyCil = { };                  //team, cil, dmg, otrava, nasobicDMG, oziveni
            short[] tretiCil = { };
            short[] ctvrtyCil = { };
            short[] celyTeam = { };
            short[] celyTeam2 = { };                  //team, dmg, otrava, nasobicDMG, oziveni
            if (team) { team = false; } else { team = true; }
            Console.WriteLine(jmeno + " útočí se silou " + uDmg);
            en -= 3;
            return Tuple.Create(prvniCil, druhyCil, tretiCil, ctvrtyCil, celyTeam, celyTeam2, (Int16)(0));

        }
        public override Tuple<short[], short[], short[], short[], short[], short[], short> Utok2(byte cil, bool team)       //dmg, otrava, team, 2cil, 3cil, vsechny cile; self-dmg
        {
            short sTeam = 2;
            if (team) { sTeam = 1; }
            short[] prvniCil = { };
            short[] druhyCil = { };                  //team, cil, dmg, otrava, nasobicDMG, oziveni
            short[] tretiCil = { };
            short[] ctvrtyCil = { };
            short[] celyTeam = { sTeam, 0, -1, 0, 0, 0, 2 };
            short[] celyTeam2 = {};                  //team, dmg, otrava, nasobicDMG, oziveni, armor
            Console.WriteLine(jmeno + " používá schopnost: motivace ");
            Console.WriteLine("energie týmu " + sTeam + " se zvýšila o 2");
            en -= 5;
            return Tuple.Create(prvniCil, druhyCil, tretiCil, ctvrtyCil, celyTeam, celyTeam2, (Int16)(0));

        }
        public override Tuple<short[], short[], short[], short[], short[], short[], short> Utok3(byte cil, bool team)       //dmg, otrava, team, 2cil, 3cil, vsechny cile; self-dmg
        {
            su = 2;
            short uDmg = (Int16)(dmg * nasobicDMG * 0.8);
            short sTeam = 2;
            if (team) { sTeam = 1;}
            short[] prvniCil = { };
            short[] druhyCil = { };                  //team, cil, dmg, otrava, nasobicDMG, oziveni
            short[] tretiCil = { };
            short[] ctvrtyCil = { };
            short[] celyTeam = { sTeam, uDmg };
            short[] celyTeam2 = { };                  //team, dmg, otrava, nasobicDMG, oziveni, armor
            Console.WriteLine(jmeno + " používá speciální schopnost: povolání záloh ");
            Console.WriteLine(jmeno + " útočí se silou " + uDmg);
            return Tuple.Create(prvniCil, druhyCil, tretiCil, ctvrtyCil, celyTeam, celyTeam2, (Int16)(0));

        }
        public override void Su(short pocetKol)
        {
            if (pocetKol == 2) su = 1;
        }

        public override bool PotrebujeCil(short cisloUtoku)
        {
            if (cisloUtoku == 1) return true; else return false;
        }
    }
}

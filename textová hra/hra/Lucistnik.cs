using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hra
{
    class Lucistnik:Jednotka
    {   
        public Lucistnik()
        {
            trida = "lučistník ";
        }
        public override bool[] UtokInfo()
        {
            bool[] moznyUtok = new bool[3];
            if (en >= 3) { moznyUtok[0] = true; Console.ForegroundColor = ConsoleColor.White; } else { Console.ForegroundColor = ConsoleColor.DarkGray; }
            Console.WriteLine("1. výstřel z luku (3 energie)");
            if (en >= 5) { moznyUtok[1] = true; Console.ForegroundColor = ConsoleColor.White; } else { Console.ForegroundColor = ConsoleColor.DarkGray; }
            Console.WriteLine("2. otrávený šíp (5 energie)");
            if (su == 1) { moznyUtok[2] = true; Console.ForegroundColor = ConsoleColor.White; Console.WriteLine("3. speciální útok"); }

            Console.ForegroundColor = ConsoleColor.White;
            return moznyUtok;
        }
        public override Tuple<short[], short[], short[], short[], short[], short[], short> Utok(byte cil, bool team)       //dmg, otrava, team, 2cil, 3cil, vsechny cile; self-dmg
        {
            short uDmg = (Int16)(dmg * nasobicDMG * 1.25);

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
            short uDmg = (Int16)(dmg * nasobicDMG);
            short otrava = 5;
            short sTeam = 1;
            if (team) { sTeam = 2; }

            short[] prvniCil = { sTeam, cil, uDmg, otrava };
            short[] druhyCil = { };
            short[] tretiCil = { };
            short[] ctvrtyCil = { };
            short[] celyTeam = { };
            short[] celyTeam2 = { };
            if (team) { team = false; } else { team = true; }
            Console.WriteLine(jmeno + " útočí se silou " + uDmg);
            en -= 5;
            return Tuple.Create(prvniCil, druhyCil, tretiCil, ctvrtyCil, celyTeam, celyTeam2, (Int16)(0));

        }
        public override Tuple<short[], short[], short[], short[], short[], short[], short> Utok3(byte cil, bool team)       //dmg, otrava, team, 2cil, 3cil, vsechny cile; self-dmg
        {
            su = 2;
            short uDmg = (Int16)(dmg * nasobicDMG * 0.80);
            short sTeam = 1;
            if (team) { sTeam = 2; }
            short[] prvniCil = {sTeam, cil, uDmg };
            short[] druhyCil = {sTeam, cil, uDmg };     //team, cil, dmg, otrava, nasobicDMG, oziveni
            short[] tretiCil = {sTeam, cil, uDmg };
            short[] ctvrtyCil = { };
            short[] celyTeam = { };
            short[] celyTeam2 = { };
            if (team) { team = false; } else { team = true; }
            Console.WriteLine(jmeno + " používá speciální schopnost: déšť šípů");
            Console.WriteLine(jmeno + " útočí 3x se silou " + uDmg);
            return Tuple.Create(prvniCil, druhyCil, tretiCil, ctvrtyCil, celyTeam, celyTeam2, (Int16)(0));

        }
        public override void Su(short pocetKol)
        {
            if (pocetKol == 4) su = 1;
        }           
    }
}

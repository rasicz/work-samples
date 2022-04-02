using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hra
{
    class Pikenyr:Jednotka
    {   
        byte aktivniZakleknuti = 0;
        public Pikenyr()
        {
            trida = "pikenýr   ";
            dmg = (Int16)(dmg * 0.8);
        }
        public override bool[] UtokInfo()
        {
            bool[] moznyUtok = new bool[3];
            if (en >= 1) { moznyUtok[0] = true; Console.ForegroundColor = ConsoleColor.White; } else { Console.ForegroundColor = ConsoleColor.DarkGray; }
            Console.WriteLine("1. bodnutí (1 energie)");
            if (en >= 3 && aktivniZakleknuti == 0) { moznyUtok[1] = true; Console.ForegroundColor = ConsoleColor.White; } else { Console.ForegroundColor = ConsoleColor.DarkGray; }
            Console.WriteLine("2. zakleknutí (3 energie)");
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
            short[] druhyCil = { };                  //team, cil, dmg, otrava, nasobicDMG, oziveni, armor
            short[] tretiCil = { };
            short[] ctvrtyCil = { };
            short[] celyTeam = { };
            short[] celyTeam2 = { };                       //team, dmg, otrava, nasobicDMG, oziveni, armor
            if (team) { team = false; } else { team = true; }
            Console.WriteLine(jmeno + " útočí se silou " + uDmg);
            en -= 1;
            return Tuple.Create(prvniCil, druhyCil, tretiCil, ctvrtyCil, celyTeam, celyTeam2, (Int16)(0));
        }
        public override Tuple<short[], short[], short[], short[], short[], short[], short> Utok2(byte cil, bool team)       //dmg, otrava, team, 2cil, 3cil, vsechny cile; self-dmg
        {
            short[] prvniCil = { };
            short[] druhyCil = { };
            short[] tretiCil = { };
            short[] ctvrtyCil = { };
            short[] celyTeam = { };
            short[] celyTeam2 = { };
            Console.WriteLine(jmeno + " používá schopnost zakleknutí ");
            aktivniZakleknuti = 3;
            en -= 3;
            nasobicDMG *= (float)1.3;
            armor += 5;
            return Tuple.Create(prvniCil, druhyCil, tretiCil, ctvrtyCil, celyTeam, celyTeam2, (Int16)(0));
        }
        public override Tuple<short[], short[], short[], short[], short[], short[], short> Utok3(byte cil, bool team)       //dmg, otrava, team, 2cil, 3cil, vsechny cile; self-dmg
        {
            su = 2;
            short uDmg = (Int16)(dmg * nasobicDMG * 2.5);
            short sTeam = 1;
            if (team) { sTeam = 2; }
            short[] prvniCil = { sTeam, cil, uDmg };
            short[] druhyCil = { };
            short[] tretiCil = { };
            short[] ctvrtyCil = { };
            short[] celyTeam = { };
            short[] celyTeam2 = { };
            if (team) { team = false; } else { team = true; }
            Console.WriteLine(jmeno + "používá speciální útok: probodnutí");
            return Tuple.Create(prvniCil, druhyCil, tretiCil, ctvrtyCil, celyTeam, celyTeam2, (Int16)(0));
        }
        public override void Su(short pocetKol)
        {
            if (pocetKol % 5 == 0) su = 1;
            if (aktivniZakleknuti == 1) { nasobicDMG /= (float)1.3; ; armor -= 5; }
            if (aktivniZakleknuti > 0) aktivniZakleknuti--;
        }
        public override bool PotrebujeCil(short cisloUtoku)
        {
            if (cisloUtoku == 2) return false; else return true;
        }   
    }
}

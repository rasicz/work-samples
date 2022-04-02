using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hra
{
    class Rytir:Jednotka
    {
        public Rytir() 
        {
            trida = "rytíř     ";
        }
        public override bool[] UtokInfo()
        {
            bool[] moznyUtok = new bool[3];
            if (en >= 2) { moznyUtok[0] = true; Console.ForegroundColor = ConsoleColor.White; } else { Console.ForegroundColor = ConsoleColor.DarkGray; }
            Console.WriteLine("1. bod mečem  (2 energie)");
            if (en >= 4) { moznyUtok[1] = true; Console.ForegroundColor = ConsoleColor.White; } else { Console.ForegroundColor = ConsoleColor.DarkGray; }
            Console.WriteLine("2. švihnutí mečem  (4 energie)");
            if (su == 1) { moznyUtok[2] = true; Console.ForegroundColor = ConsoleColor.White; Console.WriteLine("3. speciální útok"); }
            
            Console.ForegroundColor = ConsoleColor.White;
            return moznyUtok;
        }
        public override Tuple<short[], short[], short[], short[], short[], short[], short> Utok2(byte cil, bool team)       //dmg, otrava, team, 2cil, 3cil, vsechny cile; self-dmg
        {
            short uDmg = (Int16)(dmg * nasobicDMG * 1.5);
            short sTeam = 1;
            if (team) { sTeam = 2; }
            short[] prvniCil = { sTeam, cil, uDmg };
            short[] druhyCil = { };                  //team, cil, dmg, otrava, nasobicDMG, oziveni, armor
            short[] tretiCil = { };
            short[] ctvrtyCil = { };
            short[] celyTeam = { };
            short[] celyTeam2 = { };    //team, dmg, otrava, nasobicDMG, oziveni, armor
            if (team) { team = false; } else { team = true; }
            Console.WriteLine(jmeno + " útočí se silou " + uDmg);
            en -= 4;
            return Tuple.Create(prvniCil, druhyCil, tretiCil, ctvrtyCil, celyTeam, celyTeam2, (Int16)0);

        }
        public override Tuple<short[], short[], short[], short[], short[], short[], short> Utok3(byte cil, bool team)       //dmg, otrava, team, 2cil, 3cil, vsechny cile; self-dmg
        {
            su = 2;
            short sTeam = 2;
            if (team) { sTeam = 1; }
            short[] prvniCil = { };
            short[] druhyCil = { };                  //team, cil, dmg, otrava, nasobicDMG, oziveni
            short[] tretiCil = { };
            short[] ctvrtyCil = { };
            short[] celyTeam = {sTeam, 0, -1, 120};                  //team, dmg, otrava, nasobicDMG, oziveni
            short[] celyTeam2 = { };
            Console.WriteLine(jmeno + "používá speciální schopnost: motivace");
            Console.WriteLine("útok teamu {0} byl zvýšen o 20%", sTeam);
            return Tuple.Create(prvniCil, druhyCil, tretiCil, ctvrtyCil, celyTeam, celyTeam2, (Int16)0);
        }
        public override void Su(short pocetKol)
        {
            if (pocetKol == 6) su = 1;
        }
        public override bool PotrebujeCil(short cisloUtoku)
        {
            if (cisloUtoku == 3) return false; else return true;

        }

    }
}

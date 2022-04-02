using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hra
{
    class Zoldner:Jednotka
    {   
        public Zoldner()
        {
            trida = "žoldnéř   ";
            hp = (Int16)(hp * 1.2);
        }
        public override bool[] UtokInfo()
        {
            bool[] moznyUtok = new bool[3];
            if (en >= 4) { moznyUtok[0] = true; Console.ForegroundColor = ConsoleColor.White; } else { Console.ForegroundColor = ConsoleColor.DarkGray; }
            Console.WriteLine("1. silný útok (4 energie)");
            if (en >= 4) { moznyUtok[1] = true; Console.ForegroundColor = ConsoleColor.White; } else { Console.ForegroundColor = ConsoleColor.DarkGray; }
            Console.WriteLine("2. uzdravení (4 energie)");
            //if (su == 1) { moznyUtok[2] = true; Console.ForegroundColor = ConsoleColor.White; Console.WriteLine("3. třetí útok"); }

            Console.ForegroundColor = ConsoleColor.White;
            return moznyUtok;
        }
        public override Tuple<short[], short[], short[], short[], short[], short[], short> Utok(byte cil, bool team)       //dmg, otrava, team, 2cil, 3cil, vsechny cile; self-dmg
        {
            short uDmg = (Int16)(dmg * nasobicDMG * 2);
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
        public override Tuple<short[], short[], short[], short[], short[], short[], short> Utok2(byte cil, bool team)       //dmg, otrava, team, 2cil, 3cil, vsechny cile; self-dmg
        {  
            short[] prvniCil = { };
            short[] druhyCil = { };
            short[] tretiCil = { };
            short[] ctvrtyCil = { };
            short[] celyTeam = { };
            short[] celyTeam2 = { };
            if (team) { team = false; } else { team = true; }
            Console.WriteLine(jmeno + " používá schopnost uzdravení");
            Console.WriteLine(jmeno + " získal/a 50 zdraví");
            hp += 50;
            en -= 4;
            return Tuple.Create(prvniCil, druhyCil, tretiCil, ctvrtyCil, celyTeam, celyTeam2, (Int16)0);
        }
        public override bool PotrebujeCil(short cisloUtoku)
        {
            if (cisloUtoku == 2) return false; else return true;

        }
        public override void Su(short pocetKol)
        {
        }
    }
}

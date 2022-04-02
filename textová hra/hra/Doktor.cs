using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hra
{
    class Doktor:Jednotka
    {
        public Doktor()
        {
            trida = "doktor    ";
            hp = (Int16)(hp * 0.8);
            dmg = (Int16)(dmg * 0.5);
        }
        public override bool[] UtokInfo()
        {
            bool[] moznyUtok = new bool[3];
            Console.WriteLine("1. útok nožem  (0 energie)");
            moznyUtok[0] = true;
            if (en >= 4) { moznyUtok[1] = true; Console.ForegroundColor = ConsoleColor.White; } else { Console.ForegroundColor = ConsoleColor.DarkGray; }
            Console.WriteLine("2. léčení  (4 energie)");
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
            short[] celyTeam2 = { };                   //team, dmg, otrava, nasobicDMG, oziveni
            if (team) { team = false; } else { team = true; }
            Console.WriteLine(jmeno + " útočí se silou " + uDmg);
            return Tuple.Create(prvniCil, druhyCil, tretiCil, ctvrtyCil, celyTeam, celyTeam2, (Int16)0);

        }
        public override Tuple<short[], short[], short[], short[], short[], short[], short> Utok2(byte cil, bool team)       //dmg, otrava, team, 2cil, 3cil, vsechny cile; self-dmg
        {
            en -= 4;
            short uDmg = -50;
            short sTeam = 2;
            if (team) { sTeam = 1; }
            short[] prvniCil = { sTeam, cil, uDmg };
            short[] druhyCil = { };                  //team, cil, dmg, otrava, nasobicDMG, oziveni, armor
            short[] tretiCil = { };
            short[] ctvrtyCil = { };
            short[] celyTeam = { };
            short[] celyTeam2 = { };
            Console.WriteLine(jmeno + " používá schopnost léčení");
            return Tuple.Create(prvniCil, druhyCil, tretiCil, ctvrtyCil, celyTeam, celyTeam2, (Int16)0);

        }
        public override Tuple<short[], short[], short[], short[], short[], short[], short> Utok3(byte cil, bool team)       //dmg, otrava, team, 2cil, 3cil, vsechny cile; self-dmg
        {
            su = 2;
            short sTeam = 2;
            if (team) { sTeam = 1; }
            short uDmg = (Int16)(dmg * nasobicDMG);
            short[] prvniCil = { sTeam, cil, uDmg };
            short[] druhyCil = { };                  //team, cil, dmg, otrava, nasobicDMG, oziveni, armor
            short[] tretiCil = { };
            short[] ctvrtyCil = { };
            short[] celyTeam = { sTeam, -50, -1, -1, 1 };
            short[] celyTeam2 = { };            //team, dmg, otrava, nasobicDMG, oziveni
            short selfDmg = 9999;
            Console.WriteLine(jmeno + "používá speciální schopnost: sebeobětování");
            return Tuple.Create(prvniCil, druhyCil, tretiCil, ctvrtyCil, celyTeam, celyTeam2, selfDmg);

        }
        public override void Su(short pocetKol)
        {
            if (pocetKol == 8) su = 1;
        }
        public override bool PotrebujeCil(short cisloUtoku)
        {
            if (cisloUtoku == 3) return false; else return true;
        }
    }
}

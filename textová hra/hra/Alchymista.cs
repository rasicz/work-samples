using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hra
{
    class Alchymista:Jednotka
    {
        short pocetBojovniku;
        public Alchymista(short pocetBojovniku)
        {
            trida = "alchymista";
            hp = (Int16)(hp * 0.8);
            this.pocetBojovniku = pocetBojovniku;
        }
        public override bool[] UtokInfo()
        {
            bool[] moznyUtok = new bool[3];
            if (en >= 3) { moznyUtok[0] = true; Console.ForegroundColor = ConsoleColor.White; } else { Console.ForegroundColor = ConsoleColor.DarkGray; }
            Console.WriteLine("1. petarda (3 energie)");
            if (en >= 5) { moznyUtok[1] = true; Console.ForegroundColor = ConsoleColor.White; } else { Console.ForegroundColor = ConsoleColor.DarkGray; }
            Console.WriteLine("2. korozivní prach (5 energie)");
            if (su == 1) { moznyUtok[2] = true; Console.ForegroundColor = ConsoleColor.White; Console.WriteLine("3. speciální útok"); }

            Console.ForegroundColor = ConsoleColor.White;
            return moznyUtok;
        }
        public override Tuple<short[], short[], short[], short[], short[], short[], short> Utok(byte cil, bool team)       //dmg, otrava, team, 2cil, 3cil, vsechny cile; self-dmg
        {
            short uDmg = (Int16)(dmg * nasobicDMG * 0.6);
            short sTeam = 1;
            short pocetCilu = 1;
            if (team) { sTeam = 2; }
            Console.WriteLine("cíl: " + cil);
            byte cil2 = (Byte)(cil -1);            
            byte cil3 = (Byte)(cil +1);
            short[] prvniCil = { sTeam, cil, uDmg };
            short[] druhyCil;                  //team, cil, dmg, otrava, nasobicDMG, oziveni, armor
            if (cil > 0) { druhyCil = new short[3] { sTeam, cil2, uDmg }; pocetCilu++; } else druhyCil = new short[0] { };
            
            short[] tretiCil = { };
            if (cil < (pocetBojovniku - 1)) { tretiCil = new short[3] { sTeam, cil3, uDmg }; pocetCilu++; } else tretiCil = new short[0] { };
            short[] ctvrtyCil = { };
            short[] celyTeam = { };
            short[] celyTeam2 = { };                  //team, dmg, otrava, nasobicDMG, oziveni, armor
            Console.WriteLine(jmeno + " útočí " + pocetCilu + "x se silou " + uDmg);
            en -= 3;
            return Tuple.Create(prvniCil, druhyCil, tretiCil, ctvrtyCil, celyTeam, celyTeam2, (Int16)(0));

        }
        public override Tuple<short[], short[], short[], short[], short[], short[], short> Utok2(byte cil, bool team)       //dmg, otrava, team, 2cil, 3cil, vsechny cile; self-dmg
        {
            short sTeam = 1;
            if (team) { sTeam = 2; }
            byte cil2 = (Byte)(cil - 1);
            byte cil3 = (Byte)(cil + 1);
            short[] prvniCil = { sTeam, cil, 0, -1, 0, 0, -3 }; //team, cil, dmg, otrava, nasobicDMG, oziveni, armor   
            short[] druhyCil;
            if (cil > 0) { druhyCil = new short[7] { sTeam, cil2, 0, -1, 0, 0, -4 }; } else druhyCil = new short[0] { };

            short[] tretiCil;

            if (cil < (pocetBojovniku - 1)) { tretiCil = new short[7] { sTeam, cil3, 0, -1, 0, 0, -4 }; } else tretiCil = new short[0] { };
            short[] ctvrtyCil = { };
            short[] celyTeam2 = { };
            short[] celyTeam = { };           //team, dmg, otrava, nasobicDMG, oziveni, armor
            Console.WriteLine(jmeno + " používá schopnost korozivní prach");
            en -= 5;
            return Tuple.Create(prvniCil, druhyCil, tretiCil, ctvrtyCil, celyTeam, celyTeam2, (Int16)(0));

        }
        public override Tuple<short[], short[], short[], short[], short[], short[], short> Utok3(byte cil, bool team)       //dmg, otrava, team, 2cil, 3cil, vsechny cile; self-dmg
        {
            su = 2;
            short uDmg = (Int16)(dmg * nasobicDMG);
            short otrava = 3;
            short sTeam = 1;
            if (team) { sTeam = 2; }
            byte cil2 = (Byte)(cil - 1);
            byte cil3 = (Byte)(cil + 1);
            short[] prvniCil = { sTeam, cil2, uDmg, otrava };
            short[] druhyCil;                  //team, cil, dmg, otrava, nasobicDMG, oziveni, armor
            if (cil > 0) druhyCil = new short[4] { sTeam, cil2, uDmg , otrava};  else druhyCil = new short[0] { };

            short[] tretiCil = { };
            if (cil < (pocetBojovniku - 1))  tretiCil = new short[4] { sTeam, cil3, uDmg , otrava};  else tretiCil = new short[0] { };
            short[] ctvrtyCil = { };
            short[] celyTeam = { sTeam, (Int16)(uDmg * 0.5) };
            short[] celyTeam2 = { };                  //team, dmg, otrava, nasobicDMG, oziveni, armor
            Console.WriteLine(jmeno + " používá speciální schopnost: planenomet");
            return Tuple.Create(prvniCil, druhyCil, tretiCil, ctvrtyCil, celyTeam, celyTeam2, (Int16)(0));

        }
        public override void Su(short pocetKol)
        {
            if (pocetKol == 10) su = 1;
        }
        
    }
}

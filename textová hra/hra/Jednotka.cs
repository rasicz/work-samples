using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hra
{
    class Jednotka
    {
        public byte team = 0;
        public string trida = "zadny";
        public string jmeno;
        public string dlouheJmeno;
        public short hp; //zdravi
        public short dmg; //sila utoku
        public float nasobicDMG = 1;
        public short armor;
        public short en = 2; //energie
        public short otrava = 0;
        public byte su = 0; //speciální utok
        public bool zivi = true;
        public Random gen = new Random(Guid.NewGuid().GetHashCode());
        Menu menu = new Menu();
        public Jednotka()
        {
            jmeno = menu.jList();
            dlouheJmeno = jmeno;
            for(int i = jmeno.Length; i < 10; i++) { dlouheJmeno += " "; }
            hp = (Int16)gen.Next(20, 60);
            hp += (Int16)gen.Next(20, 60);
            hp += (Int16)gen.Next(20, 60);

            dmg = (Int16)gen.Next(9, 25); 
            dmg += (Int16)gen.Next(9, 25);
            armor = (Int16)gen.Next(0, 9);
            armor += (Int16)gen.Next(0, 9);
            if (gen.Next(0, 3) == 0) { armor = 0; }
        }
        public virtual void Obrana(short silaUtoku, bool brneni)
        {
            short zraneni;
            Console.ForegroundColor = ConsoleColor.Red;
            if (silaUtoku >= 0)
            {
                if (brneni) { zraneni = (Int16)(silaUtoku - armor); } else { zraneni = silaUtoku; }
                if (zraneni >= 0) hp -= zraneni; else zraneni = 0;
                if (zivi) Console.WriteLine(jmeno + " ztrácí " + zraneni + " životů"); else Console.WriteLine(jmeno + " je stále mrtvý");
            }
            else {
                silaUtoku *= -1;
                hp += silaUtoku;
                Console.WriteLine(jmeno + " získal/a " + silaUtoku + " životů");
            }
            if(hp <= 0) { hp = 0; zivi = false; }
            Console.ForegroundColor = ConsoleColor.White;
        }
        public void Info(bool energie)
        {
            if (zivi == false) Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(dlouheJmeno + " ");
            if(zivi)Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(trida);
            if (zivi) Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" [hp: ");
            if (zivi) Console.ForegroundColor = ConsoleColor.Magenta;
            if (hp < 10) Console.Write(" ");
            if (hp < 100) Console.Write(" ");
            Console.Write(hp);
            if (zivi) Console.ForegroundColor = ConsoleColor.White;
            Console.Write("][dmg: ");
            if (zivi) Console.ForegroundColor = ConsoleColor.Red;
            if (dmg < 10) Console.Write(" ");
            Console.Write(dmg);
            if (zivi) Console.ForegroundColor = ConsoleColor.White;
            if (energie)
            {
                Console.Write("][en: ");
                if (zivi) Console.ForegroundColor = ConsoleColor.Green;
                if (en < 10) Console.Write(" ");
                Console.Write(en);
            }
            if (zivi) Console.ForegroundColor = ConsoleColor.White;
            Console.Write("][ar: ");
            if (zivi) Console.ForegroundColor = ConsoleColor.Cyan;
            if (armor < 10 && armor >= 0) Console.Write(" ");
            Console.Write(armor);
            if (zivi) Console.ForegroundColor = ConsoleColor.White;
            Console.Write("]");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void FullInfo()
        {
            Info(true);
            if (zivi == false) Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("[otrava: ");
            if (zivi) Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(otrava);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("][nasobic dmg: ");
            if (zivi) Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write(nasobicDMG);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("]");
        } 
        public virtual bool[] UtokInfo() 
        {
            bool[] moznyUtok = new bool[3];
            if(en >= 2) { moznyUtok[0] = true; Console.ForegroundColor = ConsoleColor.White; } else { Console.ForegroundColor = ConsoleColor.DarkGray; }
            Console.WriteLine("1. první útok (2 energie)");
            if(en >= 4) { moznyUtok[1] = true; Console.ForegroundColor = ConsoleColor.White; } else { Console.ForegroundColor = ConsoleColor.DarkGray; }
            Console.WriteLine("2. druhý útok (4 energie)");
            if (su == 1) { moznyUtok[2] = true; Console.ForegroundColor = ConsoleColor.White; Console.WriteLine("3. speciální útok"); }
            
            Console.ForegroundColor = ConsoleColor.White;
            return moznyUtok;
        }
        //utok
        public virtual Tuple<short[], short[], short[], short[], short[], short[], short> Utok(byte cil, bool team)       //1cil, 2cil, 3cil, cely tym 1, cely tym 2; self-dmg
        {
            short uDmg = (Int16)(dmg * nasobicDMG); 
            short sTeam = 1;
            if (team) { sTeam = 2; }
            short[] prvniCil = {sTeam, cil, uDmg};
            short[] druhyCil = {};                  //team, cil, dmg, otrava, nasobicDMG, oziveni, armor
            short[] tretiCil = {};
            short[] ctvrtyCil = {};
            short[] celyTeam = {};
            short[] celyTeam2 = {};    //team, dmg, otrava, nasobicDMG, oziveni, armor
            short selfDmg = 0;
            if (team) { team = false; } else { team = true; }
            Console.WriteLine(jmeno + " útočí se silou " + uDmg);
            en -= 2;
            return Tuple.Create(prvniCil ,druhyCil, tretiCil, ctvrtyCil ,celyTeam, celyTeam2, selfDmg);

        }

        public virtual Tuple<short[], short[], short[], short[], short[], short[], short> Utok2(byte cil, bool team)       //dmg, otrava, team, 2cil, 3cil, vsechny cile; self-dmg
        {
            short uDmg = (Int16)(dmg * nasobicDMG);
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
        public virtual Tuple<short[], short[], short[], short[], short[], short[], short> Utok3(byte cil, bool team)       //dmg, otrava, team, 2cil, 3cil, vsechny cile; self-dmg
        {
            su = 2;
            short uDmg = (Int16)(dmg * nasobicDMG);
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
            return Tuple.Create(prvniCil, druhyCil, tretiCil, ctvrtyCil, celyTeam, celyTeam2, (Int16)0);
        }

        public void DalsiKolo(short pocetKol)
        {
            if (zivi) {
                en += 1;
                if (otrava > 0)
                {
                    hp -= 5;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(jmeno + " ztrácí 5 životů");
                    Console.ForegroundColor = ConsoleColor.White; 
                    otrava--;
                }
                Su(pocetKol);
            }
        }
        public virtual void Su(short pocetKol)
        {
            if (pocetKol == 5) su = 1;
        }

        public virtual bool PotrebujeCil(short cisloUtoku)
        {
            return true;
        }
       
    }
}

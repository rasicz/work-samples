using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hra
{
    class Jednotka
    {
        string trida = "zadny";
        string jmeno;
        public short hp; //zdravi
        short dmg; //sila utoku
        float nasobicDMG = 1;
        short armor;
        short en = 2; //energie
        short otrava = 0;
        byte su = 0; //speciální utok
        Random gen = new Random(Guid.NewGuid().GetHashCode());
        menu menu = new menu();
        public Jednotka()
        {
            jmeno = menu.jList();
            hp = (Int16)gen.Next(50, 301);
            dmg = (Int16)gen.Next(5, 51);
            armor = (Int16)gen.Next(0, 16);            
        }
       /** public void info()
        {
            Console.WriteLine("jmeno:" + jmeno + " hp:" + hp + " dmg:" + dmg + " energie: " + en + " armor:" + armor);
            
        }**/
        public int obrana(short silaUtoku)
        {   
            short zraneni = 0;
            zraneni = (Int16)(silaUtoku - armor);
            if(zraneni >= 0){
                hp -= zraneni;
            }else{
                zraneni = 0;
            }
            return (Int16)(zraneni);
        }
        public void info()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(trida);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" [hp: ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write(hp);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("][dmg: ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(dmg);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("][energie: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(en);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("][armor: ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(armor);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("]");
            Console.Write(trida);
        }
        public int utok()
        {
            int utok = (Int16)(dmg * nasobicDMG);
            return utok;
        }
    }
}

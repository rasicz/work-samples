using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hra
{

    class Program
    {       
        static void Main(string[] args)
        {
            
            menu menu = new menu();
         
            menu.HlavniMenu();
            
            Rytir test = new Rytir();
            test.info();
            Console.ReadKey();
        }
    }
}

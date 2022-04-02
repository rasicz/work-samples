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
            Console.Title = "War Age offline";
            Menu menu = new Menu();
            
            menu.HlavniMenu();



            Console.WriteLine("---konec---");
            Console.ReadKey();
        }
    }
}

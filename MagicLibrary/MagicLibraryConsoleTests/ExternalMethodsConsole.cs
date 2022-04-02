using Magic.MO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicLibraryConsoleTests
{
    class ExternalMethodsConsole : IMagicExternalMethods
    {
        public void Debug(string message)
        {
            Console.WriteLine("debug: " + message);
        }

        public float Distance(float angle)
        {
            Console.WriteLine("distance: " + angle);
            return 0;
        }

        public int Scan(float angle, float distance)
        {
            Console.WriteLine("scan: " + angle + " d: " + distance);
            return 0;
        }
    }
}

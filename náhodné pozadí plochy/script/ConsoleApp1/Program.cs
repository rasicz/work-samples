using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            WebClient net = new WebClient();
            net.Headers.Add("user-agent", "Only a test!");
            try
            {
                string input = net.DownloadString(@"https://boards.4chan.org/wg/");
                string[] seznam = input.Split(new string[] { "File: <a href=\"" }, StringSplitOptions.None);
                //string location = AppDomain.CurrentDomain.BaseDirectory;
                string location = @"D:\home\site\wwwroot\";
                byte b = 1;
                List<string> vystup = new List<string>();
                for (int i = 0; i < seznam.Length - 1; i++)
                {
                    //string s = seznam[i].Substring(seznam[i].IndexOf(@"</a>", seznam[i].IndexOf(',')));
                    //Console.WriteLine(s);
                    //Console.ReadKey();
                    seznam[i] = seznam[i + 1].Remove(seznam[i + 1].IndexOf('\"'));
                }
                foreach (string s in seznam)
                {
                    if (s != "" && s.Substring(s.LastIndexOf('.')) == ".jpg")
                    {
                        bool sfw = MakeRequest("https:" + s).Result;
                        Thread.Sleep(3000);
                        Console.WriteLine("s : " + sfw);
                        if(sfw) vystup.Add(s);
                        vystup.Add(s);
                        b++;
                    }
                    if (b >= 20) break;
                }
                vystup.RemoveAt(0);
                string[] sVystup = vystup.ToArray();
                List<string> obsah = File.ReadAllLines(location + @"list.txt").ToList();
                while (obsah.Count > 40) { Console.WriteLine(obsah.ElementAt(40)); obsah.RemoveAt(40); Console.WriteLine("removed: " + obsah.Count); }
                //Console.ReadKey();
                File.WriteAllLines(location + @"list.txt", sVystup.Union(obsah)); //
                Console.WriteLine(location + @"list.txt");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadKey();
            }
        }

        static async Task<bool> MakeRequest(string webpage)
        {
            var client = new HttpClient();

            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "");

            // Request parameters
            var uri = "https://randomimgmod.cognitiveservices.azure.com/contentmoderator/moderate/v1.0/ProcessImage/Evaluate";

            HttpResponseMessage response;

            // Request body
            byte[] byteData = Encoding.UTF8.GetBytes("{\"DataRepresentation\":\"URL\", \"Value\":\"" + webpage + "\"}");

            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                response = await client.PostAsync(uri, content);
            }
            string output = await response.Content.ReadAsStringAsync();
            Console.WriteLine(output); 
            if (output.Contains("IsImageAdultClassified\":false")) return (true); else return (false);
        }
    }
}
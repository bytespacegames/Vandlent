using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using WP.NET;

namespace Vandlent
{
    class Program
    {
        static string apiUrl;
        static string Read(string request)
        {
            Console.WriteLine(request);
            return Console.ReadLine();
        }

        static async Task Main(string[] args)
        {
            File.ReadAllText(Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + @"\bwfile.txt").Split(",");
            string username1 = Read("Username Prefix?");
            string username = username1 + "@" + Read("Username?");
            string password = Read("Password?");
            //apiUrl = "https://esolangs.org/w/api.php";
            apiUrl = "https://en.wikipedia.org/w/api.php";

            WPBot bot = new WPBot(username, password, apiUrl);
            bot.AddListener(new BotStartEvent());
            bot.AddListener(new BotRecieveRevisionEvent());

            while (true) { }
        }
    }
}

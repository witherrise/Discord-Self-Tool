using System;
using System.Media;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;using System.Xml.Schema;
using Newtonsoft.Json.Linq;
using Microsoft.Win32;
using Discord_Login;
using DiscordTokenLogin;



namespace dct
{
    class program
    {


        static void Main(string[] args)
        {


            while (true)
            {
                Console.Title = "dctool";
                banner();
                menu();

                ConsoleKeyInfo input = Console.ReadKey();
                char option = input.KeyChar;
                Console.WriteLine("");
                switch (option)
                {
                    case '1':
                        Discord_Login.Program.Main1(args);
                        break;
                    case '2':
                        DiscordTokenLogin.Program2.Main2(args);
                        break;
                    case '3':
                        SendWebhook();
                        break;
                    case '4':
                        return;
                }

            }

        }

        static void banner()
        {

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("██╗    ██╗██╗████████╗██╗  ██╗███████╗██████╗ ██████╗ ██╗███████╗███████╗");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("██║    ██║██║╚══██╔══╝██║  ██║██╔════╝██╔══██╗██╔══██╗██║██╔════╝██╔════╝");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("██║ █╗ ██║██║   ██║   ███████║█████╗  ██████╔╝██████╔╝██║███████╗█████╗  ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("██║███╗██║██║   ██║   ██╔══██║██╔══╝  ██╔══██╗██╔══██╗██║╚════██║██╔══╝  ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╚███╔███╔╝██║   ██║   ██║  ██║███████╗██║  ██║██║  ██║██║███████║███████╗");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(" ╚══╝╚══╝ ╚═╝   ╚═╝   ╚═╝  ╚═╝╚══════╝╚═╝  ╚═╝╚═╝  ╚═╝╚═╝╚══════╝╚══════╝");
            Console.WriteLine("\n by witherroom");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
        }

        
        static void menu()
        {
            Console.WriteLine("\n1. Grab Info");
            Console.WriteLine("2. Login");
            Console.WriteLine("3. Send webhook Message");
            Console.WriteLine("4. Exit");
        }
        


        static async void SendWebhook()
        {
            Console.Clear();
            while (true)
            {
                Send();
                Console.Write("\nPress backspace key to exit. Press any key to continue:  ");
                Console.Write("\n");
                if (Console.ReadKey().Key == ConsoleKey.Backspace) break;
                
            }

        }

        static async void Send()
        {
            Console.Write("Webhook URL: ");
            string webhook = Console.ReadLine();
            Console.Write("Message: ");
            string message = Console.ReadLine();

            string json = $"{{\"content\":\"{message}\"}}";

            using (HttpClient client = new HttpClient())
            {
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                await client.PostAsync(webhook, content);
            }
        }
        

    }

}


using System;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using Microsoft.Win32; 

namespace DiscordTokenLogin
{
    class Program2
    {
        static string GetJsCode(string strToken)
        {
            Regex TokenPattern = new Regex(@"(mfa\.[a-z0-9_-]{20,})|([a-z0-9_-]{23,28}\.[a-z0-9_-]{6,7}\.[a-z0-9_-]{27})", RegexOptions.None);
            MatchCollection Match = TokenPattern.Matches(strToken);
            if (Match.Count == 0) 
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("    Token is invalid.");

            }
            return "function login(token) { setInterval(() => { document.body.appendChild(document.createElement `iframe`).contentWindow.localStorage.token = `\"${token}\"` }, 50); setTimeout(() => { location.reload(); }, 2500); } login('" + strToken + "')";
        }
        
        static void UserDetails(string strToken)
        {

            try
            {
                HttpWebRequest Request = (HttpWebRequest)WebRequest.Create("https://discord.com/api/v9/users/@me");
                Request.Headers.Add("authorization", strToken);

                using HttpWebResponse Response = (HttpWebResponse)Request.GetResponse();
                Stream DataStream = Response.GetResponseStream();
                StreamReader Reader = new StreamReader(DataStream);
                var strData = Reader.ReadToEnd();
                JObject objData = JObject.Parse(strData);

                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"\n\n    ID:   {objData.GetValue("id")}\n    USERNAME:    {objData.GetValue("username")}#{objData.GetValue("discriminator")}\n    EMAIL:    {objData.GetValue("email")}\n    PHONE:    {objData.GetValue("phone")}\n    VERIFIED:    {objData.GetValue("verified")}");
                Reader.Close();
                DataStream.Close();
            }
            catch (WebException wex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                if (wex.Response != null)
                {
                    using (var errorResponse = (HttpWebResponse)wex.Response)
                    {
                        using (var reader = new StreamReader(errorResponse.GetResponseStream()))
                        {
                            string errorText = reader.ReadToEnd();
                            Console.WriteLine($"\n    Error: Cannot get user's information: {errorResponse.StatusCode} - {errorText}");
                            Console.Clear();
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"\n    Internet Issue: {wex.Message}");
                }
                Console.ForegroundColor = ConsoleColor.White; 
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n    Unexpected Error:   {ex.Message}");

            }
        }


        public static async System.Threading.Tasks.Task Main2(string[] args)
        {
            Console.Write("\n");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("\n    Token:    ");
            Console.ForegroundColor = ConsoleColor.White;
            string strToken = Console.ReadLine();
    
    
            Regex TokenPattern = new Regex(@"(mfa\.[a-z0-9_-]{20,})|([a-z0-9_-]{23,28}\.[a-z0-9_-]{6,7}\.[a-z0-9_-]{27})", RegexOptions.None);



            UserDetails(strToken); 
            
            Console.ReadKey(); 
            Console.Clear();
        }
    }
}
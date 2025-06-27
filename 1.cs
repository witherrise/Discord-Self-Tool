using System;

using System.Collections.Generic;

using System.Net.Http;

using System.Text;

using System.Threading.Tasks;

using Newtonsoft.Json;

using Newtonsoft.Json.Linq;


namespace Discord_Login

{

    internal class Program

    {

        public static async Task Main1(string[] args)

        {
            Console.WriteLine("Please Enter An Email Then Password To Get A UserID And Token");
            Console.WriteLine("               ");
            Console.Write("Enter Email: ");

            string email = Console.ReadLine();


            Console.Write("Enter Password: ");

            string pass = Console.ReadLine();

            Console.Clear();
            await GetToken(email, pass);
        }


        async static Task GetToken(string email, string pass)

        {

            string url = "https://discord.com/api/v9/auth/login";


            var payload = new Dictionary<string, string>()

            {

                { "login", email }, { "password", pass }

            };


            string jsonPayload = JsonConvert.SerializeObject(payload);


            HttpClient client = new HttpClient();


            StringContent stringContent = new StringContent(jsonPayload, Encoding.UTF8, "application/json");


            HttpResponseMessage response = await client.PostAsync(url, stringContent);


            if (response.IsSuccessStatusCode)

            {

                Console.WriteLine("\nLogin Successful!");


                string responseContent = await response.Content.ReadAsStringAsync();


                // Deserialize the JSON string into a JObject

                JObject json = JObject.Parse(responseContent);


                // Access the token and user ID from the JObject

                string token = json["token"].ToString();

                string userId = json["user_id"].ToString();


                Console.WriteLine($"User ID: {userId}");

                Console.WriteLine($"Token: {token}");
                Console.WriteLine("\nPress select an option to to choose another one");
                Console.ReadKey();
                Console.Clear();
                

            }

            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)

            {

                Console.WriteLine("\nError: Invalid email or password.");
                Console.WriteLine("\nPress select an option to to choose another one");
                Console.ReadKey();
                Console.Clear();
            }

            else

            {

                Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");

            }

        }

    }

}
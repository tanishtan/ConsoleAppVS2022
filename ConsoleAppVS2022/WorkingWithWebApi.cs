using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsoleAppVS2022
{
    public class WorkingWithWebApi
    {
        static string baseUrl = "http://localhost:5041";
        static string token = "";
        static  AuthResponse authResponse;
        internal static void test()
        {
            AuthenticateAPI();
            //System.Net.Http.HttpCLient class
            using(HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {authResponse.Token}");
                //client.DefaultRequestHeaders.Add("Content-Type", "application/json");
                var response = client.GetAsync("api/products").Result;
                Console.WriteLine(response);
                response.EnsureSuccessStatusCode();
                var result = response.Content.ReadFromJsonAsync<List<Product>>().Result;
                foreach(var item in result)
                {
                    item.Show();
                }
                Console.ReadKey();
            }
        }
        static void AuthenticateAPI()
        {
            var authObj = new { userName="admin", password="admin"};
            var authObjJson = JsonSerializer.Serialize(authObj);
            var jsonSerialerOptions = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };
            using(HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var response = client.PostAsync(
                    requestUri: "api/accounts/authenticate",
                    content: JsonContent.Create(inputValue: authObj, inputType: authObj.GetType(), mediaType: new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"))).Result;

                var result = response.Content.ReadFromJsonAsync<AuthResponse>().Result;
                Console.WriteLine(result);
                authResponse = result;
            }
        }
    }
    class AuthResponse
    {   
        public string FullName { get; set; }
        public int UserId { get; set; }
        public string Token { get; set; }
        public override string ToString()
        {
            return $"ID: {UserId}, FullName: {FullName}, Token: {Token}";
        }

    }
}

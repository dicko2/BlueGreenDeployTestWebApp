using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Hostsol.Demo.BlueGreenCheckConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            DoStuff(args[0]);
        }

        async static Task DoStuff(string Url)
        {
            while (true)
            {
                using (var client = new HttpClient())
                {
                    //"http://localhost:29977/MyObject/Index/2"

                    var response = GetResponseText(Url);
                    Console.WriteLine(response.Result.StatusCode.ToString());
                }
            }
        }
        public static async Task<HttpResponseMessage> GetResponseText(string address)
        {
            using (var httpClient = new HttpClient())
                return await httpClient.GetAsync(address);
        }

    }
}

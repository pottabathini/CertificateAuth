using System;
using System.IO;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;

namespace CertValidate
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Program p = new Program();
            p.GetApiDataAsync();
            Console.Read();
        }

        private async void GetApiDataAsync()
        {
            try
            {
                var cert = new X509Certificate2(Path.Combine("sts_dev_cert.pfx"), "1234");

                var client = new HttpClient();

                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri("https://localhost:44357/api/values"),
                    Method = HttpMethod.Get,
                };

                request.Headers.Add("X-ARR-ClientCert", cert.GetRawCertDataString());
                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                }

                //throw new ApplicationException($"Status code: {response.StatusCode}, Error: {response.ReasonPhrase}");
            }
            catch (Exception e)
            {
                throw new ApplicationException($"Exception {e}");
            }
        }
    }
}

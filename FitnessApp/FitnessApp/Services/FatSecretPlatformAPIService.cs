using FitnessApp.Common;
using FitnessApp.Interfaces;
using ModernHttpClient;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FitnessApp.Services
{
    public static class FatSecretPlatformAPIService
    {
        private const string APP_URL = "http://platform.fatsecret.com/rest/server.api";

        private const string HTTP_METHOD = "GET";

        private const int TIMEOUT = 3000;

        private static string Nonce()
        {
            Random r = new Random();
            string n = "";
            for (int i = 0; i < r.Next(8) + 2; i++)
            {
                n += ((char)(r.Next(26) + 'a')).ToString();
            }
            return n;
        }

        private static string Encode(string value)
        {
            StringBuilder escaped = new StringBuilder(Uri.EscapeDataString(value));

            escaped.Replace("+", "%20")
                .Replace("!", "%21")
                .Replace("*", "%2A")
                .Replace("\\", "%27")
                .Replace("(", "%28")
                .Replace(")", "%29");

            return escaped.ToString();
        }

        private static string[] GenerateOauthParams()
        {
            return new string[]
                {
                    "format=json",
                    "oauth_consumer_key=" + Constants.FATSECRET_API_KEY,
                    "oauth_nonce=" + Nonce(),
                    "oauth_signature_method=HMAC-SHA1",
                    "oauth_timestamp=" + new DateTimeOffset(DateTime.UtcNow).ToString(),
                    "oauth_version=1.0"
                };
        }

        private static string CreateSignature(string method, string uri, string[] parameters)
        {
            string[] p = new string[]
            {
                method,
                Encode(uri),
                Encode(String.Join("&", parameters))
            };
            string text = String.Join("&", p);
            string key = Constants.FATSECRET_SECRET + "&";

            IEncodingHelper encoder = DependencyService.Get<IEncodingHelper>();
            string sign = encoder.Encode(text, key);

            return sign;
        }

        public static async Task<string> GetFoods(string keyword)
        {
            try
            {
                List<string> parameters = new List<string>(GenerateOauthParams())
                {
                    "method=foods.search",
                    "max_results=50",
                    "search_expression=" + Encode(keyword)
                };
                parameters.Add("oauth_signature=" + CreateSignature(HTTP_METHOD, APP_URL, parameters.ToArray()));

                var requestURI = $"{APP_URL}?{String.Join("&", parameters)}";

                var client = new HttpClient(new NativeMessageHandler())
                {
                    Timeout = TimeSpan.FromSeconds(TIMEOUT)
                };
                var request = new HttpRequestMessage(HttpMethod.Get, requestURI);
                var response = await client.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Request denied.");
                    return null;
                }

                var result = await response.Content.ReadAsStringAsync();

                if (result == null)
                {
                    Debug.WriteLine("FatSecret API has returned nothing.");
                    return null;
                }

                JObject json = null;
                try
                {
                    json = JObject.Parse(result);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.InnerException);
                    return null;
                }

                if (json["status"].ToString() == "VALID")
                {
                    return null;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.InnerException);
                return null;
            }
        }
    }
}

using FitnessApp.Interfaces;
using FitnessApp.iOS.Helpers;
using System;
using System.Security.Cryptography;
using System.Text;
using Xamarin.Forms;

[assembly: Dependency(typeof(EncodingHelper))]
namespace FitnessApp.iOS.Helpers
{
    public class EncodingHelper : IEncodingHelper
    {
        public string Encode(string data, string key)
        {
            byte[] secretKey = Encoding.UTF8.GetBytes(key);

            HMACSHA1 hmac = new HMACSHA1(secretKey);
            hmac.Initialize();

            byte[] bytes = Encoding.UTF8.GetBytes(data);
            byte[] rawHmac = hmac.ComputeHash(bytes);
            string result = Convert.ToBase64String(rawHmac);

            return result;
        }
    }
}

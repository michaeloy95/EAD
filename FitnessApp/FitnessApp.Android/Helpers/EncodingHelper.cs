using FitnessApp.Droid.Helpers;
using FitnessApp.Interfaces;
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Xamarin.Forms;

[assembly: Dependency(typeof(EncodingHelper))]
namespace FitnessApp.Droid.Helpers
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
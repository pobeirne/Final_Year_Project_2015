using System;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace LuckyMe.CMS.WebAPI.Extensions
{
    public static class StringExtension
    {
        public static string GenerateAppSecretProof(this string accessToken)
        {
            using (
                var algorithm =
                    new HMACSHA256(Encoding.ASCII.GetBytes(ConfigurationManager.AppSettings["Facebook_AppSecret"])))
            {
                var hash = algorithm.ComputeHash(Encoding.ASCII.GetBytes(accessToken));
                var builder = new StringBuilder();
                foreach (var t in hash)
                {
                    builder.Append(t.ToString("x2", CultureInfo.InvariantCulture));
                }
                return builder.ToString();
            }
        }

        public static string GraphApiCall(this string baseGraphApiCall, params object[] args)
        {
           
            if (string.IsNullOrEmpty(baseGraphApiCall)) return string.Empty;
            if (args == null || !args.Any())
                throw new Exception(
                    "GraphAPICall requires at least one string parameter that contains the appsecret_proof value.");
            
            var graphApiCall = baseGraphApiCall.Contains("?")
                ? string.Format(baseGraphApiCall + "&appsecret_proof={" + (args.Count() - 1) + "}", args)
                : string.Format(baseGraphApiCall + "?appsecret_proof={" + (args.Count() - 1) + "}", args);

            
            return string.Format("v2.3/{0}", graphApiCall);
        }
    }
}
using System.Security.Claims;

namespace Client.Helper
{
    public static class JwtParser
    {
        public static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var claims = new List<Claim>();
            string payload = jwt.Split('.')[1];

            byte[] jsonBytes = ParseBase64WithoutPadding(payload);

            if(jsonBytes.Any())
            {
                Dictionary<string, object> keyValuePairs = jsonBytes.Deserialize<Dictionary<string, object>>();
                if(keyValuePairs != null && keyValuePairs.Any())
                {
                    foreach (var kvp in keyValuePairs)
                    {
                        object value = kvp.Value ?? new();
                        string claimValue = $"{value}";
                        claims.Add(new(kvp.Key, claimValue));
                    }
                }
            }
            return claims;
        }
        private static byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }
    }
}

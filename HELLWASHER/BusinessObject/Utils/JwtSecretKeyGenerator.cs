using System;
using System.Security.Cryptography;
using System.Text;

public static class JwtSecretKeyGenerator
{
    public static string GenerateSecretKey(int length = 32)
    {
        using (var rng = new RNGCryptoServiceProvider())
        {
            var byteArray = new byte[length];
            rng.GetBytes(byteArray);
            return Convert.ToBase64String(byteArray);
        }
    }
}

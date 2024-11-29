using Net.payOS.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Utils.PayOs
{
    public class PayOSUtils
    {
        private const string SecretKey = "your-secret-key"; // Replace with your PayOS secret key

        public static string GenerateSignature(PaymentData paymentData)
        {
            // Concatenate required fields in the correct order
            string rawData = $"{paymentData.orderCode}{paymentData.amount}{paymentData.description}{paymentData.returnUrl}{paymentData.cancelUrl}";

            // Append the secret key
            string rawDataWithKey = rawData + SecretKey;

            // Hash the data using HMAC-SHA256
            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(SecretKey)))
            {
                byte[] hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(rawDataWithKey));

                // Convert hash to hexadecimal string
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }
    }
}

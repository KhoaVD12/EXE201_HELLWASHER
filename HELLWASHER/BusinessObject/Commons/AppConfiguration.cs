using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Commons
{
    public class AppConfiguration
    {
        public string DefaultConnection { get; set; }
        public JWTSection JWTSection { get; set; }

        public AppConfiguration()
        {
            JWTSection = new JWTSection
            {
                SecretKey = "InotX7Z9U7xmCP7AQRmzwZkhmv7iJcW9Vw7+Q1V3lIo=",
                Issuer = "http://localhost:5295",
                Audience = "http://localhost:5295"
            };
        }
    }

    public class JWTSection
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}

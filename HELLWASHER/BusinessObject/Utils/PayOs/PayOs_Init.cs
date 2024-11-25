using Microsoft.Extensions.Configuration;
using Net.payOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Utils.PayOs
{
    public static class PayOs_Init
    {
        public static PayOS InitializePayOS(IConfiguration configuration)
        {
            return new PayOS(
                configuration["Environment:PAYOS_CLIENT_ID"] ?? throw new Exception("Cannot find environment"),
                configuration["Environment:PAYOS_API_KEY"] ?? throw new Exception("Cannot find environment"),
                configuration["Environment:PAYOS_CHECKSUM_KEY"] ?? throw new Exception("Cannot find environment"),
                configuration["Environment:PAYOS_PARTNER_CODE"] ?? throw new Exception("Cannot find environment")
            );
        }
    }
}

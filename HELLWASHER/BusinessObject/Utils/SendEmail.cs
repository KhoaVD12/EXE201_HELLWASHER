using BusinessObject.ViewModels.OrderDTO;
using MailKit.Security;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using MailKit.Net.Smtp;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Utils
{
    public class SendEmail
    {
        public static async Task<bool> SendOrderEmail(ShowOrderEmailDTO orderEmailDto, string toEmail)
        {
            var userName = "WashShop";
            var emailFrom = "thongsieusao3@gmail.com";
            var password = "wktk xcae fwxp vqwb";

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(userName, emailFrom));
            message.To.Add(new MailboxAddress("", toEmail));
            message.Subject = "Invoice for Your Recent Order";

            // Create a variable to hold the service checkout rows
            var serviceRows = new StringBuilder();
            foreach (var serviceCheckout in orderEmailDto.ServiceCheckouts)
            {
                serviceRows.Append($@"
        <tr>
            <td>{serviceCheckout.Service.Name}</td>
            <td>{serviceCheckout.Weight}</td>
            <td>{serviceCheckout.TotalPricePerService:C}</td>
        </tr>");
            }

            // Create a variable to hold the product checkout rows
            var productRows = new StringBuilder();
            foreach (var productCheckout in orderEmailDto.ProductCheckouts)
            {
                productRows.Append($@"
        <tr>
            <td>{productCheckout.Product.Name}</td>
            <td>{productCheckout.QuantityPerProduct}</td>
            <td>{productCheckout.TotalPricePerProduct:C}</td>
        </tr>");
            }

            message.Body = new TextPart("html")
            {
                Text = $@"
    <html>
    <head>
        <style>
            body {{
                font-family: Arial, sans-serif;
                margin: 0;
                padding: 0;
                background-color: #f4f6f9;
                color: #333;
            }}
            .container {{
                width: 90%;
                max-width: 800px;
                background-color: #fff;
                margin: 20px auto;
                padding: 20px;
                border-radius: 8px;
                box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
            }}
            h1 {{
                color: #0056b3;
                text-align: center;
                margin-bottom: 20px;
            }}
            h2 {{
                color: #444;
                margin-bottom: 10px;
                border-bottom: 2px solid #0056b3;
                padding-bottom: 5px;
            }}
            .info-table {{
                width: 100%;
                border-collapse: collapse;
                margin: 20px 0;
            }}
            .info-table th, .info-table td {{
                padding: 10px;
                border: 1px solid #ddd;
                text-align: left;
            }}
            .info-table th {{
                background-color: #0056b3;
                color: white;
                text-transform: uppercase;
            }}
            .info-table td {{
                background-color: #f9f9f9;
            }}
            .total-summary {{
                text-align: right;
                font-weight: bold;
                font-size: 1.2em;
                margin-top: 10px;
                color: #0056b3;
            }}
            .footer {{
                text-align: center;
                font-size: 0.9em;
                color: #666;
                margin-top: 20px;
            }}
        </style>
    </head>
    <body>
        <div class='container'>
            <h1>Order Invoice</h1>
            
            <h2>Customer Information</h2>
            <table class='info-table'>
                <tr><th>Name</th><td>{orderEmailDto.CustomerName}</td></tr>
                <tr><th>Phone</th><td>{orderEmailDto.CusomterPhone}</td></tr>
                <tr><th>Email</th><td>{orderEmailDto.CustomerEmail}</td></tr>
                <tr><th>Delivery Address</th><td>{orderEmailDto.Address}</td></tr>
            </table>
            
            <h2>Order Details</h2>
            <table class='info-table'>
                <tr><th>Date Placed</th><td>{orderEmailDto.OrderDate}</td></tr>
                <tr><th>Total Amount</th><td>{orderEmailDto.TotalPrice:C}</td></tr>
            </table>
            
            {GenerateTableSection("Service List", "Service", "Weight", "Price", serviceRows.ToString(), orderEmailDto.TotalService)}
            
            {GenerateTableSection("Product List", "Product", "Quantity", "Price", productRows.ToString(), orderEmailDto.TotalProduct)}
            
            <div class='footer'>
                Thank you for shopping with us! If you have any questions, feel free to contact our support team.
            </div>
        </div>
    </body>
    </html>
    "
            };

            string GenerateTableSection(string title, string col1, string col2, string col3, string rows, decimal total)
            {
                return $@"
    <h2>{title}</h2>
    <table class='info-table'>
        <thead>
            <tr>
                <th>{col1}</th>
                <th>{col2}</th>
                <th>{col3}</th>
            </tr>
        </thead>
        <tbody>
            {rows}
        </tbody>
    </table>
    <div class='total-summary'>Total {title}: {total:C}</div>";
            }





            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                client.Authenticate(emailFrom, password);

                try
                {
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                    return true;
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex.Message);
                    return false;
                }
            }
        }
    }
}
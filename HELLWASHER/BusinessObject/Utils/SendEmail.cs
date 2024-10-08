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
        public static async Task<bool> SendOrderEmail(OrderDTO orderEmailDto, string toEmail)
        {
            var userName = "WashShop";
            var emailFrom = "thongsieusao3@gmail.com";
            var password = "wktk xcae fwxp vqwb";

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(userName, emailFrom));
            message.To.Add(new MailboxAddress("", toEmail));
            message.Subject = "Order Payment Successful";

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
                display: flex;
                justify-content: center;
                align-items: center;
                height: 100vh;
            }}
            .container {{
                width: 80%;
                margin: auto;
            }}
            .content {{
                text-align: center;
            }}
            table {{
                width: 100%;
                border-collapse: collapse;
            }}
            th, td {{
                padding: 10px;
                border: 1px solid #ddd;
                text-align: left;
            }}
            th {{
                background-color: #f2f2f2;
            }}
        </style>
    </head>
    <body>
        <div class='container'>
            <div class='content'>
                <h1>Thank you {orderEmailDto.UserName} for using our service!</h1>
                <p>Your order has been confirmed successfully.</p>
                <h2>Order Details</h2>
                <table>
                    <thead>
                        <tr>
                            <th>Name</th>
                            <th>Addess</th>
                            <th>Pick Up Date</th>
                            <th>Total</th>
                        </tr>
                    </thead>
                    <tbody>
                        <td>{orderEmailDto.UserName}</td>
                        <td>{orderEmailDto.Address}</td>
                        <td>{orderEmailDto.PickUpDate}</td>
                        <td>{orderEmailDto.TotalPrice:C}</td>
                    </tbody>
                    <tfoot>
                        <tr>
                            <td colspan='3' style='text-align:right'><strong>Total Price:</strong></td>
                            <td>{orderEmailDto.TotalPrice:C}</td>
                        </tr>
                    </tfoot>
                </table>
            </div>
        </div>
    </body>
</html>"
            };

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
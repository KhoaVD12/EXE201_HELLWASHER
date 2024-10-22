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
            <td>{serviceCheckout.QuantityPerService}</td>
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
            <td>{productCheckout.QuantityPerService}</td>
            <td>{productCheckout.TotalPricePerService:C}</td>
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
                background-color: #eaf2f8;
                color: #333;
            }}
            .container {{
                width: 80%;
                background-color: #fff;
                margin: 20px auto;
                padding: 20px;
                border-radius: 8px;
                box-shadow: 0 0 10px rgba(0,0,0,0.1);
            }}
            h1, h2 {{
                color: #007BFF;
                text-align: center;
            }}
            .section {{
                padding: 15px;
                border-radius: 8px;
                margin-bottom: 20px;
            }}
            .section-title {{
                background-color: #007BFF;
                color: white;
                padding: 10px;
                border-radius: 6px;
            }}
            .info-table {{
                width: 100%;
                margin-top: 10px;
                border-spacing: 0;
            }}
            .info-table th, .info-table td {{
                padding: 12px;
                border-bottom: 1px solid #ddd;
                text-align: left;
            }}
            .status-table th, .status-table td {{
                padding: 12px;
                text-align: center;
            }}
            .status-table tr {{
                border-bottom: 1px solid #ddd;
            }}
            .info-table th {{
                background-color: #f8f9fa;
                color: #333;
            }}
            .total-summary {{
                text-align: right;
                padding: 10px;
                font-weight: bold;
            }}
            .footer {{
                text-align: center;
                margin-top: 20px;
                font-size: 12px;
                color: #888;
            }}
        </style>
    </head>
    <body>
        <div class='container'>
            <h1>Order Details</h1>

            <!-- Customer Information -->
            <div class='section'>
                <h2 class='section-title'>Customer Information</h2>
                <table class='info-table'>
                    <tr>
                        <th>Name</th>
                        <td>{orderEmailDto.CustomerName}</td>
                    </tr>
                    <tr>
                        <th>Phone</th>
                        <td>{orderEmailDto.CusomterPhone}</td>
                    </tr>
                    <tr>
                        <th>Email</th>
                        <td>{orderEmailDto.CustomerEmail}</td>
                    </tr>
                    <tr>
                        <th>Delivery Address</th>
                        <td>{orderEmailDto.Address}</td>
                    </tr>
                </table>
            </div>

            <!-- Order Information -->
            <div class='section'>
                <h2 class='section-title'>Order Confirmed</h2>
                <table class='info-table'>
                    <tr>
                        <th>Date Placed</th>
                        <td>{orderEmailDto.OrderDate}</td>
                    </tr>
                    <tr>
                        <th>Total Amount</th>
                        <td>{orderEmailDto.TotalPrice:C}</td>
                    </tr>
                </table>
            </div>

            <!-- Service List (Display only if there are services) -->
            {(orderEmailDto.ServiceCheckouts != null && orderEmailDto.ServiceCheckouts.Any() ? $@"
            <div class='section'>
                <h2 class='section-title'>Service List</h2>
                <table class='info-table'>
                    <thead>
                        <tr>
                            <th>Service</th>
                            <th>Quantity</th>
                            <th>Price</th>
                        </tr>
                    </thead>
                    <tbody>
                        {serviceRows.ToString()}
                    </tbody>
                </table>
                <div class='total-summary'>Total Service Price: {orderEmailDto.TotalService:C}</div>
            </div>" : string.Empty)}

            <!-- Product List (Display only if there are products) -->
            {(orderEmailDto.ProductCheckouts != null && orderEmailDto.ProductCheckouts.Any() ? $@"
            <div class='section'>
                <h2 class='section-title'>Product List</h2>
                <table class='info-table'>
                    <thead>
                        <tr>
                            <th>Product</th>
                            <th>Quantity</th>
                            <th>Price</th>
                        </tr>
                    </thead>
                    <tbody>
                        {productRows.ToString()}
                    </tbody>
                </table>
                <div class='total-summary'>Total Product Price: {orderEmailDto.TotalProduct:C}</div>
            </div>" : string.Empty)}
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
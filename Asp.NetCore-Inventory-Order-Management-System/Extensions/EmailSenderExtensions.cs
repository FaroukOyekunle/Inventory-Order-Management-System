using System;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Encodings.Web;
using System.Collections.Generic;
using Asp.NetCore_Inventory_Order_Management_System.Services;

namespace Asp.NetCore_Inventory_Order_Management_System.Services
{
    public static class EmailSenderExtensions
    {
        public static Task SendEmailConfirmationAsync(this IEmailSender emailSender, string email, string link)
        {
            return emailSender.SendEmailAsync(email, "Confirm your email",
                $"Please confirm your account by clicking this link: <a href='{HtmlEncoder.Default.Encode(link)}'>link</a>");
        }
    }
}

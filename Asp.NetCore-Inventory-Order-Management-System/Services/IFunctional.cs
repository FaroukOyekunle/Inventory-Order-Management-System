using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
﻿using Microsoft.AspNetCore.Hosting;

namespace Asp.NetCore_Inventory_Order_Management_System.Services
{
    public interface IFunctional
    {
        Task InitAppData();

        Task CreateDefaultSuperAdmin();

        Task SendEmailBySendGridAsync(string apiKey, 
            string fromEmail, 
            string fromFullName, 
            string subject, 
            string message, 
            string email);

        Task SendEmailByGmailAsync(string fromEmail,
            string fromFullName,
            string subject,
            string messageBody,
            string toEmail,
            string toFullName,
            string smtpUser,
            string smtpPassword,
            string smtpHost,
            int smtpPort,
            bool smtpSSL);

        Task<string> UploadFile(List<IFormFile> files, IWebHostEnvironment env, string uploadFolder);
    }
}

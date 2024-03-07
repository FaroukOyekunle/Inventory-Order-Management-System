﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Asp.NetCore_Inventory_Order_Management_System.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}

using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Asp.NetCore_Inventory_Order_Management_System.Services
{
    public class SmtpOptions
    {
        public string fromEmail { get; set; }
        public string fromFullName { get; set; }
        public string smtpUserName { get; set; }
        public string smtpPassword { get; set; }
        public string smtpHost { get; set; }
        public int smtpPort { get; set; }
        public bool smtpSSL { get; set; }
        public bool IsDefault { get; set; }

    }
}

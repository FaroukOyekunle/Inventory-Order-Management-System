using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Asp.NetCore_Inventory_Order_Management_System.Services
{
    public class SendGridOptions
    {
        public string SendGridUser { get; set; }
        public string SendGridKey { get; set; }
        public string FromEmail { get; set; }
        public string FromFullName { get; set; }
        public bool IsDefault { get; set; }
    }
}

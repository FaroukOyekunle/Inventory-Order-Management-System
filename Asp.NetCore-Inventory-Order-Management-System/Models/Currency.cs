using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Asp.NetCore_Inventory_Order_Management_System.Models
{
    public class Currency
    {
        public int CurrencyId { get; set; }
        [Required]
        public string CurrencyName { get; set; }
        [Required]
        public string CurrencyCode { get; set; }
        public string Description { get; set; }
    }
}

using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Asp.NetCore_Inventory_Order_Management_System.Models
{
    public class ProductType
    {
        public int ProductTypeId { get; set; }
        [Required]
        public string ProductTypeName { get; set; }
        public string Description { get; set; }
    }
}

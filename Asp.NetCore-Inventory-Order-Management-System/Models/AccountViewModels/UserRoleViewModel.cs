using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Asp.NetCore_Inventory_Order_Management_System.Models.AccountViewModels
{
    public class UserRoleViewModel
    {
        public int CounterId { get; set; }
        public string ApplicationUserId { get; set; }
        public string RoleName { get; set; }
        public bool IsHaveAccess { get; set; }
    }
}

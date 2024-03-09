using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Asp.NetCore_Inventory_Order_Management_System.Services
{
    public interface IRoles
    {
        Task GenerateRolesFromPagesAsync();

        Task AddToRoles(string applicationUserId);
    }
}

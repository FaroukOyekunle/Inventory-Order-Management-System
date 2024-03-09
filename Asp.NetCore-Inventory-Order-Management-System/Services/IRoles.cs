using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Asp.NetCore_Inventory_Order_Management_System.Services
{
    public interface IRoles
    {
        Task GenerateRolesFromPagesAsync();

        Task AddToRoles(string applicationUserId);
    }
}

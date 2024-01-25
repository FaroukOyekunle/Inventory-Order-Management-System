using Asp.NetCore_Inventory_Order_Management_System.Services;

namespace Asp.NetCore_Inventory_Order_Management_System.Data
{
    public static class DbInitializer
    {
        public static async Task Initialize(ApplicationDbContext context,
           IFunctional functional)
        {
            context.Database.EnsureCreated();

            //check for users
            if (context.ApplicationUser.Any())
            {
                return; //if user is not empty, DB has been seed
            }

            //init app with super admin user
            await functional.CreateDefaultSuperAdmin();

            //init app data
            await functional.InitAppData();
        }
    }
}

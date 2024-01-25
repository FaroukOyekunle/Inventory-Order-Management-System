using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Asp.NetCore_Inventory_Order_Management_System.Data;
using Asp.NetCore_Inventory_Order_Management_System.Models;
using Asp.NetCore_Inventory_Order_Management_System.Models.ManageViewModels;
using Asp.NetCore_Inventory_Order_Management_System.Models.SyncfusionViewModels;

namespace Asp.NetCore_Inventory_Order_Management_System.Controllers.Api
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/ChangePassword")]
    public class ChangePasswordController : Controller
    {
        private readonly ApplicationDbContext _applicationContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ChangePasswordController(ApplicationDbContext context,
                        UserManager<ApplicationUser> userManager,
                        RoleManager<IdentityRole> roleManager)
        {
            _applicationContext = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: api/ChangePassword
        [HttpGet]
        public IActionResult GetChangePassword()
        {
            List<ApplicationUser> Items = new List<ApplicationUser>();
            Items = _applicationContext.Users.ToList();
            int Count = Items.Count();
            return Ok(new { Items, Count });
        }

        //POST: api/Update
        [HttpPost("[action]")]
        public async Task<IActionResult> Update([FromBody]CrudViewModel<ChangePasswordViewModel> payload)
        {
            ChangePasswordViewModel changePasswordViewModel = payload.value;
            var user = _applicationContext.Users.SingleOrDefault(x => x.Id.Equals(changePasswordViewModel.Id));

            if (user != null &&
                changePasswordViewModel.NewPassword.Equals(changePasswordViewModel.ConfirmPassword))
            {
                await _userManager.ChangePasswordAsync(user, changePasswordViewModel.OldPassword, changePasswordViewModel.NewPassword);
            }

            return Ok();
        } 
    }
}

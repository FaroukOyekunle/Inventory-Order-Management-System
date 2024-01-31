using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Asp.NetCore_Inventory_Order_Management_System.Data;
using Asp.NetCore_Inventory_Order_Management_System.Models;
using Asp.NetCore_Inventory_Order_Management_System.Models.SyncfusionViewModels;

namespace Asp.NetCore_Inventory_Order_Management_System.Controllers.Api
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/CustomerType")]
    public class CustomerTypeController : Controller
    {
        private readonly ApplicationDbContext _applicationContext;

        public CustomerTypeController(ApplicationDbContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        // GET: api/CustomerType
        [HttpGet]
        public async Task<IActionResult> GetCustomerType()
        {
            List<CustomerType> Items = await _applicationContext.CustomerType.ToListAsync();
            int Count = Items.Count();
            return Ok(new { Items, Count });
        }

        // POST: api/CustomerType
        [HttpPost("[action]")]
        public IActionResult Insert([FromBody]CrudViewModel<CustomerType> payload)
        {
            CustomerType customerType = payload.value;
            _applicationContext.CustomerType.Add(customerType);
            _applicationContext.SaveChanges();
            return Ok(customerType);
        }

        // POST: api/CustomerType
        [HttpPost("[action]")]
        public IActionResult Update([FromBody]CrudViewModel<CustomerType> payload)
        {
            CustomerType customerType = payload.value;
            _applicationContext.CustomerType.Update(customerType);
            _applicationContext.SaveChanges();
            return Ok(customerType);
        }

        // POST: api/CustomerType
        [HttpPost("[action]")]
        public IActionResult Remove([FromBody]CrudViewModel<CustomerType> payload)
        {
            CustomerType customerType = _applicationContext.CustomerType
                .Where(x => x.CustomerTypeId == (int)payload.key)
                .FirstOrDefault();
            _applicationContext.CustomerType.Remove(customerType);
            _applicationContext.SaveChanges();
            return Ok(customerType);
        }
    }
}

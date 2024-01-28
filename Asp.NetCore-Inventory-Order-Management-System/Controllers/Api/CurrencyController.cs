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
    [Route("api/Currency")]
    public class CurrencyController : Controller
    {
        private readonly ApplicationDbContext _applicationContext;

        public CurrencyController(ApplicationDbContext context)
        {
            _applicationContext = context;
        }

        // GET: api/Currency
        [HttpGet]
        public async Task<IActionResult> GetCurrency()
        {
            List<Currency> Items = await _applicationContext.Currency.ToListAsync();
            int Count = Items.Count();
            return Ok(new { Items, Count });
        }

        // GET: api/Currency
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetByBranchId([FromRoute]int id)
        {
            Branch branch = new Branch();
            Currency currency = new Currency();
            branch = await _applicationContext.Branch.SingleOrDefaultAsync(x => x.BranchId.Equals(id));
            if (branch != null && branch.CurrencyId != 0)
            {
                currency = await _applicationContext.Currency.SingleOrDefaultAsync(x => x.CurrencyId.Equals(branch.CurrencyId));
                
            }
            return Ok(currency);
        }

        // POST: api/Currency
        [HttpPost("[action]")]
        public IActionResult Insert([FromBody]CrudViewModel<Currency> payload)
        {
            Currency currency = payload.value;
            _applicationContext.Currency.Add(currency);
            _applicationContext.SaveChanges();
            return Ok(currency);
        }

        //POST: api/Currency
        [HttpPost("[action]")]
        public IActionResult Update([FromBody]CrudViewModel<Currency> payload)
        {
            Currency currency = payload.value;
            _applicationContext.Currency.Update(currency);
            _applicationContext.SaveChanges();
            return Ok(currency);
        }

        //POST: api/Currency
        [HttpPost("[action]")]
        public IActionResult Remove([FromBody]CrudViewModel<Currency> payload)
        {
            Currency currency = _applicationContext.Currency
                .Where(x => x.CurrencyId == (int)payload.key)
                .FirstOrDefault();
            _applicationContext.Currency.Remove(currency);
            _applicationContext.SaveChanges();
            return Ok(currency);
        }
    }
}

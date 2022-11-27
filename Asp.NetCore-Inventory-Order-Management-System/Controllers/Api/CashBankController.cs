using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Asp.NetCore_Inventory_Order_Management_System.Data;
using Asp.NetCore_Inventory_Order_Management_System.Models;
using Asp.NetCore_Inventory_Order_Management_System.Models.SyncfusionViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Asp.NetCore_Inventory_Order_Management_System.Controllers.Api
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/CashBank")]
    public class CashBankController : Controller
    {
        private readonly ApplicationDbContext _applicationContext;

        public CashBankController(ApplicationDbContext context)
        {
            _applicationContext = context;
        }

        // GET: api/CashBank
        [HttpGet]
        public async Task<IActionResult> GetCashBank()
        {
            List<CashBank> Items = await _applicationContext.CashBank.ToListAsync();
            int Count = Items.Count();
            return Ok(new { Items, Count });
        }

        [HttpPost("[action]")]
        public IActionResult Insert([FromBody]CrudViewModel<CashBank> payload)
        {
            CashBank cashBank = payload.value;
            _applicationContext.CashBank.Add(cashBank);
            _applicationContext.SaveChanges();
            return Ok(cashBank);
        }

        [HttpPost("[action]")]
        public IActionResult Update([FromBody]CrudViewModel<CashBank> payload)
        {
            CashBank cashBank = payload.value;
            _applicationContext.CashBank.Update(cashBank);
            _applicationContext.SaveChanges();
            return Ok(cashBank);
        }

        [HttpPost("[action]")]
        public IActionResult Remove([FromBody]CrudViewModel<CashBank> payload)
        {
            CashBank cashBank = _applicationContext.CashBank
                .Where(x => x.CashBankId == (int)payload.key)
                .FirstOrDefault();
            _applicationContext.CashBank.Remove(cashBank);
            _applicationContext.SaveChanges();
            return Ok(cashBank);
        }
    }
}
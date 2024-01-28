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
    [Route("api/BillType")]
    public class BillTypeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BillTypeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/BillType
        [HttpGet]
        public async Task<IActionResult> GetBillType()
        {
            List<BillType> Items = await _context.BillType.ToListAsync();
            int Count = Items.Count();
            return Ok(new { Items, Count });
        }

        // POST: api/BillType
        [HttpPost("[action]")]
        public IActionResult Insert([FromBody]CrudViewModel<BillType> payload)
        {
            BillType billType = payload.value;
            _context.BillType.Add(billType);
            _context.SaveChanges();
            return Ok(billType);
        }

        // POST: api/BillType
        [HttpPost("[action]")]
        public IActionResult Update([FromBody]CrudViewModel<BillType> payload)
        {
            BillType billType = payload.value;
            _context.BillType.Update(billType);
            _context.SaveChanges();
            return Ok(billType);
        }

        // POST: api/BillType
        [HttpPost("[action]")]
        public IActionResult Remove([FromBody]CrudViewModel<BillType> payload)
        {
            BillType billType = _context.BillType
                .Where(x => x.BillTypeId == (int)payload.key)
                .FirstOrDefault();
            _context.BillType.Remove(billType);
            _context.SaveChanges();
            return Ok(billType);
        }
    }
}

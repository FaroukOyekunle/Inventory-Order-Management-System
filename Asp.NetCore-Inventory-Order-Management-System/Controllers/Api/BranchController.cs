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
using Asp.NetCore_Inventory_Order_Management_System.Services;
using Asp.NetCore_Inventory_Order_Management_System.Models.SyncfusionViewModels;

namespace Asp.NetCore_Inventory_Order_Management_System.Controllers.Api
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/Branch")]
    public class BranchController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BranchController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Branch
        [HttpGet]
        public async Task<IActionResult> GetBranch()
        {
            List<Branch> Items = await _context.Branch.ToListAsync();
            int Count = Items.Count();
            return Ok(new { Items, Count });
        }

        // POST: api/Branch
        [HttpPost("[action]")]
        public IActionResult Insert([FromBody]CrudViewModel<Branch> payload)
        {
            Branch branch = payload.value;
            _context.Branch.Add(branch);
            _context.SaveChanges();
            return Ok(branch);
        }

        // POST: api/Branch
        [HttpPost("[action]")]
        public IActionResult Update([FromBody]CrudViewModel<Branch> payload)
        {
            Branch branch = payload.value;
            _context.Branch.Update(branch);
            _context.SaveChanges();
            return Ok(branch);
        }

        // POST: api/Branch
        [HttpPost("[action]")]
        public IActionResult Remove([FromBody]CrudViewModel<Branch> payload)
        {
            Branch branch = _context.Branch
                .Where(x => x.BranchId == (int)payload.key)
                .FirstOrDefault();
            _context.Branch.Remove(branch);
            _context.SaveChanges();
            return Ok(branch);
        }
    }
}

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
    [Route("api/Bill")]
    public class BillController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly INumberSequence _numberSequence;

        public BillController(ApplicationDbContext context,
                        INumberSequence numberSequence)
        {
            _context = context;
            _numberSequence = numberSequence;
        }

        // GET: api/Bill
        [HttpGet]
        public async Task<IActionResult> GetBill()
        {
            List<Bill> Items = await _context.Bill.ToListAsync();
            int Count = Items.Count();
            return Ok(new { Items, Count });
        }

        // GET: api/Bill
        [HttpGet("[action]")]
        public async Task<IActionResult> GetNotPaidYet()
        {
            List<Bill> bills = new List<Bill>();
            try
            {
                List<PaymentVoucher> vouchers = new List<PaymentVoucher>();
                vouchers = await _context.PaymentVoucher.ToListAsync();
                List<int> ids = new List<int>();

                foreach (var item in vouchers)
                {
                    ids.Add(item.BillId);
                }

                bills = await _context.Bill
                    .Where(x => !ids.Contains(x.BillId))
                    .ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
            return Ok(bills);
        }

        // POST: api/Bill
        [HttpPost("[action]")]
        public IActionResult Insert([FromBody]CrudViewModel<Bill> payload)
        {
            Bill bill = payload.value;
            bill.BillName = _numberSequence.GetNumberSequence("BILL");
            _context.Bill.Add(bill);
            _context.SaveChanges();
            return Ok(bill);
        }

        // POST: api/Bill
        [HttpPost("[action]")]
        public IActionResult Update([FromBody]CrudViewModel<Bill> payload)
        {
            Bill bill = payload.value;
            _context.Bill.Update(bill);
            _context.SaveChanges();
            return Ok(bill);
        }

        // POST: api/Bill
        [HttpPost("[action]")]
        public IActionResult Remove([FromBody]CrudViewModel<Bill> payload)
        {
            Bill bill = _context.Bill
                .Where(x => x.BillId == (int)payload.key)
                .FirstOrDefault();
            _context.Bill.Remove(bill);
            _context.SaveChanges();
            return Ok(bill);
        }
    }
}

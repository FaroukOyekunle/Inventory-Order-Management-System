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
    [Route("api/Invoice")]
    public class InvoiceController : Controller
    {
        private readonly ApplicationDbContext _applicationContext;
        private readonly INumberSequence _numberSequence;

        public InvoiceController(ApplicationDbContext applicationContext,
                        INumberSequence numberSequence)
        {
            _applicationContext = applicationContext;
            _numberSequence = numberSequence;
        }

        // GET: api/Invoice
        [HttpGet]
        public async Task<IActionResult> GetInvoice()
        {
            List<Invoice> Items = await _applicationContext.Invoice.ToListAsync();
            int Count = Items.Count();
            return Ok(new { Items, Count });
        }

        // GET: api/Invoice
        [HttpGet("[action]")]
        public async Task<IActionResult> GetNotPaidYet()
        {
            List<Invoice> invoices = new List<Invoice>();
            try
            {
                List<PaymentReceive> receives = new List<PaymentReceive>();
                receives = await _applicationContext.PaymentReceive.ToListAsync();
                List<int> ids = new List<int>();

                foreach (var item in receives)
                {
                    ids.Add(item.InvoiceId);
                }

                invoices = await _applicationContext.Invoice
                    .Where(x => !ids.Contains(x.InvoiceId))
                    .ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
            return Ok(invoices);
        }

        // POST: api/Invoice
        [HttpPost("[action]")]
        public IActionResult Insert([FromBody]CrudViewModel<Invoice> payload)
        {
            Invoice invoice = payload.value;
            invoice.InvoiceName = _numberSequence.GetNumberSequence("INV");
            _applicationContext.Invoice.Add(invoice);
            _applicationContext.SaveChanges();
            return Ok(invoice);
        }

        // POST: api/Invoice
        [HttpPost("[action]")]
        public IActionResult Update([FromBody]CrudViewModel<Invoice> payload)
        {
            Invoice invoice = payload.value;
            _applicationContext.Invoice.Update(invoice);
            _applicationContext.SaveChanges();
            return Ok(invoice);
        }

        // POST: api/Invoice
        [HttpPost("[action]")]
        public IActionResult Remove([FromBody]CrudViewModel<Invoice> payload)
        {
            Invoice invoice = _applicationContext.Invoice
                .Where(x => x.InvoiceId == (int)payload.key)
                .FirstOrDefault();
            _applicationContext.Invoice.Remove(invoice);
            _applicationContext.SaveChanges();
            return Ok(invoice);
        }
    }
}

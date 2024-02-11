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
    [Route("api/InvoiceType")]
    public class InvoiceTypeController : Controller
    {
        private readonly ApplicationDbContext _applicationContext;

        public InvoiceTypeController(ApplicationDbContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        // GET: api/InvoiceType
        [HttpGet]
        public async Task<IActionResult> GetInvoiceType()
        {
            List<InvoiceType> Items = await _applicationContext.InvoiceType.ToListAsync();
            int Count = Items.Count();
            return Ok(new { Items, Count });
        }

        // POST: api/InvoiceType
        [HttpPost("[action]")]
        public IActionResult Insert([FromBody]CrudViewModel<InvoiceType> payload)
        {
            InvoiceType invoiceType = payload.value;
            _applicationContext.InvoiceType.Add(invoiceType);
            _applicationContext.SaveChanges();
            return Ok(invoiceType);
        }

        // PUT: api/InvoiceType
        [HttpPost("[action]")]
        public IActionResult Update([FromBody]CrudViewModel<InvoiceType> payload)
        {
            InvoiceType invoiceType = payload.value;
            _applicationContext.InvoiceType.Update(invoiceType);
            _applicationContext.SaveChanges();
            return Ok(invoiceType);
        }

        [HttpPost("[action]")]
        public IActionResult Remove([FromBody]CrudViewModel<InvoiceType> payload)
        {
            InvoiceType invoiceType = _applicationContext.InvoiceType
                .Where(x => x.InvoiceTypeId == (int)payload.key)
                .FirstOrDefault();
            _applicationContext.InvoiceType.Remove(invoiceType);
            _applicationContext.SaveChanges();
            return Ok(invoiceType);
        }
    }
}

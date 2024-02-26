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
    [Route("api/PaymentVoucher")]
    public class PaymentVoucherController : Controller
    {
        private readonly ApplicationDbContext _applicationContext;
        private readonly INumberSequence _numberSequence;

        public PaymentVoucherController(ApplicationDbContext applicationContext,
                        INumberSequence numberSequence)
        {
            _applicationContext = applicationContext;
            _numberSequence = numberSequence;
        }

        // GET: api/PaymentVoucher
        [HttpGet]
        public async Task<IActionResult> GetPaymentVoucher()
        {
            List<PaymentVoucher> Items = await _applicationContext.PaymentVoucher.ToListAsync();
            int Count = Items.Count();
            return Ok(new { Items, Count });
        }


        //POST: api/PaymentVoucher
        [HttpPost("[action]")]
        public IActionResult Insert([FromBody]CrudViewModel<PaymentVoucher> payload)
        {
            PaymentVoucher paymentVoucher = payload.value;
            paymentVoucher.PaymentVoucherName = _numberSequence.GetNumberSequence("PAYVCH");
            _applicationContext.PaymentVoucher.Add(paymentVoucher);
            _applicationContext.SaveChanges();
            return Ok(paymentVoucher);
        }

        //POST: api/PaymentVoucher
        [HttpPost("[action]")]
        public IActionResult Update([FromBody]CrudViewModel<PaymentVoucher> payload)
        {
            PaymentVoucher paymentVoucher = payload.value;
            _applicationContext.PaymentVoucher.Update(paymentVoucher);
            _applicationContext.SaveChanges();
            return Ok(paymentVoucher);
        }

        [HttpPost("[action]")]
        public IActionResult Remove([FromBody]CrudViewModel<PaymentVoucher> payload)
        {
            PaymentVoucher paymentVoucher = _applicationContext.PaymentVoucher
                .Where(x => x.PaymentvoucherId == (int)payload.key)
                .FirstOrDefault();
            _applicationContext.PaymentVoucher.Remove(paymentVoucher);
            _applicationContext.SaveChanges();
            return Ok(paymentVoucher);
        }
    }
}

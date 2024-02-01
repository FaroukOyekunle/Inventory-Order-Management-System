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
    [Route("api/GoodsReceivedNote")]
    public class GoodsReceivedNoteController : Controller
    {
        private readonly ApplicationDbContext _applicationContext;
        private readonly INumberSequence _numberSequence;

        public GoodsReceivedNoteController(ApplicationDbContext applicationContext,
                        INumberSequence numberSequence)
        {
            _applicationContext = applicationContext;
            _numberSequence = numberSequence;
        }

        // GET: api/GoodsReceivedNote
        [HttpGet]
        public async Task<IActionResult> GetGoodsReceivedNote()
        {
            List<GoodsReceivedNote> Items = await _applicationContext.GoodsReceivedNote.ToListAsync();
            int Count = Items.Count();
            return Ok(new { Items, Count });
        }

        // GET: api/GoodsReceivedNote
        [HttpGet("[action]")]
        public async Task<IActionResult> GetNotBilledYet()
        {
            List<GoodsReceivedNote> goodsReceivedNotes = new List<GoodsReceivedNote>();
            try
            {
                List<Bill> bills = new List<Bill>();
                bills = await _applicationContext.Bill.ToListAsync();
                List<int> ids = new List<int>();

                foreach (var item in bills)
                {
                    ids.Add(item.GoodsReceivedNoteId);
                }

                goodsReceivedNotes = await _applicationContext.GoodsReceivedNote
                    .Where(x => !ids.Contains(x.GoodsReceivedNoteId))
                    .ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
            return Ok(goodsReceivedNotes);
        }

        // POST: api/GoodsReceivedNote
        [HttpPost("[action]")]
        public IActionResult Insert([FromBody]CrudViewModel<GoodsReceivedNote> payload)
        {
            GoodsReceivedNote goodsReceivedNote = payload.value;
            goodsReceivedNote.GoodsReceivedNoteName = _numberSequence.GetNumberSequence("GRN");
            _applicationContext.GoodsReceivedNote.Add(goodsReceivedNote);
            _applicationContext.SaveChanges();
            return Ok(goodsReceivedNote);
        }

        // POST: api/GoodsReceivedNote
        [HttpPost("[action]")]
        public IActionResult Update([FromBody]CrudViewModel<GoodsReceivedNote> payload)
        {
            GoodsReceivedNote goodsReceivedNote = payload.value;
            _applicationContext.GoodsReceivedNote.Update(goodsReceivedNote);
            _applicationContext.SaveChanges();
            return Ok(goodsReceivedNote);
        }

        // POST: api/GoodsReceivedNote
        [HttpPost("[action]")]
        public IActionResult Remove([FromBody]CrudViewModel<GoodsReceivedNote> payload)
        {
            GoodsReceivedNote goodsReceivedNote = _applicationContext.GoodsReceivedNote
                .Where(x => x.GoodsReceivedNoteId == (int)payload.key)
                .FirstOrDefault();
            _applicationContext.GoodsReceivedNote.Remove(goodsReceivedNote);
            _applicationContext.SaveChanges();
            return Ok(goodsReceivedNote);
        }
    }
}

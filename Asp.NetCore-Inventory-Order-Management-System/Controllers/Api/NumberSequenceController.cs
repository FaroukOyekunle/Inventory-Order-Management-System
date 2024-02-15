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

namespace Asp.NetCore_Inventory_Order_Management_System.Controllers.Api
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/NumberSequence")]
    public class NumberSequenceController : Controller
    {
        private readonly ApplicationDbContext _applicationContext;

        public NumberSequenceController(ApplicationDbContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        // GET: api/NumberSequence
        [HttpGet]
        public async Task<IActionResult> GetNumberSequence()
        {
            List<NumberSequence> Items = await _applicationContext.NumberSequence.ToListAsync();
            int Count = Items.Count();
            return Ok(new { Items, Count });
        }

        // PUT: api/NumberSequence/5
        [HttpPut]
        public async Task<IActionResult> PutNumberSequence([FromBody] NumberSequence numberSequence)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _applicationContext.Entry(numberSequence).State = EntityState.Modified;

            await _applicationContext.SaveChangesAsync();

            return Ok();
        }

        // POST: api/NumberSequence
        [HttpPost]
        public async Task<IActionResult> PostNumberSequence([FromBody] NumberSequence numberSequence)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _applicationContext.NumberSequence.Add(numberSequence);
            await _applicationContext.SaveChangesAsync();

            return Ok(numberSequence);
        }

        // DELETE: api/NumberSequence/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNumberSequence([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var numberSequence = await _applicationContext.NumberSequence.SingleOrDefaultAsync(m => m.NumberSequenceId == id);
            if (numberSequence is null)
            {
                return NotFound();
            }

            _applicationContext.NumberSequence.Remove(numberSequence);
            await _applicationContext.SaveChangesAsync();

            return Ok(numberSequence);
        }

        private bool NumberSequenceExists(int id)
        {
            return _applicationContext.NumberSequence.Any(e => e.NumberSequenceId == id);
        }
    }
}

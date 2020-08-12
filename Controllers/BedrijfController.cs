using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using cbstest.Models;

namespace cbstest.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BedrijfController : ControllerBase
    {
        public CBSDBContext _context;

        public BedrijfController(CBSDBContext context)
        {
            _context = context;
        }

        // GET: api/Bedrijf
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bedrijf>>> GetBedrijf()
        {
            return await _context.Bedrijf.ToListAsync();
        }

        // GET: api/Bedrijf/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bedrijf>> GetBedrijf(decimal id)
        {
            var bedrijf = await _context.Bedrijf.FindAsync(id);

            if (bedrijf == null)
            {
                return NotFound();
            }

            return bedrijf;
        }

        // PUT: api/Bedrijf/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBedrijf(decimal id, Bedrijf bedrijf)
        {
            if (id != bedrijf.BeId)
            {
                return BadRequest();
            }

            _context.Entry(bedrijf).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BedrijfExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Bedrijf
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Bedrijf>> PostBedrijf(Bedrijf bedrijf)
        {
            _context.Bedrijf.Add(bedrijf);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BedrijfExists(bedrijf.BeId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetBedrijf", new { id = bedrijf.BeId }, bedrijf);
        }

        // DELETE: api/Bedrijf/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Bedrijf>> DeleteBedrijf(decimal id)
        {
            var bedrijf = await _context.Bedrijf.FindAsync(id);
            if (bedrijf == null)
            {
                return NotFound();
            }

            _context.Bedrijf.Remove(bedrijf);
            await _context.SaveChangesAsync();

            return bedrijf;
        }

        private bool BedrijfExists(decimal id)
        {
            return _context.Bedrijf.Any(e => e.BeId == id);
        }
    }
}

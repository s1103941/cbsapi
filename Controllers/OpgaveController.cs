using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using cbstest.Models;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.AspNetCore.Cors;

namespace cbstest.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OpgaveController : ControllerBase
    {
        private readonly CBSDBContext _context;

        public OpgaveController(CBSDBContext context)
        {
            _context = context;
        }

        // GET: api/Opgave/5
        [HttpGet("/api/gemiddelde-omzet/{periodeID}")]
        public async Task<ActionResult<GemiddeldeOmzet>> GetOpgave(decimal periodeID)
        {

            var opgave = await _context.Opgave.Where(s => s.PeriodeId == periodeID).ToListAsync();

            decimal gemiddeldeOmzet = 0;
            foreach(Opgave o in opgave)
            {
                gemiddeldeOmzet += o.OmzetInclusiefbtw - o.Btw;
            }


            if (opgave == null)
            {
                return NotFound();
            }

            return new GemiddeldeOmzet(periodeID,gemiddeldeOmzet);
        }

        // PUT: api/Opgave/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOpgave(decimal id, Opgave opgave)
        {
            if (id != opgave.OpgaveId)
            {
                return BadRequest();
            }

            _context.Entry(opgave).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OpgaveExists(id))
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

        // POST: api/Opgave
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Opgave>> PostOpgave(Opgave opgave)
        {
            _context.Opgave.Add(opgave);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (OpgaveExists(opgave.OpgaveId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetOpgave", new { id = opgave.OpgaveId }, opgave);
        }

        // DELETE: api/Opgave/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Opgave>> DeleteOpgave(decimal id)
        {
            var opgave = await _context.Opgave.FindAsync(id);
            if (opgave == null)
            {
                return NotFound();
            }

            _context.Opgave.Remove(opgave);
            await _context.SaveChangesAsync();

            return opgave;
        }

        private bool OpgaveExists(decimal id)
        {
            return _context.Opgave.Any(e => e.OpgaveId == id);
        }
    }
}

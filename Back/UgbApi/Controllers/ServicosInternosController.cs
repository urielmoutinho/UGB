using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UgbApi.Data;
using UgbApi.Models;

namespace UgbApi.Controllers
{
    [Route("/ServicosInternos")]
    [ApiController]
    public class ServicoInternoController : ControllerBase
    {
        private readonly UgbDbContext _context;

        public ServicoInternoController(UgbDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServicoInternoModel>>> Get()
        {
            return await _context.ServicosInternos.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ServicoInternoModel>> Getid(int id)
        {
            var result = ServicoInternoExists(id);
            var servicoInterno = await _context.ServicosInternos.FindAsync(id);

            if (servicoInterno == null)
            {
                return NotFound();
            }

            return servicoInterno;
        }

        [HttpPost]
        public async Task<ActionResult<ServicoInternoModel>> Post(ServicoInternoModel servicoInterno)
        {
            _context.ServicosInternos.Add(servicoInterno);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = servicoInterno.ServicoInternoId }, servicoInterno);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ServicoInternoModel servicoInterno)
        {
            _context.Entry(servicoInterno).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServicoInternoExists(id))
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
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var servicoInterno = await _context.ServicosInternos.FindAsync(id);
            if (servicoInterno == null)
            {
                return NotFound();
            }

            _context.ServicosInternos.Remove(servicoInterno);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ServicoInternoExists(int id)
        {
            return _context.ServicosInternos.Any(e => e.ServicoInternoId == id);
        }
    }
}
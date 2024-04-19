using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UgbApi.Data;
using UgbApi.Models;

namespace UgbApi.Controllers
{
    [Route("/PedidosInternos")]
    [ApiController]
    public class PedidosInternosController : ControllerBase
    {
        private readonly UgbDbContext _context;

        public PedidosInternosController(UgbDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PedidoInternoModel>>> Get()
        {
            return await _context.PedidosInternos.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<PedidoInternoModel>> Get(int id)
        {
            var pedidoInterno = await _context.PedidosInternos.FindAsync(id);

            if (pedidoInterno == null)
            {
                return NotFound();
            }

            return pedidoInterno;
        }
        [HttpPost]
        public async Task<ActionResult<PedidoInternoModel>> Post(int usuarioId, int produtoId, PedidoInternoModel pedidoInterno)
        {
            pedidoInterno.DataCadastro = DateTime.Now;

            _context.PedidosInternos.Add(pedidoInterno);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = pedidoInterno.PedidoInternoId }, pedidoInterno);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, int usuarioId, int produtoId, PedidoInternoModel pedidoInterno)
        {
            if (id != pedidoInterno.PedidoInternoId)
            {
                return BadRequest();
            }

            _context.Entry(pedidoInterno).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PedidoInternoExists(id))
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
            var pedidoInterno = await _context.PedidosInternos.FindAsync(id);
            if (pedidoInterno == null)
            {
                return NotFound();
            }

            _context.PedidosInternos.Remove(pedidoInterno);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PedidoInternoExists(int id)
        {
            return _context.PedidosInternos.Any(e => e.PedidoInternoId == id);
        }
    }
}
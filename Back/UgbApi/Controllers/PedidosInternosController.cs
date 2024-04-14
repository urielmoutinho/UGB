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
    [Route("api/PedidosInternos")]
    [ApiController]
    public class PedidosInternosController : ControllerBase
    {
        private readonly UgbDbContext _context;

        public PedidosInternosController(UgbDbContext context)
        {
            _context = context;
        }

        // GET: api/PedidosInternos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PedidoInternoModel>>> GetPedidosInternos()
        {
            return await _context.PedidosInternos.ToListAsync();
        }

        // GET: api/PedidosInternos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PedidoInternoModel>> GetPedidoInterno(int id)
        {
            var pedidoInterno = await _context.PedidosInternos.FindAsync(id);

            if (pedidoInterno == null)
            {
                return NotFound();
            }

            return pedidoInterno;
        }

        // POST: api/PedidosInternos
        [HttpPost]
        public async Task<ActionResult<PedidoInternoModel>> PostPedidoInterno(PedidoInternoModel pedidoInterno)
        {
            pedidoInterno.DataEntrega = DateTime.Now; // Definindo a data de cadastro como a data atual
            _context.PedidosInternos.Add(pedidoInterno);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPedidoInterno), new { id = pedidoInterno.PedidoInternoId }, pedidoInterno);
        }
    }
}
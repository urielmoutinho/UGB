using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using UgbApi.Data;
using UgbApi.Models;

namespace UgbApi.Controllers
{
    [Route("api/EntradaSaidaEstoque")]
    [ApiController]
    public class EntradaSaidaEstoqueController : ControllerBase
    {
        private readonly UgbDbContext _context;

        public EntradaSaidaEstoqueController(UgbDbContext context)
        {
            _context = context;
        }

        // POST: api/EntradaSaidaEstoque/Entrada
        [HttpPost("Entrada")]
        public async Task<ActionResult> RegistrarEntradaEstoque(EntradaEstoqueModel entradaEstoque)
        {
            entradaEstoque.DataCadastro = DateTime.Now; // Definindo a data de cadastro como a data atual
            _context.EntradasEstoque.Add(entradaEstoque);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // POST: api/EntradaSaidaEstoque/Saida
        [HttpPost("Saida")]
        public async Task<ActionResult> RegistrarSaidaEstoque(SaidaEstoqueModel saidaEstoque)
        {
            // Verifica se há estoque suficiente para realizar a saída
            var produto = await _context.Produtos.FirstOrDefaultAsync(p => p.ProdutoId == saidaEstoque.ProdutoId);
            if (produto == null || produto.QuantidadeEstoque < saidaEstoque.Quantidade)
            {
                return BadRequest("Estoque insuficiente para realizar a saída.");
            }

            saidaEstoque.DataCadastro = DateTime.Now; // Definindo a data de cadastro como a data atual
            _context.SaidasEstoque.Add(saidaEstoque);

            // Atualiza a quantidade de estoque do produto
            produto.QuantidadeEstoque -= saidaEstoque.Quantidade;

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
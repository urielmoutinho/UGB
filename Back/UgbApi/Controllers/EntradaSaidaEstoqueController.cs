using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using UgbApi.Data;
using UgbApi.Models;

namespace UgbApi.Controllers
{
    [Route("EntradaSaidaEstoque")]
    [ApiController]
    public class EntradaSaidaEstoqueController : ControllerBase
    {
        private readonly UgbDbContext _context;

        public EntradaSaidaEstoqueController(UgbDbContext context)
        {
            _context = context;
        }
        [HttpPost("Entrada")]
        public async Task<ActionResult> RegistrarEntradaEstoque(EntradaEstoqueModel entradaEstoque)
        {
            entradaEstoque.DataCadastro = DateTime.Now; 
            _context.EntradasEstoque.Add(entradaEstoque);
            await _context.SaveChangesAsync();

            return Ok();
        }
        [HttpPost("Saida")]
        public async Task<ActionResult> RegistrarSaidaEstoque(SaidaEstoqueModel saidaEstoque)
        {
            try
            {
                // Verifica se há estoque suficiente para realizar a saída
                var quantidadeEmEstoque = await CalcularQuantidadeEmEstoque(saidaEstoque.ProdutoId);
                if (quantidadeEmEstoque < saidaEstoque.Quantidade)
                {
                    return BadRequest("Estoque insuficiente para realizar a saída.");
                }

                // Se houver estoque suficiente, prosseguir com o registro da saída
                saidaEstoque.DataCadastro = DateTime.Now; 
                _context.SaidasEstoque.Add(saidaEstoque);

                await _context.SaveChangesAsync();

                return Ok(); 
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao registrar saída de estoque: {ex.Message}");
            }
        }
        // Método para calcular a quantidade em estoque de um produto
        private async Task<int> CalcularQuantidadeEmEstoque(int produtoId)
        {
            var entradas = await _context.EntradasEstoque.Where(e => e.ProdutoId == produtoId).ToListAsync();
            var saidas = await _context.SaidasEstoque.Where(s => s.ProdutoId == produtoId).ToListAsync();

            var totalEntradas = entradas.Sum(e => e.Quantidade);
            var totalSaidas = saidas.Sum(s => s.Quantidade);

            return totalEntradas - totalSaidas;
        }
    }
}
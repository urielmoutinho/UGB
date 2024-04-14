using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UgbApi.Models
{
    [Table("EntradasEstoque")]
    public class EntradaEstoqueModel
    {
        [Key]
        public int EntradaEstoqueId { get; set; }

        public int ProdutoId { get; set; }
        public ProdutoModel Produto { get; set; }

        public int Quantidade { get; set; }

        public int FornecedorId { get; set; }
        public FornecedorModel Fornecedor { get; set; }

        public string NumeroNotaFiscal { get; set; }

        public string DepositoArmazenamento { get; set; }

        public DateTime DataCadastro { get; set; }
    }
}
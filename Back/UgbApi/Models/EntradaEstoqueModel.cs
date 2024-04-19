using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UgbApi.Models
{
    [Table("EntradasEstoque")]
    public class EntradaEstoqueModel
    {
        [Key] // Define a propriedade como chave primária
        public int EntradaEstoqueId { get; set; }
        public int ProdutoId { get; set; } // Referência ao produto associado
        public int Quantidade { get; set; }
        public int FornecedorId { get; set; } // Referência ao fornecedor associado
        public string NumeroNotaFiscal { get; set; }
        public string DepositoArmazenamento { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
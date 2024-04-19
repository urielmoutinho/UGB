using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UgbApi.Models
{
    [Table("SaidasEstoque")]
    public class SaidaEstoqueModel
    {
        [Key] // Define a propriedade como chave primária
        public int SaidaEstoqueId { get; set; }
        public int ProdutoId { get; set; } // Referência ao produto associado
        public int Quantidade { get; set; }
        public int UsuarioId { get; set; } // Referência ao usuario associado
        public DateTime DataCadastro { get; set; }
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UgbApi.Models
{
    [Table("SaidasEstoque")]
    public class SaidaEstoqueModel
    {
        [Key]
        public int SaidaEstoqueId { get; set; }

        public int ProdutoId { get; set; }
        public ProdutoModel Produto { get; set; }

        public int Quantidade { get; set; }

        public int UsuarioId { get; set; }
        public UsuarioModel Usuario { get; set; }

        public string Departamento { get; set; }

        public DateTime DataCadastro { get; set; }
    }
}
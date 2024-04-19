using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UgbApi.Models
{
    [Table("Produtos")]
    public class ProdutoModel
    {
        [Key]// Define a propriedade como chave primária
        public int ProdutoId { get; set; }
        public string Nome { get; set; }
        public string EAN { get; set; }
        public decimal PrecoUnitario { get; set; }
        public int CotaMinima { get; set; }
    }
}
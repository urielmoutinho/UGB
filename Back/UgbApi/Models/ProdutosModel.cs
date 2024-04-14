using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UgbApi.Models
{
    [Table("Produtos")]
    public class ProdutoModel
    {
        [Key]
        public int ProdutoId { get; set; }

        public string Nome { get; set; }
        public string EAN { get; set; }
        public decimal PrecoUnitario { get; set; }
        public int CotaMinima { get; set; }
        public int QuantidadeEstoque { get; set; }
    }
}
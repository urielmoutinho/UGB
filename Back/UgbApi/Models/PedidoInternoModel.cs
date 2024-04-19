using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UgbApi.Models
{
    [Table("PedidosInternos")]
    public class PedidoInternoModel
    {
        [Key] // Define a propriedade como chave primária
        public int PedidoInternoId { get; set; }
        public int ProdutoId { get; set; } // Referência ao produto associado
        public int UsuarioId { get; set; } // Referência ao usuario associado
        public DateTime DataCadastro { get; set; }
        public string Fabricante { get; set; }
        public int Quantidade { get; set;}
        public string Observacao { get; set; }

    }
}
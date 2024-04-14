using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UgbApi.Models
{
    [Table("PedidosInternos")]
    public class PedidoInternoModel
    {
        [Key]
        public int PedidoInternoId { get; set; }

        public int ProdutoId { get; set; } // ID do produto associado ao pedido
        public ProdutoModel Produto { get; set; } // Referência ao produto associado ao pedido

        public int FornecedorId { get; set; } // ID do fornecedor associado ao pedido
        public FornecedorModel Fornecedor { get; set; } // Referência ao fornecedor associado ao pedido

        public int Quantidade { get; set; } // Quantidade do produto solicitada
        public string Descricao { get; set; } // Descrição do produto ou serviço
        public decimal ValorUnitario { get; set; } // Valor unitário do produto ou serviço
        public DateTime DataEntrega { get; set; } // Data de entrega solicitada
        public decimal PrecoTotal { get; set; } // Preço total do pedido
        public string CodigoEAN { get; set; } // Código EAN do produto associado ao pedido
    }
}
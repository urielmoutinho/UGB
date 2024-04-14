using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UgbApi.Models
{
    // Atributo [Table] especifica o nome da tabela associada a esta classe no banco de dados
    [Table("Fornecedores")]
    public class FornecedorModel
    {
        public FornecedorModel()
        {
            // Inicialização da coleção de pedidos internos para evitar exceções de referência nula
            PedidosInternos = new Collection<PedidoInternoModel>();
        }

        [Key] // Define a propriedade como chave primária
        public int FornecedorId { get; set; } // Propriedade para o ID do fornecedor

        public string Nome { get; set; } // Propriedade para o nome do fornecedor
        public string Endereco { get; set; } // Propriedade para o endereço do fornecedor
        public string Email { get; set; } // Propriedade para o email do fornecedor
        public string CNPJ { get; set; } // Propriedade para o CNPJ do fornecedor
        public string InscricaoEstadual { get; set; } // Propriedade para a inscrição estadual do fornecedor
        public string InscricaoMunicipal { get; set; } // Propriedade para a inscrição municipal do fornecedor

        // Define uma coleção de pedidos internos associados a este fornecedor
        public ICollection<PedidoInternoModel> PedidosInternos { get; set; }
    }
}
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UgbApi.Models
{
    [Table("Servicos")]
    public class ServicoModel
    {
        [Key] // Define a propriedade como chave primária
        public int ServicoId { get; set; }
        public int FornecedorId { get; set; } // Referência ao fornecedor associado
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime PrazoEntrega { get; set; }
    }
}
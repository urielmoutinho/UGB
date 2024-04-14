using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UgbApi.Models
{
    [Table("Servicos")]
    public class ServicoModel
    {
        [Key]
        public int ServicoId { get; set; }

        public string Nome { get; set; }
        public int FornecedorId { get; set; }
        public FornecedorModel Fornecedor { get; set; }
        public string Descricao { get; set; }
        public int PrazoEntrega { get; set; }
    }
}
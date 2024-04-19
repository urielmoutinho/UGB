using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UgbApi.Models
{
    [Table("ServicoInternos")]
    public class ServicoInternoModel
    {
        [Key]// Define a propriedade como chave primária
        public int ServicoInternoId { get; set; }
        public int UsuarioId { get; set; }// Referência ao usuario associado 
        public int ServicoId { get; set; } // Referência ao serviço associado e ao fornecedor do servico
        public DateTime DataCadastro { get; set; } 
        public string Descricao { get; set; } 

    }
}
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UgbApi.Models
{
    [Table("Fornecedores")]
    public class FornecedorModel
    {

        [Key] // Define a propriedade como chave primária
        public int FornecedorId { get; set; } 
        public string Nome { get; set; } 
        public string Endereco { get; set; } 
        public string Email { get; set; } 
        public string CNPJ { get; set; } 
        public string InscricaoEstadual { get; set; } 
        public string InscricaoMunicipal { get; set; } 
    }
}
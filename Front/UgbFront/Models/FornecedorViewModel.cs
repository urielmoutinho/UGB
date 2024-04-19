using System.ComponentModel.DataAnnotations;

namespace UgbFront.Models
{
    public class FornecedorModel
    {
        public int FornecedorId { get; set; }
        [Required(ErrorMessage = "O nome do fornecedor é obrigatório.")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O endereço do fornecedor é obrigatório.")]
        public string Endereco { get; set; }
        [EmailAddress(ErrorMessage = "Por favor, insira um email válido.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "O CNPJ do fornecedor é obrigatório.")]
        [RegularExpression(@"^\d{2}\.\d{3}\.\d{3}/\d{4}-\d{2}$", ErrorMessage = "Por favor, insira um CNPJ válido.")]
        public string CNPJ { get; set; }
        [Display(Name = "Inscrição Estadual")]
        public string InscricaoEstadual { get; set; }
        [Display(Name = "Inscrição Municipal")]
        public string InscricaoMunicipal { get; set; }
    }
}
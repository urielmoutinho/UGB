using System.ComponentModel.DataAnnotations;

namespace UgbFront.Models
{
    public class ServicoModel
    {
        public int ServicoId { get; set; }
        [Required(ErrorMessage = "O nome do serviço é obrigatório.")]
        public string Nome { get; set; }
        public int FornecedorId { get; set; }
        public string Descricao { get; set; }
        [Display(Name = "Prazo de Entrega")]
        public DateTime PrazoEntrega { get; set; }
    }
}
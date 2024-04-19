using System.ComponentModel.DataAnnotations;

namespace UgbFront.Models
{
    public class ProdutoModel
    {
        public int ProdutoId { get; set; }
        [Required(ErrorMessage = "O nome do produto é obrigatório.")]
        public string Nome { get; set; }
        [Display(Name = "Código EAN")]
        [Required(ErrorMessage = "O código EAN é obrigatório.")]
        public string EAN { get; set; }
        [Display(Name = "Preço Unitário")]
        [Required(ErrorMessage = "O preço unitário é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O preço unitário deve ser maior que zero.")]
        public decimal PrecoUnitario { get; set; }
        [Display(Name = "Cota Mínima")]
        [Required(ErrorMessage = "A cota mínima é obrigatória.")]
        [Range(1, int.MaxValue, ErrorMessage = "A cota mínima deve ser maior que zero.")]
        public int CotaMinima { get; set; }
    }
}
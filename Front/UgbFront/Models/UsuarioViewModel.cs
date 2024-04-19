using System.ComponentModel.DataAnnotations;

namespace UgbFront.Models
{
    public class UsuarioModel
    {
        public int UsuarioId { get; set; }
        [Required(ErrorMessage = "A matrícula do usuário é obrigatória.")]
        public int Matricula { get; set; }
        [Required(ErrorMessage = "O nome do usuário é obrigatório.")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O departamento do usuário é obrigatório.")]
        public string Departamento { get; set; }
    }
}
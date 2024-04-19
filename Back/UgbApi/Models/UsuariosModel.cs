using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UgbApi.Models
{
    [Table("Usuarios")]
    public class UsuarioModel
    {
        [Key] // Define a propriedade como chave primária
        public int UsuarioId { get; set; }

        public int Matricula { get; set; }
        public string Nome { get; set; }
        public string Departamento { get; set; }
    }
}
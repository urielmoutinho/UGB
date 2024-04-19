using System;
using System.ComponentModel.DataAnnotations;

namespace UgbFront.Models
{
    public class ServicoInternoModel
    {
        public int ServicoInternoId { get; set; }
        public int ServicoId { get; set; }
        public int UsuarioId { get; set; }
        [Display(Name = "Data de Cadastro")]
        public DateTime DataCadastro { get; set; }
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
    }
}
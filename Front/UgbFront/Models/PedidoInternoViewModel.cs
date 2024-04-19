using System;
using System.ComponentModel.DataAnnotations;

namespace UgbFront.Models
{
    public class PedidoInternoModel
    {
        public int PedidoInternoId { get; set; }
        public int ProdutoId { get; set; }
        public int UsuarioId { get; set; }
        public string Fabricante { get; set; }
        public int Quantidade { get; set; }

        [Display(Name = "Data de Cadastro")]
        public DateTime DataCadastro { get; set; }

        [Display(Name = "Observação")]
        public string Observacao { get; set; }
    }
}
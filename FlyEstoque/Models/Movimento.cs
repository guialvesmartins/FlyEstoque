using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlyEstoque.Models
{
    public class Movimento
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Informe a Data do Movimento")]
        public DateTime DtMovimento { get; set; }
        public Produto Produto { get; set; }
        [Required(ErrorMessage = "Informe o Produto do Movimento")]
        public int ProdutoId { get; set; }
        [Required(ErrorMessage = "Informe o Tipo do Movimento")]
        public TipoMovimento TipoMovimento { get; set; }
        [Required(ErrorMessage = "Informe a Quantidade que será Movimentada")]
        public decimal Quantidade { get; set; }
    }
    public enum TipoMovimento 
    {
        [Display(Name = "Entrada")]
        E,
        [Display(Name = "Saída")]
        S,
        [Display(Name = "Inventário")]
        I
    }
}

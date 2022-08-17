using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlyEstoque.Models
{
    public class Produto
    {
        public int Id { get; set; }

        [StringLength(6, MinimumLength = 6)]
        public string Codigo { get; set; }
        [Required(ErrorMessage = "Informe a descrição do produto.")]
        [StringLength(40, MinimumLength = 1)]
        public string Descricao { get; set; }
        [StringLength(20, MinimumLength = 1)]
        public string CodBarras { get; set; }
        public decimal EstoqueMin { get; set; }
        public decimal EstoqueMax { get; set; }
        public decimal SaldoAtual { get; set; }
        public GrupoProduto GrupoProduto { get; set; }
        public int GrupoProdutoId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlyEstoque.Models
{
    public class GrupoProduto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe o Codigo do Grupo de Produto, o mesmo deve conter 2 Digitos.")]
        [StringLength(2, MinimumLength = 2)]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "Informe a Descrição do Grupo de Produto")]
        [StringLength(40, MinimumLength = 1)]
        public string Descricao { get; set; }
        public List<Produto> Produtos { get; set; }
    }
}

using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;


namespace devSteamAPI.Migrations
{
    public class ItemCarrinho
    {
        public Guid ItemCarrinhoId { get; set; }
        public Guid CarrinhoId { get; set; }
        public Guid JogoId { get; set; }
        [Required(ErrorMessage = "O campo é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser maior que zero")]
        public int Quantidade { get; set; }
        [Required(ErrorMessage = "O campo é obrigatório")]
        [Range(0.01, 9999.99, ErrorMessage = "O valor  deve ser maior que zero e menor que R$ 9.999,99")]
        public decimal Valor { get; set; }
        public decimal? Total { get; set; } // O valor total tem um ponto de ? pois ele vai ser calculado e não informado pelo usuário
    }
}

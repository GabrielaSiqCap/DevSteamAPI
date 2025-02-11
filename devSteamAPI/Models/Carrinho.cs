namespace devSteamAPI.Models
{
    public class Carrinho
    {
        public Guid CarrinhoId { get; set; }
        public Guid UsuarioId { get; set; }
        public DateTime? DataCriacao { get; set; }
        public decimal? Total { get; set; }
        public bool? Finalizado { get; set; } = false;
    }
}

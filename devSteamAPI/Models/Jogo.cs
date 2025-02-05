namespace devSteamAPI.Models
{
    public class Jogo
    {
        public Guid JogoId { get; set; }
        public string Nome { get; set; }
        public int Classificacao { get; set; }
        public string? Descricao { get; set; } //? é não o obrigatório
        public string? Imagem { get; set; } // salvamos o caminho da imagem e ai ela busca sozinho
        public Guid CategoriaId { get; set; }
        public Categoria? Categoria { get; set; }
    }
}

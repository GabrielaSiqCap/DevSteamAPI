using devSteamAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using devSteamAPI.Migrations;

namespace devSteamAPI.Data
{
    public class DevSteamAPIContext : IdentityDbContext
    {
        //Método construtor
        public DevSteamAPIContext(DbContextOptions<DevSteamAPIContext> options) : base(options)
        {
        }

        //Definir as tabelas do banco de dados// como o modelo vai ser referênciado no sistema
        public DbSet<Jogo> Jogos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }

        //Sobrescrever o método OnModelCreating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Jogo>().ToTable("Jogos");
            modelBuilder.Entity<Categoria>().ToTable("Categorias");
        }
        public DbSet<devSteamAPI.Models.Carrinho> Carrinho { get; set; } = default!;
        public DbSet<devSteamAPI.Migrations.ItemCarrinho> ItemCarrinho { get; set; } = default!;
    }
}

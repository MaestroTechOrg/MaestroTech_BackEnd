using MaestroTech.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace MaestroTech.Infrastructure.Data
{
    public class MaestroTechDbContext : IdentityDbContext<Usuario, IdentityRole<int>, int>
    {
        public MaestroTechDbContext(DbContextOptions<MaestroTechDbContext> options) : base(options)
        {
        }

        public DbSet<Igreja> Igrejas { get; set; }
        public DbSet<Musica> Musicas { get; set; }
        public DbSet<Culto> Cultos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; } // Adicionar esta linha

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configurações adicionais, se necessário
        }
    }
}

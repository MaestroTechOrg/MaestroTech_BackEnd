using MaestroTech.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MaestroTech.Infrastructure.Data
{
    public class MaestroTechDbContext : DbContext
    {
        public MaestroTechDbContext(DbContextOptions<MaestroTechDbContext> options) : base(options)
        {
        }

        public DbSet<Igreja> Igrejas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Musica> Musicas { get; set; }
        public DbSet<Culto> Cultos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // fiquem a vontade para adicionar configurações de entidades
        }
    }
}

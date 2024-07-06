using MaestroTech.Domain.Entities;
using MaestroTech.Domain.Repositories;
using MaestroTech.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MaestroTech.Infrastructure.Repositories
{
    public class MusicaRepository : IMusicaRepository
    {
        private readonly MaestroTechDbContext _context;

        public MusicaRepository(MaestroTechDbContext context)
        {
            _context = context;
        }

        public async Task<Musica?> GetByIdAsync(int id)
        {
            return await _context.Musicas.FindAsync(id);
        }

        public async Task<IEnumerable<Musica>> GetAllAsync()
        {
            return await _context.Musicas.ToListAsync();
        }

        public async Task AddAsync(Musica musica)
        {
            await _context.Musicas.AddAsync(musica);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Musica musica)
        {
            _context.Musicas.Update(musica);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var musica = await _context.Musicas.FindAsync(id);
            if (musica != null)
            {
                _context.Musicas.Remove(musica);
                await _context.SaveChangesAsync();
            }
        }
    }
}

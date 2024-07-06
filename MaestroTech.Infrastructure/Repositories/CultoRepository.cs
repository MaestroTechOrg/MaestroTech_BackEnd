using MaestroTech.Domain.Entities;
using MaestroTech.Domain.Repositories;
using MaestroTech.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MaestroTech.Infrastructure.Repositories
{
    public class CultoRepository : ICultoRepository
    {
        private readonly MaestroTechDbContext _context;

        public CultoRepository(MaestroTechDbContext context)
        {
            _context = context;
        }

        public async Task<Culto> GetByIdAsync(int id)
        {
            return await _context.Cultos.FindAsync(id);
        }

        public async Task<IEnumerable<Culto>> GetAllAsync()
        {
            return await _context.Cultos.ToListAsync();
        }

        public async Task AddAsync(Culto culto)
        {
            await _context.Cultos.AddAsync(culto);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Culto culto)
        {
            _context.Cultos.Update(culto);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var culto = await _context.Cultos.FindAsync(id);
            if (culto != null)
            {
                _context.Cultos.Remove(culto);
                await _context.SaveChangesAsync();
            }
        }
    }
}

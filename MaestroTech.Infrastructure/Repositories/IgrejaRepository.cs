using MaestroTech.Domain.Entities;
using MaestroTech.Domain.Repositories;
using MaestroTech.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MaestroTech.Infrastructure.Repositories
{
    public class IgrejaRepository : IIgrejaRepository
    {
        private readonly MaestroTechDbContext _context;

        public IgrejaRepository(MaestroTechDbContext context)
        {
            _context = context;
        }

        public async Task<Igreja> GetByIdAsync(int id)
        {
            return await _context.Igrejas.FindAsync(id);
        }

        public async Task<IEnumerable<Igreja>> GetAllAsync()
        {
            return await _context.Igrejas.ToListAsync();
        }

        public async Task AddAsync(Igreja igreja)
        {
            await _context.Igrejas.AddAsync(igreja);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Igreja igreja)
        {
            _context.Igrejas.Update(igreja);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var igreja = await _context.Igrejas.FindAsync(id);
            if (igreja != null)
            {
                _context.Igrejas.Remove(igreja);
                await _context.SaveChangesAsync();
            }
        }
    }
}

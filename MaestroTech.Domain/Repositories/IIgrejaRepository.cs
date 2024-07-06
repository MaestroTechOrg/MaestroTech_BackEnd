using System.Collections.Generic;
using System.Threading.Tasks;
using MaestroTech.Domain.Entities;

namespace MaestroTech.Domain.Repositories
{
    public interface IIgrejaRepository
    {
        Task<Igreja?> GetByIdAsync(int id); // Permitir retorno nulo
        Task<IEnumerable<Igreja>> GetAllAsync();
        Task AddAsync(Igreja igreja);
        Task UpdateAsync(Igreja igreja);
        Task DeleteAsync(int id);
    }
}


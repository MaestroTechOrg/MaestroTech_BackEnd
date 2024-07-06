namespace MaestroTech.Domain.Repositories
{
    public interface IIgrejaRepository
    {
        Task<Igreja> GetByIdAsync(int id);
        Task<IEnumerable<Igreja>> GetAllAsync();
        Task AddAsync(Igreja igreja);
        Task UpdateAsync(Igreja igreja);
        Task DeleteAsync(int id);
    }
}

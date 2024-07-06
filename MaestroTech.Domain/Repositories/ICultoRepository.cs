namespace MaestroTech.Domain.Repositories
{
    public interface ICultoRepository
    {
        Task<Culto> GetByIdAsync(int id);
        Task<IEnumerable<Culto>> GetAllAsync();
        Task AddAsync(Culto culto);
        Task UpdateAsync(Culto culto);
        Task DeleteAsync(int id);
    }
}

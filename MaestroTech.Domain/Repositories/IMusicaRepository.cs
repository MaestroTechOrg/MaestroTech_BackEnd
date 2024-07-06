namespace MaestroTech.Domain.Repositories
{
    public interface IMusicaRepository
    {
        Task<Musica> GetByIdAsync(int id);
        Task<IEnumerable<Musica>> GetAllAsync();
        Task AddAsync(Musica musica);
        Task UpdateAsync(Musica musica);
        Task DeleteAsync(int id);
    }
}

using MaestroTech.Domain.Repositories;

namespace MaestroTech.Application.UseCases.Cultos
{
    public class DeleteCultoCommand
    {
        private readonly ICultoRepository _cultoRepository;

        public DeleteCultoCommand(ICultoRepository cultoRepository)
        {
            _cultoRepository = cultoRepository;
        }

        public async Task Execute(int id)
        {
            await _cultoRepository.DeleteAsync(id);
        }
    }
}

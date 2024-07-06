using MaestroTech.Domain.Entities;
using MaestroTech.Domain.Repositories;

namespace MaestroTech.Application.UseCases.Cultos
{
    public class UpdateCultoCommand
    {
        private readonly ICultoRepository _cultoRepository;

        public UpdateCultoCommand(ICultoRepository cultoRepository)
        {
            _cultoRepository = cultoRepository;
        }

        public async Task Execute(Culto culto)
        {
            await _cultoRepository.UpdateAsync(culto);
        }
    }
}

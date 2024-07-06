using MaestroTech.Domain.Entities;
using MaestroTech.Domain.Repositories;

namespace MaestroTech.Application.UseCases.Cultos
{
    public class CreateCultoCommand
    {
        private readonly ICultoRepository _cultoRepository;

        public CreateCultoCommand(ICultoRepository cultoRepository)
        {
            _cultoRepository = cultoRepository;
        }

        public async Task Execute(Culto culto)
        {
            await _cultoRepository.AddAsync(culto);
        }
    }
}

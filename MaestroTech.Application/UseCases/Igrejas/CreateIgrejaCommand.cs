using MaestroTech.Domain.Entities;
using MaestroTech.Domain.Repositories;

namespace MaestroTech.Application.UseCases.Igrejas
{
    public class CreateIgrejaCommand
    {
        private readonly IIgrejaRepository _igrejaRepository;

        public CreateIgrejaCommand(IIgrejaRepository igrejaRepository)
        {
            _igrejaRepository = igrejaRepository;
        }

        public async Task Execute(Igreja igreja)
        {
            await _igrejaRepository.AddAsync(igreja);
        }
    }
}

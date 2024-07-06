using MaestroTech.Domain.Entities;
using MaestroTech.Domain.Repositories;

namespace MaestroTech.Application.UseCases.Igrejas
{
    public class UpdateIgrejaCommand
    {
        private readonly IIgrejaRepository _igrejaRepository;

        public UpdateIgrejaCommand(IIgrejaRepository igrejaRepository)
        {
            _igrejaRepository = igrejaRepository;
        }

        public async Task Execute(Igreja igreja)
        {
            await _igrejaRepository.UpdateAsync(igreja);
        }
    }
}

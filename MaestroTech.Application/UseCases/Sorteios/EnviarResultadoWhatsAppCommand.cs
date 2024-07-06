using MaestroTech.Domain.Repositories;
using MaestroTech.Application.Services;

namespace MaestroTech.Application.UseCases.Sorteios
{
    public class EnviarResultadoWhatsAppCommand
    {
        private readonly IWhatsAppService _whatsAppService;

        public EnviarResultadoWhatsAppCommand(IWhatsAppService whatsAppService)
        {
            _whatsAppService = whatsAppService;
        }

        public async Task Execute(string message)
        {
            await _whatsAppService.SendMessageAsync(message);
        }
    }
}

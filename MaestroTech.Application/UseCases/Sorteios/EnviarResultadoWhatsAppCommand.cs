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

        public async Task Execute(string to, string message)
        {
            if (string.IsNullOrWhiteSpace(to))
                throw new ArgumentException("Recipient cannot be null or whitespace.", nameof(to));

            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentException("Message cannot be null or whitespace.", nameof(message));

            await _whatsAppService.SendMessageAsync(to, message);
        }
    }
}

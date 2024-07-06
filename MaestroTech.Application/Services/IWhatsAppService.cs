namespace MaestroTech.Application.Services
{
    public interface IWhatsAppService
    {
        Task SendMessageAsync(string to, string message);
    }
}

using Microsoft.AspNetCore.Mvc;
using MaestroTech.Application.UseCases.Sorteios;
using Microsoft.AspNetCore.Authorization;

namespace MaestroTech.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class SorteiosController : ControllerBase
    {
        private readonly SorteioMusicaCommand _sorteioMusicaCommand;
        private readonly EnviarResultadoWhatsAppCommand _enviarResultadoWhatsAppCommand;

        public SorteiosController(SorteioMusicaCommand sorteioMusicaCommand, EnviarResultadoWhatsAppCommand enviarResultadoWhatsAppCommand)
        {
            _sorteioMusicaCommand = sorteioMusicaCommand;
            _enviarResultadoWhatsAppCommand = enviarResultadoWhatsAppCommand;
        }

        [HttpPost]
        public async Task<IActionResult> RealizarSorteio()
        {
            await _sorteioMusicaCommand.Execute();
            var message = "Resultado do sorteio de músicas...";
            var phoneNumber = "whatsapp:+1234567890"; // Substitua pelo número de telefone desejado
            await _enviarResultadoWhatsAppCommand.Execute(phoneNumber, message);
            return Ok(new { Message = "Sorteio realizado e resultados enviados para WhatsApp" });
        }
    }
}

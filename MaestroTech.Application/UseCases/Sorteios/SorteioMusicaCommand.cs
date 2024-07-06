using MaestroTech.Domain.Repositories;
using System.Threading.Tasks;

namespace MaestroTech.Application.UseCases.Sorteios
{
    public class SorteioMusicaCommand
    {
        private readonly IMusicaRepository _musicaRepository;

        public SorteioMusicaCommand(IMusicaRepository musicaRepository)
        {
            _musicaRepository = musicaRepository;
        }

        public async Task Execute()
        {
            // Adicione uma chamada await para tornar o método verdadeiramente assíncrono
            await Task.CompletedTask;
        }
    }
}

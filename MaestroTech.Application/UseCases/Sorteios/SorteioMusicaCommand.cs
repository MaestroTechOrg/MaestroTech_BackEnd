using MaestroTech.Domain.Repositories;

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
            // lógica de sorteio de músicas
        }
    }
}

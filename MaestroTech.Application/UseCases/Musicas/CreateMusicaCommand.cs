using MaestroTech.Domain.Entities;
using MaestroTech.Domain.Repositories;

namespace MaestroTech.Application.UseCases.Musicas
{
    public class CreateMusicaCommand
    {
        private readonly IMusicaRepository _musicaRepository;

        public CreateMusicaCommand(IMusicaRepository musicaRepository)
        {
            _musicaRepository = musicaRepository;
        }

        public async Task Execute(Musica musica)
        {
            await _musicaRepository.AddAsync(musica);
        }
    }
}

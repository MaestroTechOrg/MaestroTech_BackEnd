using MaestroTech.Domain.Entities;
using MaestroTech.Domain.Repositories;

namespace MaestroTech.Application.UseCases.Musicas
{
    public class UpdateMusicaCommand
    {
        private readonly IMusicaRepository _musicaRepository;

        public UpdateMusicaCommand(IMusicaRepository musicaRepository)
        {
            _musicaRepository = musicaRepository;
        }

        public async Task Execute(Musica musica)
        {
            await _musicaRepository.UpdateAsync(musica);
        }
    }
}

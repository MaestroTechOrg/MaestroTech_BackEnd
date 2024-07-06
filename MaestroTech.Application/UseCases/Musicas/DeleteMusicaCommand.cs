using MaestroTech.Domain.Repositories;

namespace MaestroTech.Application.UseCases.Musicas
{
    public class DeleteMusicaCommand
    {
        private readonly IMusicaRepository _musicaRepository;

        public DeleteMusicaCommand(IMusicaRepository musicaRepository)
        {
            _musicaRepository = musicaRepository;
        }

        public async Task Execute(int id)
        {
            await _musicaRepository.DeleteAsync(id);
        }
    }
}

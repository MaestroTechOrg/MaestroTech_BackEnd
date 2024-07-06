using MaestroTech.Domain.Repositories;

namespace MaestroTech.Application.UseCases.Usuarios
{
    public class DeleteUsuarioCommand
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public DeleteUsuarioCommand(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task Execute(int id)
        {
            await _usuarioRepository.DeleteAsync(id);
        }
    }
}

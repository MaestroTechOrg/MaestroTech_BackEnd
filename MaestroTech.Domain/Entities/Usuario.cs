using Microsoft.AspNetCore.Identity;

namespace MaestroTech.Domain.Entities
{
    public class Usuario : IdentityUser<int>
    {
        public string? Nome { get; set; }
        public string? Perfil { get; set; } // Administrador, Usuário
        public int IgrejaId { get; set; }
        public Igreja? Igreja { get; set; }
    }
}

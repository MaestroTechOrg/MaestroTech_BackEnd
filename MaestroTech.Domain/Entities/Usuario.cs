namespace MaestroTech.Domain.Entities
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Perfil { get; set; } // Administrador, Usuário
        public int IgrejaId { get; set; }
        public Igreja Igreja { get; set; }
    }
}

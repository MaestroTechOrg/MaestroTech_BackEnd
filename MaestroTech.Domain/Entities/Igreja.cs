namespace MaestroTech.Domain.Entities
{
    public class Igreja
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string UF { get; set; }
        public string Cidade { get; set; }
        public List<Culto> DiasDeCulto { get; set; }
        public List<Usuario> Usuarios { get; set; }
    }
}

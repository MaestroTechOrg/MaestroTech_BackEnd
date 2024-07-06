namespace MaestroTech.Domain.Entities
{
    public class Culto
    {
        public int Id { get; set; }
        public string DiaDaSemana { get; set; }
        public int IgrejaId { get; set; }
        public Igreja Igreja { get; set; }
    }
}

namespace MaestroTech.Domain.Entities
{
    public class Musica
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string LinkSpotify { get; set; }
        public string LinkYouTube { get; set; }
        public string Notas { get; set; }
        public string Cantores { get; set; }
        public string DiaDePreferencia { get; set; }
        public int IgrejaId { get; set; }
        public Igreja Igreja { get; set; }
    }
}

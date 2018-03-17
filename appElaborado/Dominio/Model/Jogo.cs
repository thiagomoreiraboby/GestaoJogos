namespace Dominio.Model
{
    public class Jogo: EntidadeBase
    {
        public string Nome { get; set; }
        public int? AmigoID { get; set; }
        public Amigo Amigo { get; set; }
    }
}

namespace LivrariaHenriqueKorp.Models
{
    public class LivroModel
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Capa { get; set; } = string.Empty ;
        public string Descricao { get; set; } = string.Empty;
        public double Valor { get; set; }
    }
}

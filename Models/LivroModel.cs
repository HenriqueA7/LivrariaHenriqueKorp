using System.ComponentModel.DataAnnotations;

namespace LivrariaHenriqueKorp.Models
{
    public class LivroModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O título do livro é obrigatório")]
        public string Titulo { get; set; } = string.Empty;
        public string Capa { get; set; } = string.Empty ;
        [Required(ErrorMessage = "A descrição do livro é obrigatória")]
        public string Descricao { get; set; } = string.Empty;
        [Required(ErrorMessage = "O valor é obrigatório")]
        [Range(0.01, 999.99, ErrorMessage = "A valor deve ser entre R$0,01 e R$999,99")]
        public double? Valor { get; set; }
    }
}

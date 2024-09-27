using LivrariaHenriqueKorp.Dto;
using LivrariaHenriqueKorp.Models;

namespace LivrariaHenriqueKorp.Services.Livro
{
    public interface ILivroInterface
    {
        Task<LivroModel> CriarLivro(LivroCriacaoDto livroCriacaoDto, IFormFile foto);
        Task<List<LivroModel>> GetLivros();
        Task<LivroModel> GetLivroPorId(int id);
        Task<LivroModel> EditarLivro(LivroModel livro, IFormFile? foto);
        Task<LivroModel> RemoverLivro(int id);
        Task<List<LivroModel>> GetLivrosFiltro(string? pesquisa);
    }
}

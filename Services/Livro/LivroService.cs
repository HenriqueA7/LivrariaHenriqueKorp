using LivrariaHenriqueKorp.Data;
using LivrariaHenriqueKorp.Dto;
using LivrariaHenriqueKorp.Models;
using Microsoft.EntityFrameworkCore;

namespace LivrariaHenriqueKorp.Services.Livro
{
	public class LivroService : ILivroInterface
	{
		private readonly AppDbContext _context;
		private readonly string _sistema;

        public LivroService(AppDbContext context, IWebHostEnvironment sistema)
        {
			_context = context;
			_sistema = sistema.WebRootPath;
        }
		public string GeraCaminhoArquivo(IFormFile foto)
		{
			var codigoUnico = Guid.NewGuid().ToString();
			var nomeCaminhoImagem = foto.FileName.Replace(" ", "").ToLower() + codigoUnico + ".png";

			var caminhoParaSalvarImagem = _sistema + "\\imagem\\";

			if (!Directory.Exists(caminhoParaSalvarImagem))
			{
				Directory.CreateDirectory(caminhoParaSalvarImagem);
			}

			using (var stream = File.Create(caminhoParaSalvarImagem + nomeCaminhoImagem))
			{
				foto.CopyToAsync(stream).Wait();
			}

			return nomeCaminhoImagem;
		}
        public async Task<LivroModel> CriarLivro(LivroCriacaoDto livroCriacaoDto, IFormFile foto)
		{
			try
			{
				var nomeCaminhoImagem = GeraCaminhoArquivo(foto);

				var livro = new LivroModel
				{
					Titulo = livroCriacaoDto.Titulo,
					Descricao = livroCriacaoDto.Descricao,
					Valor = (double)livroCriacaoDto.Valor,
					Capa = nomeCaminhoImagem
				};

				_context.Add(livro);
				await _context.SaveChangesAsync();

				return livro;
			}
			catch (Exception ex) 
			{
				throw new NotImplementedException();
			}
		}
		public async Task<List<LivroModel>> GetLivros()
		{
			try
			{
				return await _context.Livros.ToListAsync();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<LivroModel> GetLivroPorId(int id)
		{
			try
			{
				return await _context.Livros.FirstOrDefaultAsync(livro => livro.Id == id);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
		public async Task<LivroModel> EditarLivro(LivroModel livro, IFormFile? foto)
		{
			try
			{
				var livroBanco = await _context.Livros.AsNoTracking().FirstOrDefaultAsync(livroDb => livroDb.Id == livro.Id);

				var nomeCaminhoCapa = "";

				if (foto != null) 
				{
					string caminhoCapaExistente = _sistema + "\\imagem\\" + livroBanco.Capa;

					if (File.Exists(caminhoCapaExistente))
					{
						File.Delete(caminhoCapaExistente);
					}

					nomeCaminhoCapa = GeraCaminhoArquivo(foto);
				}

				if (nomeCaminhoCapa != "")
				{
					livroBanco.Capa = nomeCaminhoCapa;
				}
				else
				{
					livroBanco.Capa = livro.Capa;
				}

				livroBanco.Titulo = livro.Titulo;
				livroBanco.Descricao = livro.Descricao;
				livroBanco.Valor = livro.Valor;

				_context.Update(livroBanco);
				await _context.SaveChangesAsync();

				return livro;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<LivroModel> RemoverLivro(int id)
		{
			try
			{
				var livro = await _context.Livros.FirstOrDefaultAsync(x => x.Id == id);

				_context.Remove(livro);
				_context.SaveChanges();

				return livro;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

        public async Task<List<LivroModel>> GetLivrosFiltro(string? pesquisar)
        {
            try
            {
				var livros = await _context.Livros.Where(livroBanco => livroBanco.Titulo.Contains(pesquisar)).ToListAsync();
                return livros;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

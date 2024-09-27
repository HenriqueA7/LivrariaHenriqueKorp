using LivrariaHenriqueKorp.Dto;
using LivrariaHenriqueKorp.Models;
using LivrariaHenriqueKorp.Services.Livro;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace LivrariaHenriqueKorp.Controllers
{
    public class LivroController : Controller
    {
		private readonly ILivroInterface _livrointerface;

		public LivroController(ILivroInterface livroInterface)
        {
            _livrointerface = livroInterface;
        }

        public async Task<IActionResult> Index()
        {
            var livros = await _livrointerface.GetLivros();
            return View(livros);
        }
        public IActionResult Cadastrar() 
        {
            return View(); 
        }
		public async Task<IActionResult> Editar(int id)
		{
			var livro = await _livrointerface.GetLivroPorId(id);

			return View(livro);
		}
		public async Task<IActionResult> Remover(int id)
		{
			var livro = await _livrointerface.RemoverLivro(id);

			return RedirectToAction("Index", "Livro");
		}
		public async Task<IActionResult> Detalhes (int id)
		{
			var livro = await _livrointerface.GetLivroPorId(id);
			return View(livro);
		}

		[HttpPost]
        public async Task<IActionResult> Cadastrar(LivroCriacaoDto livroCriacaoDto, IFormFile foto)
        {
            if (ModelState.IsValid) 
            {
                var livro = await _livrointerface.CriarLivro(livroCriacaoDto, foto);
                return RedirectToAction("Index", "Livro");
            }
            else
            {
                return View(livroCriacaoDto);
            }
        }

        [HttpPost]
		public async Task<IActionResult> Editar(LivroModel livroModel, IFormFile? foto)
		{
			if (ModelState.IsValid)
			{
				var livro = await _livrointerface.EditarLivro(livroModel, foto);
				return RedirectToAction("Index", "Livro");
			}
			else
			{
				return View(livroModel);
			}
		}
		
		[HttpGet]
		[Route("livro/excluir/{id}")]
		public async Task<IActionResult> RemoverLivro(int id)
		{
			try
			{
				var livro = await _livrointerface.RemoverLivro(id);
				return Json(new { ok = true, message = "Removido com sucesso"});
			}
			catch (Exception)
			{
				return Json(new { ok = false, message = "Houve um problema, tente novamente mais tarde" });
			}
		}
	}
}

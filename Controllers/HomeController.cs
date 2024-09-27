using LivrariaHenriqueKorp.Services.Livro;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LivrariaHenriqueKorp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILivroInterface _liivroInterface;
        public HomeController(ILivroInterface livroInterface)
        {
            _liivroInterface = livroInterface;
        }

        public async Task<IActionResult> Index(string? pesquisar)
        {
            if (pesquisar == null)
            {
                var livro = await _liivroInterface.GetLivros();
                return View(livro);
            }
            else
            {
                var livro = await _liivroInterface.GetLivrosFiltro(pesquisar);
                return View(livro);
            }
        }
    }
}

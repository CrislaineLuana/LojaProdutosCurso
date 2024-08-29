using LojaProdutosCurso.Filtros;
using LojaProdutosCurso.Models;
using LojaProdutosCurso.Services.Produto;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LojaProdutosCurso.Controllers
{
    [UsuarioLogado]
    public class HomeController : Controller
    {
        private readonly IProdutoInterface _produtoInterface;

        public HomeController(IProdutoInterface produtoInterface)
        {
            _produtoInterface = produtoInterface;
        }

 
        public async Task<IActionResult> Index(string? pesquisar)
        {

            List<ProdutoModel> produtos = new List<ProdutoModel>();

            if (pesquisar == null)
            {
                produtos = await _produtoInterface.BuscarProdutos();
            }
            else
            {
                produtos = await _produtoInterface.BuscarProdutoFiltro(pesquisar);
            }


            return View(produtos);
        }

       
    }
}

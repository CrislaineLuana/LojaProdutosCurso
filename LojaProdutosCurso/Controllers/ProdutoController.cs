using LojaProdutosCurso.Dto.Produto;
using LojaProdutosCurso.Filtros;
using LojaProdutosCurso.Services.Categoria;
using LojaProdutosCurso.Services.Produto;
using Microsoft.AspNetCore.Mvc;

namespace LojaProdutosCurso.Controllers
{
    [UsuarioLogado]
    public class ProdutoController : Controller
    {
        private readonly IProdutoInterface _produtoInterface;
        private readonly ICategoriaInterface _categoriaInterface;
        public ProdutoController(IProdutoInterface produtoInterface,
                                 ICategoriaInterface categoriaInterface   
                                    )
        {
            _produtoInterface = produtoInterface;
            _categoriaInterface = categoriaInterface;
        }

        [UsuarioLogadoAdm]
        public async Task<IActionResult> Index()
        {

            var produtos = await _produtoInterface.BuscarProdutos();

            return View(produtos);
        }

        [UsuarioLogadoAdm]  
        public async Task<IActionResult> Cadastrar()
        {

            ViewBag.Categorias = await _categoriaInterface.BuscarCategorias();

            return View();
        }

        [UsuarioLogadoAdm]  
        public async Task<IActionResult> Remover(int id)
        {
            var produto = await _produtoInterface.Remover(id);
            return RedirectToAction("Index", "Produto");
        }


        public async Task<IActionResult> Detalhes(int id)
        {
            var produto = await _produtoInterface.BuscarProdutoPorId(id); 
            return View(produto);
        }

        [UsuarioLogadoAdm]  
        public async Task<IActionResult> Editar(int id)
        {
            var produto = await _produtoInterface.BuscarProdutoPorId(id);

            var editarProdutoDto = new EditarProdutoDto
            {
                Nome = produto.Nome,
                Marca = produto.Marca,
                Foto = produto.Foto,
                Valor = produto.Valor,
                QuantidadeEstoque = produto.QuantidadeEstoque,
                CategoriaModelId = produto.CategoriaModelId,
            };

            ViewBag.Categorias = await _categoriaInterface.BuscarCategorias();

            return View(editarProdutoDto);
            
        }


        [HttpPost]
        [UsuarioLogadoAdm]
        public async Task<IActionResult> Cadastrar(CriarProdutoDto criarProdutoDto, IFormFile foto)
        {
            if (ModelState.IsValid) {

                var produto = await _produtoInterface.Cadastrar(criarProdutoDto, foto);
                //Sucesso
                TempData["MensagemSucesso"] = "Produto cadastrado com sucesso!";
                return RedirectToAction("Index","Produto");

            }
            else
            {
                //Erro
                TempData["MensagemErro"] = "Ocorreu algum erro no processo!";
                ViewBag.Categorias = await _categoriaInterface.BuscarCategorias();
                return View(criarProdutoDto);
            }
        }

        [HttpPost]
        [UsuarioLogadoAdm]
        public async Task<IActionResult> Editar(EditarProdutoDto editarProdutoDto, IFormFile? foto)
        {
            if (ModelState.IsValid) 
            {
                var produto = await _produtoInterface.Editar(editarProdutoDto, foto);
                //Sucesso
                TempData["MensagemSucesso"] = "Produto editado com sucesso!";
                return RedirectToAction("Index", "Produto");
            }
            else
            {
                ViewBag.Categorias = await _categoriaInterface.BuscarCategorias();
                //Erro
                TempData["MensagemErro"] = "Ocorreu algum erro no processo!";
                return View(editarProdutoDto);
            }
        }

    }
}

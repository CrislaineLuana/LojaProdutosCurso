using LojaProdutosCurso.Dto.Produto;
using LojaProdutosCurso.Models;

namespace LojaProdutosCurso.Services.Produto
{
    public interface IProdutoInterface
    {

        Task<List<ProdutoModel>> BuscarProdutos();
        Task<ProdutoModel> Cadastrar(CriarProdutoDto criarProdutoDto, IFormFile foto);
        Task<ProdutoModel> BuscarProdutoPorId(int id);
        Task<ProdutoModel> Editar(EditarProdutoDto editarProdutoDto, IFormFile? foto);
        Task<ProdutoModel> Remover(int id); 
    }
}

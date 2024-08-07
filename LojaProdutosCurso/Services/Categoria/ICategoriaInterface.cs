using LojaProdutosCurso.Models;

namespace LojaProdutosCurso.Services.Categoria
{
    public interface ICategoriaInterface
    {

        Task<List<CategoriaModel>> BuscarCategorias();


    }
}

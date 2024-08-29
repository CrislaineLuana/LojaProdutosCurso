using LojaProdutosCurso.Dto.Login;
using LojaProdutosCurso.Dto.Usuario;
using LojaProdutosCurso.Models;

namespace LojaProdutosCurso.Services.Usuario
{
    public interface IUsuarioInterface
    {
        Task<List<UsuarioModel>> BuscarUsuarios();
        Task<UsuarioModel> BuscarUsuarioPorId(int id);
        Task<bool> VerificaSeExisteEmail(CriarUsuarioDto criarUsuarioDto);
        Task<CriarUsuarioDto> Cadastrar(CriarUsuarioDto criarUsuarioDto);

        Task<UsuarioModel> Editar(EditarUsuarioDto editarUsuarioDto);
        Task<UsuarioModel> Login(LoginUsuarioDto loginUsuarioDto);

    }
}

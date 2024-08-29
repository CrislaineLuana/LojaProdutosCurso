using AutoMapper;
using LojaProdutosCurso.Data;
using LojaProdutosCurso.Dto.Login;
using LojaProdutosCurso.Dto.Usuario;
using LojaProdutosCurso.Models;
using LojaProdutosCurso.Services.Autenticacao;
using LojaProdutosCurso.Services.Sessao;
using Microsoft.EntityFrameworkCore;

namespace LojaProdutosCurso.Services.Usuario
{
    public class UsuarioService : IUsuarioInterface
    {
        private readonly DataContext _context;
        private readonly IAutenticacaoInterface _autenticacaoInterface;
        private readonly IMapper _mapper;
        private readonly ISessaoInterface _sessaoInterface;

        public UsuarioService(DataContext context, 
            IAutenticacaoInterface autenticacaoInterface,
            IMapper mapper,
            ISessaoInterface sessaoInterface)
        {
            _context = context;
            _autenticacaoInterface = autenticacaoInterface;
            _mapper = mapper;
            _sessaoInterface = sessaoInterface;
        }


        public async Task<UsuarioModel> BuscarUsuarioPorId(int id)
        {
            try
            {
                var usuario = await _context.Usuarios.Include(e => e.Endereco)
                                                .FirstOrDefaultAsync(u => u.Id == id);

                return usuario;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<UsuarioModel>> BuscarUsuarios()
        {
            try
            {
                return await _context.Usuarios.Include(e => e.Endereco).ToListAsync();
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CriarUsuarioDto> Cadastrar(CriarUsuarioDto criarUsuarioDto)
        {
            try
            {
                //servico que cria a senhaHash e senhaSalt
                _autenticacaoInterface.CriarSenhaHash(criarUsuarioDto.Senha, out byte[] senhaHash, out byte[] senhaSalt);

                var usuario = new UsuarioModel
                {
                    Nome = criarUsuarioDto.Nome,
                    Email = criarUsuarioDto.Email,
                    Cargo = criarUsuarioDto.Cargo,
                    SenhaHash = senhaHash,
                    SenhaSalt = senhaSalt
                };

                var endereco = new EnderecoModel
                {

                    Logradouro = criarUsuarioDto.Logradouro,
                    Numero = criarUsuarioDto.Numero,
                    Bairro = criarUsuarioDto.Bairro,
                    Estado = criarUsuarioDto.Estado,
                    Complemento = criarUsuarioDto.Complemento,
                    CEP = criarUsuarioDto.CEP,
                    Usuario = usuario

                };


                usuario.Endereco = endereco;

                _context.Add(usuario);
                await _context.SaveChangesAsync();

                return criarUsuarioDto;


            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<UsuarioModel> Editar(EditarUsuarioDto editarUsuarioDto)
        {
            try
            {
                var usuarioBanco = await _context.Usuarios.Include(e => e.Endereco).FirstOrDefaultAsync(x => x.Id == editarUsuarioDto.Id);

                usuarioBanco.Nome = editarUsuarioDto.Nome;
                usuarioBanco.Cargo = editarUsuarioDto.Cargo;
                usuarioBanco.Email = editarUsuarioDto.Email;
                usuarioBanco.DataAlteracao = DateTime.Now;
                usuarioBanco.Endereco = _mapper.Map<EnderecoModel>(editarUsuarioDto.Endereco);

                _context.Update(usuarioBanco);
                await _context.SaveChangesAsync();

                return usuarioBanco;

            }
            catch (Exception ex) 
            {
                throw new Exception (ex.Message);
            }
        }

        public async Task<UsuarioModel> Login(LoginUsuarioDto loginUsuarioDto)
        {
            try
            {

                var usuarioBanco = await _context.Usuarios.FirstOrDefaultAsync(x => x.Email == loginUsuarioDto.Email);

                if (usuarioBanco == null) 
                {
                    return null;
                }

                if (!_autenticacaoInterface.VerificaLogin(loginUsuarioDto.Senha, usuarioBanco.SenhaHash, usuarioBanco.SenhaSalt))
                {
                    return null;
                }

                //Criar Sessao
                _sessaoInterface.CriarSessao(usuarioBanco);

                return usuarioBanco;

            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> VerificaSeExisteEmail(CriarUsuarioDto criarUsuarioDto)
        {
            try
            {

                var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.Email == criarUsuarioDto.Email);

                if(usuario == null)
                {
                    return false;
                }

                return true;

            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

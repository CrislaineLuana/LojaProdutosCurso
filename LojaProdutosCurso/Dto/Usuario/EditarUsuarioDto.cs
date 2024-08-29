﻿using LojaProdutosCurso.Dto.Endereco;
using LojaProdutosCurso.Enums;

namespace LojaProdutosCurso.Dto.Usuario
{
    public class EditarUsuarioDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public CargoEnum Cargo { get; set; }
        public EditarEnderecoDto Endereco { get; set; } 
    }
}

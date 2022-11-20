using System;
using System.ComponentModel.DataAnnotations;

namespace api.Model
{
    public class Usuario
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "Informe o nome do usuario para realizar o cadastro!")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Informe o CPF do usuario para realizar o cadastro!")]
        public string Cpf { get; set; }
        [Required(ErrorMessage = "Informe o E-Mail do usuario para realizar o cadastro!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Informe a senha do usuario para realizar o cadastro!")]
        public string Senha { get; set; }
        [Required(ErrorMessage = "Informe o Telefone do usuario para realizar o cadastro!")]
        public string Telefone { get; set; }

        public Usuario()
        {
        }
    }
}


using System;
using System.ComponentModel.DataAnnotations;

namespace api.Dto
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Informe o E-Mail do usuario para realizar o Login!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Informe a senha do usuario para realizar o Login!")]
        public string Senha { get; set; }

    }
}


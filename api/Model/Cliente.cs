using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Model
{
    public class Cliente
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe o CPF do cliente para realizar o cadastro!")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "Informe o E-mail do Cliente para realizar o cadastro!")]
        public string Email { get; set; }

        public Boolean Situacao { get; set; }

        public Cliente()
        {
        }
    }
}

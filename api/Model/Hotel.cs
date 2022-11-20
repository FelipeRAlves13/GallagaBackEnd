using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Model
{
    public class Hotel
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe o nome do Hotel para realizar o cadastro!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe a Cidade para realizar o cadastro!")]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "Informe a UF para realizar o cadastro!")]
        public string Uf { get; set; }

        [Required(ErrorMessage = "Informe o Logradouro do Hotel para realizar o cadastro!")]
        public string Logradouro { get; set; }

        [Required(ErrorMessage = "Informe o Bairro do Hotel para realizar o cadastro!")]
        public string Bairro { get; set; }

        [Required(ErrorMessage = "Informe o Número do Hotel para realizar o cadastro!")]
        public string Numero { get; set; }

        [ForeignKey("Usuario")]
        [Required(ErrorMessage = "Informe o Id do responsável pelo Hotel para realizar o cadastro!")]
        public int IdUsuario { get; set; }
        public virtual Usuario Usuario { get; set; }

        public Hotel()
        {
        }
    }
}

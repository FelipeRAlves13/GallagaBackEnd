using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Model
{
    public class Quarto
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [ForeignKey("Hotel")]
        [Required(ErrorMessage = "Informe o Id do Hotel para realizar o cadastro!")]
        public int IdHotel { get; set; }
        public virtual Hotel Hotel { get; set; }

        [Required(ErrorMessage = "Informe o número do Quarto para realizar o cadastro!")]
        public int NumeroQuarto { get; set; }

        [Required(ErrorMessage = "Informe a Classificação do Quarto para realizar o cadastro!")]
        public string Classificacao { get; set; }

        [Required(ErrorMessage = "Informe o valor da diaria do Quarto para realizar o cadastro!")]
        public double ValorDiaria { get; set; }

        public Boolean Ocupado { get; set; }

        public Quarto()
        {
        }
    }
}


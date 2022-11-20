using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Model
{
    public class HistoricoCliente
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [ForeignKey("Cliente")]
        public int IdCliente { get; set; }
        public virtual Cliente Cliente { get; set; }

        [ForeignKey("Quarto")]
        public int IdQuarto { get; set; }
        public virtual Quarto Quarto { get; set; }

        public string DataEntrada { get; set; }

        public string DataSaida { get; set; }

        public string Cpf { get; set; }

        public double ValorPago { get; set; }

        public Boolean Situacao { get; set; }

        public HistoricoCliente()
        {
        }
    }
}


using System;
using System.ComponentModel.DataAnnotations;

namespace api.Dto
{
    public class QuartosOcupados
    {
        public string NomeCliente { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public int IdQuarto { get; set; }
        public int NumeroQuarto { get; set; }
        public string Classificacao { get; set; }
        public string DataEntrada { get; set; }
        public string DataSaida { get; set; }

        public QuartosOcupados()
        {
        }
    }
}
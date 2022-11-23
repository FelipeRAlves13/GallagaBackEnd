using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Model;
using api.Dto;

namespace api.Controllers
{
    [Route("api/quarto")]
    public class ControllerQuarto : ControllerBase
    {
        private SistemaContext _context;

        public ControllerQuarto(SistemaContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult AddQuarto([FromBody] Quarto quarto)
        {
            quarto.Ocupado = false;
            _context.Quartos.Add(quarto);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetQuartoById), new { Id = quarto.Id }, quarto);
        }

        [HttpGet]
        public IEnumerable<Quarto> GetQuartos()
        {
            return _context.Quartos;
        }

        [HttpGet("{id}")]
        public IActionResult GetQuartoById(int id)
        {
            Quarto quarto = _context.Quartos.FirstOrDefault(quarto => quarto.Id == id);
            if (quarto != null)
            {
                return Ok(quarto);
            }
            return NotFound();
        }

        [HttpPut("update/{id}")]
        public IActionResult UpdateQuartoById(int id, [FromBody] Quarto quartoRequest)
        {
            Quarto quarto = _context.Quartos.FirstOrDefault(quarto => quarto.Id == id);
            if (quarto != null)
            {
                quarto.NumeroQuarto = quartoRequest.NumeroQuarto;
                _context.SaveChanges();
                return Ok();
            }
            return NotFound();
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteQuartoById(int id)
        {
            Quarto quarto = _context.Quartos.FirstOrDefault(quarto => quarto.Id == id);
            if (quarto != null)
            {
                _context.Quartos.Remove(quarto);
                _context.SaveChanges();
                return Ok();
            }
            return NotFound();
        }

        [HttpGet("ocupados/{id}")]
        public IActionResult GetQuartosOcupados(int id)
        {
            var a = (from q in _context.Quartos
                     join h in _context.HistoricoClientes on q.Id equals h.IdQuarto
                     join c in _context.Clientes on h.IdCliente equals c.Id
                     where q.Ocupado == true && q.IdHotel == id && h.Situacao == true
                     select new QuartosOcupados
                     {
                         NomeCliente = h.Nome,
                         Email = c.Email,
                         Cpf = c.Cpf,
                         IdQuarto = q.Id,
                         NumeroQuarto = q.NumeroQuarto,
                         Classificacao = q.Classificacao,
                         DataEntrada = h.DataEntrada,
                         DataSaida = h.DataSaida
                     });

            return Ok(a);
        }
    }
}

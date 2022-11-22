using System;
using api.Data;
using api.Model;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/cliente")]
    public class ControllerCliente : ControllerBase
    {
        private SistemaContext _context;

        public ControllerCliente(SistemaContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult AddCliente([FromBody] Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetClienteById), new { Id = cliente.Id }, cliente);
        }

        [HttpGet]
        public IEnumerable<Cliente> GetClientes()
        {
            return _context.Clientes;
        }

        [HttpGet("{id}")]
        public IActionResult GetClienteById(int id)
        {
            Cliente cliente = _context.Clientes.FirstOrDefault(cliente => cliente.Id == id);
            if (cliente != null)
            {
                return Ok(cliente);
            }
            return NotFound();
        }

        [HttpPut("update/{id}")]
        public IActionResult UpdateClienteById(int id, [FromBody] Cliente clienteRequest)
        {
            Cliente cliente = _context.Clientes.FirstOrDefault(cliente => cliente.Id == id);
            if (cliente != null)
            {
                cliente.Email= clienteRequest.Email;
                _context.SaveChanges();
                return Ok();
            }
            return NotFound();
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteClienteById(int id)
        {
            Cliente cliente = _context.Clientes.FirstOrDefault(cliente => cliente.Id == id);
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
                _context.SaveChanges();
                return Ok();
            }
            return NotFound();
        }

        [HttpGet("historico/{id}")]
        public IActionResult GetHistoricoClienteById(int id)
        {
            HistoricoCliente historico = _context.HistoricoClientes.FirstOrDefault(h => h.IdCliente == id);
            if (historico != null)
            {
                return Ok(historico);
            }
            return NotFound();
        }

        [HttpPost("check-out/{cpf}")]
        public IActionResult CheckoutClienteById(string cpf)
        {
            HistoricoCliente historicoCliente = _context.HistoricoClientes
                .FirstOrDefault(c => c.Cpf == cpf && c.Situacao == true);

            Quarto quarto = _context.Quartos.FirstOrDefault(quarto => quarto.Id == historicoCliente.IdQuarto);

            if (historicoCliente != null && quarto != null)
            {
                Cliente cliente = _context.Clientes.FirstOrDefault(c => c.Id == historicoCliente.IdCliente);
                cliente.Situacao = false;

                historicoCliente.Situacao = false;

                quarto.Ocupado = false;

                _context.SaveChanges();
                return Ok();
            }
            return NotFound();
        }

        [HttpPost("check-in/{cpf}")]
        public IActionResult CheckinClienteById(string cpf, [FromBody] HistoricoCliente historico)
        {
            Cliente cliente = _context.Clientes.FirstOrDefault(c => c.Cpf == cpf);

            if (cliente != null && ValidaQuartoOcupado(historico.IdQuarto) == false)
            {
                Quarto quarto = _context.Quartos.FirstOrDefault(quarto => quarto.Id == historico.IdQuarto);
                quarto.Ocupado = true;

                cliente.Situacao = true;
                HistoricoCliente historicoCliente = new HistoricoCliente();
                historicoCliente.IdCliente = cliente.Id;
                historicoCliente.IdQuarto = historico.IdQuarto;
                historicoCliente.DataEntrada = historico.DataEntrada;
                historicoCliente.DataSaida = historico.DataSaida;
                historicoCliente.Cpf = cliente.Cpf;
                historicoCliente.Nome = cliente.Nome;
                historicoCliente.ValorPago = historico.ValorPago;
                historicoCliente.Situacao = true;

                _context.HistoricoClientes.Add(historicoCliente);
                _context.SaveChanges();

                return Ok();
            }
            return NotFound();
        }

        private Boolean ValidaQuartoOcupado(int idQuarto)
        {
            Quarto quarto = _context.Quartos.FirstOrDefault(quarto => quarto.Id == idQuarto);
            if (quarto != null && quarto.Ocupado == true)
            {
                return true;
            }
            return false;
        }
    }
}

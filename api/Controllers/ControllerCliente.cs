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

        [HttpGet("check-out/{id}")]
        public IActionResult CheckoutClienteById(int id, HistoricoCliente historico)
        {
            Cliente cliente = _context.Clientes.FirstOrDefault(c => c.Id == id);
            if (cliente != null)
            {
                cliente.Situacao = false;
                HistoricoCliente historicoCliente =  new HistoricoCliente();
                historicoCliente.IdCliente = historico.Id;
                historicoCliente.IdQuarto = historico.IdQuarto;
                historicoCliente.DataEntrada = historico.DataEntrada;
                historicoCliente.DataSaida = historico.DataSaida;
                historicoCliente.Cpf = cliente.Cpf;
                historicoCliente.ValorPago = historico.ValorPago;

                _context.HistoricoClientes.Add(historicoCliente);
                _context.SaveChanges();

                return Ok();
            }
            return NotFound();
        }

        [HttpGet("check-in/{id}")]
        public IActionResult CheckinClienteById(int id, HistoricoCliente historico)
        {
            Cliente cliente = _context.Clientes.FirstOrDefault(c => c.Id == id);
            if (cliente != null && ValidaQuartoOcupado(historico.IdQuarto) == false)
            {
                cliente.Situacao = true;
                HistoricoCliente historicoCliente = new HistoricoCliente();
                historicoCliente.IdCliente = cliente.Id;
                historicoCliente.IdQuarto = historico.IdQuarto;
                historicoCliente.DataEntrada = historico.DataEntrada;
                historicoCliente.DataSaida = historico.DataSaida;
                historicoCliente.Cpf = cliente.Cpf;
                historicoCliente.ValorPago = historico.ValorPago;

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

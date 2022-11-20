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
    [Route("api/usuario")]
    public class ControllerUsuario : ControllerBase
    {
        private SistemaContext _context;

        public ControllerUsuario(SistemaContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult AddUsuario([FromBody] Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetUsuarioById), new { Id = usuario.Id }, usuario);
        }

        [HttpGet]
        public IEnumerable<Usuario> GetUsuarios()
        {
            return _context.Usuarios;
        }

        [HttpGet("{id}")]
        public IActionResult GetUsuarioById(int id)
        {
            Usuario usuario = _context.Usuarios.FirstOrDefault(usuario => usuario.Id == id);
            if (usuario != null)
            {
                return Ok(usuario);
            }
            return NotFound();
        }

        [HttpPut("update/{id}")]
        public IActionResult UpdateUsuarioById(int id, [FromBody] Usuario usuarioRequest)
        {
            Usuario usuario = _context.Usuarios.FirstOrDefault(usuario => usuario.Id == id);
            if (usuario != null)
            {
                usuario.Nome = usuarioRequest.Nome;
                _context.SaveChanges();
                return Ok();
            }
            return NotFound();
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteUsuarioById(int id)
        {
            Usuario usuario = _context.Usuarios.FirstOrDefault(usuario => usuario.Id == id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
                _context.SaveChanges();
                return Ok();
            }
            return NotFound();
        }

        [HttpGet("login")]
        public IActionResult LoginUsuario([FromBody] LoginDto loginRequest)
        {
            Usuario usuario = _context.Usuarios.FirstOrDefault(usuario => usuario.Email == loginRequest.Email &
            usuario.Senha == loginRequest.Senha);
            if (usuario != null)
            {
                return Ok();
            }
            return NotFound("Usuario não localizado ou senha inválida!");
        }
    }
}

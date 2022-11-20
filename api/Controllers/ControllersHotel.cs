using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Model;

namespace api.Controllers
{
    [Route("api/hotel")]
    public class ControllerHotel : ControllerBase
    {
        private SistemaContext _context;

        public ControllerHotel(SistemaContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult AddHotel([FromBody] Hotel hotel)
        {
            _context.Hotel.Add(hotel);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetHotelById), new { Id = hotel.Id }, hotel);
        }

        [HttpGet]
        public IEnumerable<Hotel> GetHotel()
        {
            return _context.Hotel;
        }

        [HttpGet("{id}")]
        public IActionResult GetHotelById(int id)
        {
            Hotel hotel = _context.Hotel.FirstOrDefault(hotel => hotel.Id == id);
            if (hotel != null)
            {
                return Ok(hotel);
            }
            return NotFound();
        }

        [HttpPut("update/{id}")]
        public IActionResult UpdateHotelById(int id, [FromBody] Hotel hotelRequest)
        {
            Hotel hotel = _context.Hotel.FirstOrDefault(hotel => hotel.Id == id);
            if (hotel != null)
            {
                hotel.Nome = hotelRequest.Nome;
                _context.SaveChanges();
                return Ok();
            }
            return NotFound();
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteHotelById(int id)
        {
            Hotel hotel = _context.Hotel.FirstOrDefault(hotel => hotel.Id == id);
            if (hotel != null)
            {
                _context.Hotel.Remove(hotel);
                _context.SaveChanges();
                return Ok();
            }
            return NotFound();
        }
    }
}

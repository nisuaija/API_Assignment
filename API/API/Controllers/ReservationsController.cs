using API.Models;
using API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReservationsController : Controller
    {

        private readonly BookContext _context;

        public ReservationsController(BookContext context)
        {
            _context = context;
        }

        [HttpPost("AddReservation")]
        public IActionResult AddReservation(string ID, string comment)
        {
            try
            {
                if (string.IsNullOrEmpty(ID) || string.IsNullOrEmpty(comment))
                {
                    return BadRequest("ID and comment are required.");
                }

                Book? foundBook = _context.Books.Include(b => b.Reservation).FirstOrDefault(b => b.Id == ID);

                if (foundBook != null)
                {
                    if (foundBook.Reservation != null)
                        return BadRequest("Book is already reserved");

                    Reservation res = new Reservation();
                    res.Comment = comment;
                    res.Id = Guid.NewGuid().ToString();
                    foundBook.Reservation = res;

                    _context.SaveChanges();
                    return Ok(res);
                }
                else
                {
                    return NotFound("Book not found");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("DeleteReservation")]
        public IActionResult DeleteReservation(string ID)
        {
            try
            {
                if (string.IsNullOrEmpty(ID))
                {
                    return BadRequest("ID and comment are required.");
                }

                Book? foundBook = _context.Books.Include(b => b.Reservation).FirstOrDefault(b => b.Id == ID);

                if (foundBook != null)
                {
                    if (foundBook.Reservation != null)
                    {
                        _context.Reservations.Remove(foundBook.Reservation);
                        foundBook.Reservation = null;
                        _context.SaveChanges();
                        return Ok("Reservation removed");
                    }
                    else
                        return NotFound("Book doesn't have reservation");
                }
                else
                {
                    return NotFound("Book not found");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GetReservedBooks")]
        public IActionResult GetReservedBooks()
        {
            try
            {
                var books = _context.Books.Include(b=> b.Reservation).Where(b => b.Reservation != null).ToList();
                if (books.Count > 0)
                {
                    return Ok(books);
                }
                else
                    return NotFound("No reservations at the moment");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GetAvailableBooks")]
        public IActionResult GetAvailableBooks()
        {
            try
            {
                var books = _context.Books.Where(b => b.Reservation == null).ToList();
                if (books.Count > 0)
                {
                    return Ok(books);
                }
                else
                    return NotFound("No available books at the moment");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}

using API.Models;
using API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : Controller
    {

        private readonly BookContext _context;

        public BookController(BookContext context)
        {
            _context = context;
        }

        [HttpPost("AddBook")]
        public IActionResult AddBook(string Title, string Author)
        {
            try
            {
                if (string.IsNullOrEmpty(Title) || string.IsNullOrEmpty(Author))
                {
                    return BadRequest("Title and Author are required.");
                }

                Book bookToAdd = new Book();
                bookToAdd.Title = Title;
                bookToAdd.Author = Author;
                bookToAdd.Id = Guid.NewGuid().ToString();
                bookToAdd.Reservation = null;

                _context.Books.Add(bookToAdd);
                _context.SaveChanges();
                return Ok(bookToAdd);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet("GetBook")]
        public IActionResult GetBook(string ID)
        {
            try
            {
                if (string.IsNullOrEmpty(ID))
                {
                    return BadRequest("ID is required.");
                }

                Book? foundBook = _context.Books.FirstOrDefault(b => b.Id == ID);

                if(foundBook != null)
                {
                    return Ok(foundBook);
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


        //For PUT operation lets only update Title and Author with BookUpdate for easy Swagger usage.
        [HttpPut("UpdateBook")]
        public IActionResult UpdateBook(string ID, BookUpdate book)
        {
            try
            {
                if (string.IsNullOrEmpty(ID) || book == null)
                {
                    return BadRequest("ID and Book is required.");
                }

                Book? foundBook = _context.Books.FirstOrDefault(b => b.Id == ID);

                if (foundBook != null)
                {
                    //Update the book
                    foundBook.Author = book.Author;
                    foundBook.Title = book.Title;

                    _context.SaveChanges();
                    return Ok(foundBook);

                    //No update for ID or reservation
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

        [HttpDelete("DeleteBook")]
        public IActionResult DeleteBook(string ID)
        {
            try
            {
                if (string.IsNullOrEmpty(ID))
                {
                    return BadRequest("ID is required.");
                }

                Book? foundBook = _context.Books.FirstOrDefault(b => b.Id == ID);

                if (foundBook != null)
                {
                    //Remove the book
                    _context.Books.Remove(foundBook);

                    _context.SaveChanges();
                    return Ok(foundBook);
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



    }
}

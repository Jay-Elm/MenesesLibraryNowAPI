using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Net.WebRequestMethods;

namespace MenesesLibraryNowAPI.Models.Controllers
{
    [Route("api/v1/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private static List<Book> books = new List<Book>
        {
            new Book
            {
                Id = 1,
                Title = "Diary of a Wimpy Kid",
                Author = "Jeff Kinney",
                Genre = "Children's Fiction",
                Available = true,
                PublishedYear = 2007
            },
            new Book
            {
                Id = 2,
                Title = "The Subtle Art of Not Giving a Fuck",
                Author = "Mark Manson",
                Genre = "Self-Help",
                Available = true,
                PublishedYear = 2016
            }
        };

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(new
            {
                status = "success",
                data = books,
                message = "Books Retreived."
            });
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var book = books.FirstOrDefault(x => x.Id == id);
            if (book == null)
                return NotFound(new
                {
                    status = "error",
                    data = (object?)null,
                    message = "Book Not Found."
                });
                return Ok(new
                {
                    status = "success",
                    data = books,
                    message = "Book Retreived."
                });
        }

        [HttpPost]
        public IActionResult Create([FromBody] Book newBook)
        {
            newBook.Id = books.Count + 1;
            books.Add(newBook);
            return CreatedAtAction(nameof(GetById),
                new { id = newBook.Id },
                new { status = "success",
                data = newBook,
                message = "Book Created."});
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id,
            [FromBody] Book updateBook)
        {
            var book = books.FirstOrDefault(x =>x.Id == id);
            if (book == null)
                return NotFound(new
                {
                    status = "error",
                    data = (object?)null,
                    message = "Book Not Found."
                });
            book.Title = updateBook.Title;
            book.Author = updateBook.Author;
            book.Genre = updateBook.Genre;
            book.Available  = updateBook.Available;
            book.PublishedYear  = updateBook.PublishedYear;

            return Ok(new
            {
                status = "success",
                data = books,
                message = "Book Updated."
            });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var book = books.FirstOrDefault(x =>x.Id == id);
            if (book == null)
                return NotFound(new
                {
                    status = "error",
                    data = (object?)null,
                    message = "Book Not Found."
                });
            books.Remove(book);
            return Ok(new
            {
                status = "success",
                data = (object?)null,
                message = "Book Deleted."
            });
        }
    }
}
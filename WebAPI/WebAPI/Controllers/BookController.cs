using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Entities;
using WebAPI.Models;
using WebAPI.Repositories;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly AuthenticationContext _context;
        private readonly IBookRepository repository;
        private readonly ILogger<BookController> logger;
        public BookController( IBookRepository repository, ILogger<BookController> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        // GET: api/Book
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<Book>> GetBooks(string title = null)
        {
            var Book = await repository.GetBooksAsync();

            var books = (await repository.GetBooksAsync())
             .Select(item => item.AsDto());

            if (!string.IsNullOrWhiteSpace(title))
            {
                books = books.Where(book => book.Title.Contains(title, StringComparison.OrdinalIgnoreCase));
            }

            logger.LogInformation($"{DateTime.UtcNow.ToString("hh:mm:ss")}: Retrieved {books.Count()} books");

            return Book;
        }

        // GET: api/Book/5
        [HttpGet("{id}")]
        //[Authorize(Roles = "Customer")]
        //[Route("ForCustomer")]
        public IList<Book> GetBook(int id)
        {
            IList<Book> listPaymentDestails;
            try
            {
                listPaymentDestails = repository.GetBookAsync(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return listPaymentDestails;
        }

        // POST: api/Book
        [HttpPost]
        //[Authorize(Roles = "Admin")]
        //[Route("ForAdmin")]
        public async Task<ActionResult<Book>> PostBook(Book Book)
        {
            _context.Books.Add(Book);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBook", new { id = Book.Id }, Book);
        }

    }
}
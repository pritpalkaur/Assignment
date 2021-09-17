using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public BookController(AuthenticationContext context, IBookRepository repository)
        {
            this.repository = repository;
            _context = context;
        }

        // GET: api/Book
        [HttpGet]
        public async Task<IEnumerable<Book>> GetBooks()
        {
            var Book = await repository.GetBookAsync();
            return Book;
        }


        // PUT: api/Book/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, Book Book)
        {
            if (id != Book.Id)
            {
                return BadRequest();
            }
            _context.Entry(Book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
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

        // DELETE: api/Book/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Book>> DeleteBook(int id)
        {
            var Book = await _context.Books.FindAsync(id);
            if (Book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(Book);
            await _context.SaveChangesAsync();

            return Book;
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Repositories
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetBookAsync();
        IList<Book> GetBookAsync(int Id);
     }
}

using WebAPI.Dtos;
using WebAPI.Entities;

namespace WebAPI
{
    public static class Extensions
    {
        public static BookDto AsDto(this Book book)
        {
            return new BookDto(){Id= book.Id, Title=book.Title, year=book.Year,Description= book.Description };
        }

    }
}

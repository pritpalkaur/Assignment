using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Controllers;
using WebAPI.Dtos;
using WebAPI.Entities;
using WebAPI.Repositories;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using WebAPI.Models;

namespace TestProject1
{
    public class ItemsControllerTests
    {
        private readonly Mock<IBookRepository> repositoryStub = new Mock<IBookRepository>();
        private readonly Mock<ILogger<BookController>> loggerStub = new Mock<ILogger<BookController>>();
        private readonly Random rand = new();
         [Fact]
        public void GetBooksFromController()
        {
            List<Book> bookList = new List<Book>();
            
            var bookDTO = new Book()
            {
                Id = 1,
                Title = "Math",
                Year = 2021,
                Description = "This is Math"
            };
            bookList.Add(bookDTO);
            repositoryStub.Setup(p => p.GetBookAsync(1)).Returns(bookList);
            BookController book = new BookController(repositoryStub.Object,loggerStub.Object);
            var result = book.GetBook(1);
            Assert.True(bookList.Equals(result));
        }
        [Fact]
        public async void GetBooksFromRepo()
        {
            //List<Book> bookList = new List<Book>();

            ////var bookDTO = new Book()
            ////{
            ////    Id = 1,
            ////    Title = "Math",
            ////    Year = 2021,
            ////    Description = "This is Math"
            ////};
            ////bookList.Add();
            //await repositoryStub.Setup(p => p.GetBooksAsync()).ReturnsAsync();
            //BookController book = new BookController(repositoryStub.Object, loggerStub.Object);
            //var result = book.GetBook(1);
            //Assert.True(bookList.Equals(result));
        }
    }
}

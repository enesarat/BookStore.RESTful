using AutoMapper;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public DeleteBookCommandTest(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture.Context;
        }

        [Theory]
        [InlineData(-5)]
        [InlineData(99999)]
        public void WhenGivenBookIdIsNotExist_InvalidOperationException_ShouldBeReturn(int bookId)
        {
            // Arrange (preparation)
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = bookId;

            // Act & Assert (run and confirmation)
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Silinecek kitap bulunamamıştır!");
        }

        [Theory]
        [InlineData(3)]
        [InlineData(1)]
        public void WhenValidInputsAreGiven_Book_ShouldBeDeleted(int bookId)
        {
            // Arrange (preparation)
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = bookId;

            // Act
            FluentActions
               .Invoking(() => command.Handle()).Invoke();
             
            // Assert 
            var book = _context.Books.SingleOrDefault(x => x.Id == bookId);
            book.Should().BeNull();
        }
    }
}

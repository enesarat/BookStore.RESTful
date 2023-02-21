using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;
using WebApi.DbOperations;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public DeleteGenreCommandTest(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture.Context;
        }

        [Theory]
        [InlineData(-5)]
        [InlineData(99999)]
        public void WhenGivenBookIdIsNotExist_InvalidOperationException_ShouldBeReturn(int bookId)
        {
            // Arrange (preparation)
            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.GenreId = bookId;

            // Act & Assert (run and confirmation)
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap türü bulunamadı.");
        }

        [Theory]
        [InlineData(3)]
        [InlineData(1)]
        public void WhenValidInputsAreGiven_Genre_ShouldBeDeleted(int genreId)
        {
            // Arrange (preparation)
            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.GenreId = genreId;

            // Act
            FluentActions
               .Invoking(() => command.Handle()).Invoke();

            // Assert 
            var genre = _context.Genres.SingleOrDefault(x => x.Id == genreId);
            genre.Should().BeNull();
        }
    }
}

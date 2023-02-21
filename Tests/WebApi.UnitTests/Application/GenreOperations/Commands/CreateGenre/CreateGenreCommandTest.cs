using AutoMapper;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public CreateGenreCommandTest(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture.Context;
        }
        [Fact]
        public void WhenAlreadyExistGenreNameIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // Arrange (preparation)
            var genre = new Genre() { Name = "Test_WhenAlreadyExistGenreNameIsGiven_InvalidOperationException_ShouldBeReturn"};
            _context.Genres.Add(genre);
            _context.SaveChanges();

            CreateGenreCommand command = new CreateGenreCommand(_context);
            command.Model = new CreateGenreViewModel() { Name = genre.Name };

            // Act & Assert (run and confirmation)
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Bu kitap türü zaten mevcut.");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Genre_ShouldBeCreated()
        {
            // Arrange (preparation)
            CreateGenreCommand command = new CreateGenreCommand(_context);
            CreateGenreViewModel model = new CreateGenreViewModel() { Name = "Underground Lliterature" };
            command.Model = model;

            // Act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            // Assert
            var genre = _context.Genres.SingleOrDefault(x => x.Name == model.Name);

            genre.Should().NotBeNull();
            genre.Name.Should().Be(model.Name);
        }
    }
}

using AutoMapper;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;


        public CreateAuthorCommandTest(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture.Context;
            _mapper = commonTestFixture.Mapper;
        }
        [Fact]
        public void WhenAlreadyExistAuthorIdentityIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // Arrange (preparation)
            var author = new Author() { Name = "Test_WhenAlreadyExistAuthorIdentityIsGiven_InvalidOperationException_ShouldBeReturn", Surname = "Test_Surname", BirthDate = DateTime.Now.AddYears(-2) };
            _context.Authors.Add(author);
            _context.SaveChanges();

            CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);
            command.Model = new CreateAuthorViewModel() { Name = author.Name, Surname = author.Surname, BirthDate = author.BirthDate };

            // Act & Assert (run and confirmation)
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Aynı kimliğe sahip yazar bulunmaktadır.");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Author_ShouldBeCreated()
        {
            // Arrange (preparation)
            CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);
            CreateAuthorViewModel model = new CreateAuthorViewModel() { Name = "Jack", Surname = "Sparrow", BirthDate = DateTime.Now.AddYears(-2) };
            command.Model = model;

            // Act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            // Assert
            var author = _context.Authors.SingleOrDefault(x => x.Name == model.Name && x.Surname == model.Surname);

            author.Should().NotBeNull();
            author.Name.Should().Be(model.Name);
            author.Surname.Should().Be(model.Surname);
            author.BirthDate.Should().Be(model.BirthDate);
        }
    }
}

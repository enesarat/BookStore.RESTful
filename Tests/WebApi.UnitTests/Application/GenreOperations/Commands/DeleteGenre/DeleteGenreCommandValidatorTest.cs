using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommandValidatorTest : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(-999)]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(1)]
        public void WhenLowerThanAndEqualToZeroIdIsGiven_Validator_ShouldBeReturnError(int genreId)
        {
            // Arrange
            DeleteGenreCommand command = new DeleteGenreCommand(null);
            command.GenreId = genreId;

            // Act
            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
            var result = validator.Validate(command);


            // Assert
            result.Errors.Count.Should().NotBe(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            // Arrange
            DeleteGenreCommand command = new DeleteGenreCommand(null);
            command.GenreId = 55;

            // Act
            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
            var result = validator.Validate(command);

            // Assert
            result.Errors.Count.Should().Be(0);
        }
    }
}

using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandValidatorTest : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(2, "Underground Literature")]
        [InlineData(3, "")]
        [InlineData(4, "Soc")]

        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int genreId, string genreName)
        {
            // Arrange
            UpdateGenreCommand command = new UpdateGenreCommand(null);
            command.GenreId = genreId;
            command.Model = new UpdateGenreViewModel()
            {
                Name= genreName,
            };

            // Act
            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            var result = validator.Validate(command);

            // Assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            // Arrange
            UpdateGenreCommand command = new UpdateGenreCommand(null);
            command.GenreId = 1;
            command.Model = new UpdateGenreViewModel()
            {
                Name = "Underground Literature",
            };

            // Act
            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            var result = validator.Validate(command);

            // Assert
            result.Errors.Count.Should().Be(0);
        }
    }
}

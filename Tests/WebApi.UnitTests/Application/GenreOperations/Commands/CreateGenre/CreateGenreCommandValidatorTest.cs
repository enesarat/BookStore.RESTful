using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandValidatorTest : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("UderGround Literature")]
        [InlineData("")]
        [InlineData("Und")]

        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string genreName)
        {
            // Arrange
            CreateGenreCommand command = new CreateGenreCommand(null);
            command.Model = new CreateGenreViewModel()
            {
                Name = genreName,
            };

            // Act
            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            var result = validator.Validate(command);

            // Assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            // Arrange
            CreateGenreCommand command = new CreateGenreCommand(null);
            command.Model = new CreateGenreViewModel()
            {
                Name = "Underground Literature"
            };

            // Act
            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            var result = validator.Validate(command);

            // Assert
            result.Errors.Count.Should().Be(0);
        }
    
    }
}

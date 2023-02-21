using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using WebApi.Application.GenreOperations.Queries.GetGenresDetail;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailQueryValidatorTest : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(-999)]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(1)]
        public void WhenLowerThanAndEqualToZeroIdIsGiven_Validator_ShouldBeReturnError(int genreId)
        {
            // Arrange
            GetGenreDetailQuery query = new GetGenreDetailQuery(null, null);
            query.GenreId = genreId;

            // Act
            GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
            var result = validator.Validate(query);


            // Assert
            result.Errors.Count.Should().NotBe(0);
        }


        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            // Arrange
            GetGenreDetailQuery query = new GetGenreDetailQuery(null, null);
            query.GenreId = 2;

            // Act
            GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
            var result = validator.Validate(query);

            // Assert
            result.Errors.Count.Should().Be(0);
        }
    }
}

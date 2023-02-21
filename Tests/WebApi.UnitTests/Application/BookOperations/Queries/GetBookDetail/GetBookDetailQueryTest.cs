using AutoMapper;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using WebApi.DbOperations;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.BookOperations.Queries.GetBookDetail
{
    public class GetBookDetailQueryTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetBookDetailQueryTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Theory]
        [InlineData(1)]
        [InlineData(55)]
        [InlineData(9999)]
        public void WhenBookIdIsNotFound_InvalidOperationException_ShouldReturnError(int id)
        {
            GetBookByIdQuery query = new GetBookByIdQuery(_context, _mapper);
            query.BookId = id;
            FluentActions.Invoking(() => query.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap bulunamadı!");

        }
    }
}

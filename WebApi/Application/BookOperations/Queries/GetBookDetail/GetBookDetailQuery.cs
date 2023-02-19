using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DbOperations;

namespace WebApi.Application.BookOperations.Queries.GetBookDetail
{
    public class GetBookByIdQuery
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public int BookId { get; set; }

        public GetBookByIdQuery(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public BookDetailViewModel Handle()
        {
            var book = _dbContext.Books.Include(x => x.Genre).Where(Book => Book.Id == BookId).SingleOrDefault();
            if (book is null)
                throw new InvalidOperationException("Kitap bulunamadı!");
            BookDetailViewModel viewModel = _mapper.Map<BookDetailViewModel>(book);
            return viewModel;
        }

    }
    public class BookDetailViewModel
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string Genre { get; set; }
        public string PublishDate { get; set; }
    }
}

using WebApi.DbOperations;

namespace WebApi.BookOperations.GetBookDetail
{
    public class GetBookByIdQuery
    {
        private readonly BookStoreDbContext _dbContext;
        public int BookId { get; set; }

        public GetBookByIdQuery(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public BookDetailViewModel Handle()
        {
            var book = _dbContext.Books.Where(Book=> Book.Id == BookId).SingleOrDefault();
            if (book is null)
                throw new InvalidOperationException("Kitap bulunamadı!");
            BookDetailViewModel viewModel = new BookDetailViewModel();
            viewModel.Title = book.Title;
            viewModel.PageCount= book.PageCount;
            viewModel.PublishDate = book.PublishDate.Date.ToString("dd/mm/yyyy");
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

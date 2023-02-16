using WebApi.Common;
using WebApi.DbOperations;

namespace WebApi.BookOperations.GetBooks
{
    public class GetBooksQuery
    {
        private readonly BookStoreDbContext _dbContext;
        public GetBooksQuery(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<BooksViewModel> Handle() 
        {
            var bookList = _dbContext.Books.OrderBy(x => x.Id).ToList<Book>();
            List<BooksViewModel> viewModel = new List<BooksViewModel>();
            foreach (var book in bookList)
            {
                viewModel.Add(new BooksViewModel()
                {
                    Title= book.Title,
                    Genre=((GenreEnum)book.GenreId).ToString(),
                    PublishDate= book.PublishDate.Date.ToString("dd/mm/yyyy"),
                    PageCount= book.PageCount
                });
            }
            return viewModel;
        }
    }

    public class BooksViewModel
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
        public string Genre { get; set; }
    }
}

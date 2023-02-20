using Microsoft.EntityFrameworkCore;
using WebApi.DbOperations;

namespace WebApi.Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommand
    {
        private readonly IBookStoreDbContext _dbContext;
        public int BookId { get; set; }
        public UpdateBookViewModel Model { get; set; }

        public UpdateBookCommand(IBookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.Id == BookId);
            if (book is null)
                throw new InvalidOperationException("Güncellenecek kitap bulunamadı");
            book.Title = Model.Title != default ? Model.Title : book.Title;
            book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId;
            book.PageCount = Model.PageCount != default ? Model.PageCount : book.PageCount;
            book.PublishDate = Model.publishDate != default ? Model.publishDate : book.PublishDate;
            _dbContext.SaveChanges();
        }
    }
    public class UpdateBookViewModel
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public DateTime publishDate { get; set; }
        public int GenreId { get; set; }
    }
}

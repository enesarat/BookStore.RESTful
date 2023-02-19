using WebApi.DbOperations;

namespace WebApi.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommand
    {
        private readonly BookStoreDbContext _context;
        public int AuthorId { get; set; }
        public DeleteAuthorCommand(BookStoreDbContext context)
        {
            _context = context;
        }
        public void Handle()
        {
            var author = _context.Authors.SingleOrDefault(x => x.AuthorId == AuthorId);
            if (author is null)
                throw new InvalidOperationException("Yazar bulunamadı");
            var bookOfAuthor = _context.Books.Where(x => x.AuthorId == AuthorId).Any();
            if (bookOfAuthor)
                throw new InvalidProgramException("Yazarın kayıtlı kitabı bulunduğu için işlem gerçekleştirilemedi");

            _context.Authors.Remove(author);
            _context.SaveChanges();
        }
    }
}

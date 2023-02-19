using AutoMapper;
using WebApi.DbOperations;

namespace WebApi.Application.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQuery
    {
        public int AuthorId { get; set; }
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetAuthorDetailQuery(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public AuthorDetailViewModel Handle()
        {
            var author = _context.Authors.SingleOrDefault(x => x.AuthorId == AuthorId);
            if (author is null)
                throw new InvalidOperationException("Yazar kaydı bulunamadı");
            AuthorDetailViewModel viewModel = _mapper.Map<AuthorDetailViewModel>(author);
            return viewModel;
        }
    }
    public class AuthorDetailViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }

    }
}

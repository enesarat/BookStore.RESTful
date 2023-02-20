using AutoMapper;
using WebApi.DbOperations;

namespace WebApi.Application.AuthorOperations.Queries.GetAuthors
{
    public class GetAuthorsQuery
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetAuthorsQuery(IBookStoreDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        public List<GetAuthorModel> Handle()
        {
            var authorList = _context.Authors.OrderBy(x => x.AuthorId);
            List<GetAuthorModel> viewModelList = _mapper.Map<List<GetAuthorModel>>(authorList);
            
            return viewModelList;
        }
    }
    public class GetAuthorModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
    }
}

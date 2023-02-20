using AutoMapper;
using WebApi.DbOperations;

namespace WebApi.Application.GenreOperations.Queries.GetGenresDetail
{
    public class GetGenreDetailQuery
    {
        public readonly IBookStoreDbContext _context;
        public readonly IMapper _mapper;
        public int GenreId { get; set; }
        public GetGenreDetailQuery(IBookStoreDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        public GenreDetailViewModel Handle()
        {
            var genres = _context.Genres.SingleOrDefault(x => x.IsActive && x.Id == GenreId);
            if (genres is null)
            {
                throw new InvalidOperationException("Kitap türü bulunamadı");
            }
            GenreDetailViewModel returnObj = _mapper.Map<GenreDetailViewModel>(genres);
            return returnObj;
        }
    }
    public class GenreDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}

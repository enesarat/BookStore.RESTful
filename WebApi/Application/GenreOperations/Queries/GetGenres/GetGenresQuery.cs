using AutoMapper;
using WebApi.Application.BookOperations.Queries.GetBooks;
using WebApi.Common;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.GenreOperations.Queries.GetGenres
{
    public class GetGenresQuery
    {
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetGenresQuery(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<GenresViewModel> Handle()
        {
            var genreList = _dbContext.Genres.Where(x=>x.IsActive).OrderBy(x => x.Id);
            List<GenresViewModel> returnObj = _mapper.Map<List<GenresViewModel>>(genreList);
            return returnObj;
        }
    }
    public class GenresViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}

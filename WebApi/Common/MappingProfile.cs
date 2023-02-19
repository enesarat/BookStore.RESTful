using AutoMapper;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using WebApi.Application.AuthorOperations.Queries.GetAuthors;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using WebApi.Application.BookOperations.Queries.GetBooks;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using WebApi.Application.GenreOperations.Queries.GetGenresDetail;
using WebApi.Entities;

namespace WebApi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookViewModel, Book>();
            CreateMap<Book, BookDetailViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name));
            CreateMap<Book, BooksViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name));
            CreateMap<Genre, GenresViewModel>();
            CreateMap<Genre, GenreDetailViewModel>();
            CreateMap<CreateAuthorViewModel, Author>();
            CreateMap<Author, AuthorDetailViewModel>();
            CreateMap<Author, GetAuthorModel>();
        }
    }
}

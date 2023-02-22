using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using WebApi.Application.BookOperations.Queries.GetBooks;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using WebApi.Application.GenreOperations.Queries.GetGenresDetail;
using WebApi.DbOperations;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("[controller]s")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GenreController(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Here, we retreive all genres data from database context via GetGenre endpoint.
        [HttpGet]
        public IActionResult GetGenres()
        {
            GetGenresQuery query = new GetGenresQuery(_context, _mapper);
            var result = query.Handle();
            return Ok(result);
        }

        // Here, we retreive the genre data according to given id info from database context via GetById endpoint.
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            GetGenreDetailQuery query = new GetGenreDetailQuery(_context, _mapper);
            query.GenreId = id;
            GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
            validator.ValidateAndThrow(query);
            var result = query.Handle();

            return Ok(result);
        }

        // Here, we create genre data according to incoming genre informations into database context via AddGenre endpoint.
        [HttpPost]
        public IActionResult AddGenre([FromBody] CreateGenreViewModel newBook)
        {
            CreateGenreCommand createGenreCommand = new CreateGenreCommand(_context);

            createGenreCommand.Model = newBook;
            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            validator.ValidateAndThrow(createGenreCommand);
            createGenreCommand.Handle();

            return Ok();
        }

        // Here, we update the genre data according to related genre informations which exist on Id to database context via UpdateGenre endpoint.
        [HttpPut("{id}")]
        public IActionResult UpdateGenre(int id, [FromBody] UpdateGenreViewModel updatedGenre)
        {
            UpdateGenreCommand updateGenreCommand = new UpdateGenreCommand(_context);

            updateGenreCommand.GenreId = id;
            updateGenreCommand.Model = updatedGenre;
            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            validator.ValidateAndThrow(updateGenreCommand);
            updateGenreCommand.Handle();

            return Ok();

        }

        // Here, we delete the genre data according to given id info from database context via DeleteGenre endpoint.
        [HttpDelete("{id}")]
        public IActionResult DeleteGenre(int id)
        {

            DeleteGenreCommand deleteGenreCommand = new DeleteGenreCommand(_context);
            deleteGenreCommand.GenreId = id;
            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
            validator.ValidateAndThrow(deleteGenreCommand);
            deleteGenreCommand.Handle();

            return Ok();
        }
    }
}

using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.Application.AuthorOperations.Commands.DeleteAuthor;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using WebApi.Application.AuthorOperations.Queries.GetAuthors;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using WebApi.Application.BookOperations.Queries.GetBooks;
using WebApi.DbOperations;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("[controller]s")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public AuthorController(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Here, we retreive all author data from database context via GetAuthors endpoint.
        [HttpGet]
        public IActionResult GetAuthors()
        {
            GetAuthorsQuery query = new GetAuthorsQuery(_context, _mapper);
            var result = query.Handle();
            return Ok(result);
        }

        // Here, we retreive the author data according to given id info from database context via GetById endpoint.
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            AuthorDetailViewModel result;

            GetAuthorDetailQuery query = new GetAuthorDetailQuery(_context, _mapper);
            query.AuthorId = id;
            GetAuthorDetailQueryValidator validator = new GetAuthorDetailQueryValidator();
            validator.ValidateAndThrow(query);
            result = query.Handle();

            return Ok(result);
        }

        // Here, we create author data according to incoming author informations into database context via AddAuthor endpoint.
        [HttpPost]
        public IActionResult AddAuthor([FromBody] CreateAuthorViewModel newAuthor)
        {
            CreateAuthorCommand createAuthorCommand = new CreateAuthorCommand(_context, _mapper);

            createAuthorCommand.Model = newAuthor;
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            validator.ValidateAndThrow(createAuthorCommand);
            createAuthorCommand.Handle();

            return Ok();
        }

        // Here, we update the author data according to related author informations which exist on Id to database context via UpdateAuthor endpoint.
        [HttpPut("{id}")]
        public IActionResult UpdateAuthor(int id, [FromBody] UpdateAuthorViewModel updatedAuthor)
        {
            UpdateAuthorCommand updateAuthorCommand = new UpdateAuthorCommand(_context);

            updateAuthorCommand.AuthorId = id;
            updateAuthorCommand.Model = updatedAuthor;
            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            validator.ValidateAndThrow(updateAuthorCommand);
            updateAuthorCommand.Handle();

            return Ok();
        }

        // Here, we delete the author data according to given id info from database context via DeleteAuthor endpoint.
        [HttpDelete("{id}")]
        public IActionResult DeleteAuthor(int id)
        {

            DeleteAuthorCommand deleteAuthorCommand = new DeleteAuthorCommand(_context);
            deleteAuthorCommand.AuthorId = id;
            DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
            validator.ValidateAndThrow(deleteAuthorCommand);
            deleteAuthorCommand.Handle();

            return Ok();
        }
    }
}

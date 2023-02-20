using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.DbOperations;
using FluentValidation.Results;
using FluentValidation;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using WebApi.Application.BookOperations.Queries.GetBooks;
using AutoMapper;

namespace WebApi.AddControllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public BookController(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Here, we retreive all books data from database context via GetBook endpoint.
        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context,_mapper);
            var result = query.Handle();
            return Ok(result);
        }

        // Here, we retreive the book data according to given id info from database context via GetById endpoint.
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            BookDetailViewModel result;

            GetBookByIdQuery query = new GetBookByIdQuery(_context,_mapper);
            query.BookId = id;
            GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
            validator.ValidateAndThrow(query);
            result = query.Handle();

            return Ok(result);
        }

        // Here, we create book data according to incoming book informations into database context via AddBook endpoint.
        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookViewModel newBook)
        {
            CreateBookCommand createBookCommand = new CreateBookCommand(_context,_mapper);

            createBookCommand.Model = newBook;
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            validator.ValidateAndThrow(createBookCommand);
            createBookCommand.Handle();

            return Ok();
        }

        // Here, we update the book data according to related book informations which exist on Id to database context via UpdateBook endpoint.
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookViewModel updatedBook)
        {
            UpdateBookCommand updateBookCommand = new UpdateBookCommand(_context);

            updateBookCommand.BookId = id;
            updateBookCommand.Model = updatedBook;
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            validator.ValidateAndThrow(updateBookCommand);
            updateBookCommand.Handle();

            return Ok();

        }

        // Here, we delete the book data according to given id info from database context via DeleteBook endpoint.
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {

            DeleteBookCommand deleteBookCommand = new DeleteBookCommand(_context);
            deleteBookCommand.BookId = id;
            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            validator.ValidateAndThrow(deleteBookCommand);
            deleteBookCommand.Handle();

            return Ok();
        }
    }
}
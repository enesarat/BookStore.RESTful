﻿using AutoMapper;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommand
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateAuthorViewModel Model { get; set; }
        public CreateAuthorCommand(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public void Handle()
        {
            var author = _context.Authors.FirstOrDefault(x => x.Name == Model.Name && x.Surname == Model.Surname);
            if (author is not null)
            {
                throw new InvalidOperationException("Aynı kimliğe sahip yazar bulunmaktadır");
            }
            author = _mapper.Map<Author>(Model);
            _context.Authors.Add(author);
            _context.SaveChanges();
        }

    }
    public class CreateAuthorViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
    }
}

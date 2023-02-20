using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Common;
using WebApi.DbOperations;

namespace WebApi.UnitTests.TestSetup
{
    public class CommonTestFixture
    {
        public BookStoreDbContext Context { get; set; }
        public IMapper Mapper { get; set; }
        public CommonTestFixture()
        {
            var operations = new DbContextOptionsBuilder<BookStoreDbContext>().UseInMemoryDatabase(databaseName: "BookStoreTestDB").Options;
            Context = new BookStoreDbContext(operations);

            Context.Database.EnsureCreated();
            Context.AddGenres();
            Context.AddAuthors();
            Context.AddBooks();

            Context.SaveChanges();

            Mapper = new MapperConfiguration(cfg => { cfg.AddProfile<MappingProfile>(); }).CreateMapper();
        }
    }
}

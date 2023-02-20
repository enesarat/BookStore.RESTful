using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.UnitTests.TestSetup
{
    public static class Books
    {
        public static void AddBooks(this BookStoreDbContext context)
        {
            context.Books.AddRange(
                    new Book{Title = "Lean Startup",GenreId = 1,AuthorId = 1,PageCount = 200,PublishDate = new DateTime(2001, 06, 12)},
                    new Book{Title = "Herland",GenreId = 2,AuthorId = 2,PageCount = 250,PublishDate = new DateTime(2010, 05, 23)},
                    new Book{Title = "Dune",GenreId = 2,AuthorId = 3,PageCount = 540,PublishDate = new DateTime(2001, 12, 21)},
                    new Book{Title = "28.Harf",GenreId = 2,AuthorId = 4,PageCount = 2207,PublishDate = new DateTime(2022, 07, 22)}
                );
        }
    }
}

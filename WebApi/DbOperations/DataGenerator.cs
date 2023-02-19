using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Entities;

namespace WebApi.DbOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if (context.Books.Any())
                {
                    return;
                }

                context.Genres.AddRange(
                    new Genre
                    {
                        Name = "Personal Growth"
                    },
                    new Genre
                    {
                        Name = "Science Fiction"
                    },
                    new Genre
                    {
                        Name = "Romance"
                    }
                );

                context.Books.AddRange(
                    new Book
                    {
                        Title = "Lean Startup",
                        GenreId = 1,
                        AuthorId = 0,
                        PageCount = 200,
                        PublishDate = new DateTime(2001, 06, 12)
                    },
                    new Book
                    {
                        Title = "Herland",
                        GenreId = 2,
                        AuthorId = 1,
                        PageCount = 250,
                        PublishDate = new DateTime(2010, 05, 23)
                    },
                    new Book
                    {
                        Title = "Dune",
                        GenreId = 2,
                        AuthorId = 2,
                        PageCount = 540,
                        PublishDate = new DateTime(2001, 12, 21)
                    },
                    new Book
                    {
                        Title = "28.Harf",
                        GenreId = 2,
                        AuthorId = 3,
                        PageCount = 2207,
                        PublishDate = new DateTime(2022, 07, 22)
                    }
                );

                context.AddRange(new Author
                {
                    Name = "Fyodor",
                    Surname = "Dostoyevski",
                    BirthDate = new DateTime(1821, 11, 11)
                },
                    new Author
                    {
                        Name = "Jean Paul",
                        Surname = "Sartre",
                        BirthDate = new DateTime(1905, 06, 21)
                    },
                    new Author
                    {
                        Name = "Albert",
                        Surname = "Camus",
                        BirthDate = new DateTime(1913, 11, 07)
                    },
                    new Author
                    {
                        Name = "Enes",
                        Surname = "Arat",
                        BirthDate = new DateTime(1999, 07, 07)
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
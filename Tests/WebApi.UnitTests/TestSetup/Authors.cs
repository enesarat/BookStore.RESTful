using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.UnitTests.TestSetup
{
    public static class Authors
    {
        public static void AddAuthors(this BookStoreDbContext context)
        {
            context.AddRange(
                    new Author{Name = "Fyodor",Surname = "Dostoyevski",BirthDate = new DateTime(1821, 11, 11)},
                    new Author{Name = "Jean Paul",Surname = "Sartre",BirthDate = new DateTime(1905, 06, 21)},
                    new Author{Name = "Albert",Surname = "Camus",BirthDate = new DateTime(1913, 11, 07)},
                    new Author{Name = "Enes",Surname = "Arat",BirthDate = new DateTime(1999, 07, 07)}
                );
        }
    }
}

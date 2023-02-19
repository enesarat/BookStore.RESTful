using FluentValidation;

namespace WebApi.Application.GenreOperations.Queries.GetGenresDetail
{
    public class GetGenreDetailQueryValidator : AbstractValidator<GetGenreDetailQuery>
    {
        public GetGenreDetailQueryValidator()
        {
            RuleFor(x => x.GenreId).GreaterThan(0);
        }
    }
}

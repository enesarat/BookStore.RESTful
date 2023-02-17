using FluentValidation;

namespace WebApi.BookOperations.GetBookDetail
{
    public class GetBookDetailValidator : AbstractValidator<GetBookByIdQuery>
    {
        public GetBookDetailValidator()
        {
            RuleFor(command => command.BookId).GreaterThan(0);
        }
    }
}

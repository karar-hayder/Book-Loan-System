
using FluentValidation;

public class CreateLoanRequestValidator : AbstractValidator<CreateLoanRequest>
{
    public CreateLoanRequestValidator()
    {
        RuleFor(x => x.UserId)
            .GreaterThan(-1).WithMessage("UserId must be greater than -1");

        RuleFor(x => x.BookId)
            .GreaterThan(-1).WithMessage("BookId must be greater than -1");
    }
}


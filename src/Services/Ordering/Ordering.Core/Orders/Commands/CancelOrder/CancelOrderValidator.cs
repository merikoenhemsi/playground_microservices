using FluentValidation;

namespace Ordering.Core.Orders.Commands.CancelOrder;

public class CancelOrderValidator:AbstractValidator<CancelOrderCommand>
{
    public CancelOrderValidator()
    {
        RuleFor(command => command.Id)
            .NotEmpty().WithMessage("{Id} is required.");

    }
}
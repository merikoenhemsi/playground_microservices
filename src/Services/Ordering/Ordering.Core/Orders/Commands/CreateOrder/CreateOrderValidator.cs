using FluentValidation;

namespace Ordering.Core.Orders.Commands.CreateOrder;

public class CreateOrderValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderValidator()
    {
        RuleFor(command => command.OrderItems).Must(orderItems => orderItems.Any()).WithMessage("There is no product");
    }
   
}

using FluentValidation.TestHelper;
using Ordering.Core.Entities;
using Ordering.Core.Orders.Commands.CreateOrder;
using Xunit;

namespace Ordering.UnitTests.Validators
{
    public class CreateOrderValidatorTest
    {
        private readonly CreateOrderValidator _validator;
        public CreateOrderValidatorTest()
        {
            _validator=new CreateOrderValidator();
        }

        [Fact]
        public void FirstName_WhenLongerThanTwoCharacter_ShouldNotHaveValidationError()
        {
            var command = new CreateOrderCommand(5, "customerName", new List<OrderItem>());
           //TODO _validator.TestValidate(x => x.OrderItems, command).WithErrorMessage("There is no product");
        }
    }
}

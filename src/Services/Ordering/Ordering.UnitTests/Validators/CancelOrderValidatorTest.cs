using FluentValidation.TestHelper;
using Ordering.Core.Orders.Commands.CancelOrder;
using Xunit;

namespace Ordering.UnitTests.Validators;

public class CancelOrderValidatorTest 
{
    private readonly CancelOrderValidator _validator;

    public CancelOrderValidatorTest()
    {
        _validator = new CancelOrderValidator();
    }


    [Fact]
    public void CustomerFullName_WhenLongerThanTwoCharacter_ShouldNotHaveValidationError()
    {
      //TODO  _validator.ShouldNotHaveValidationErrorFor(x => x.Id, );
    }
}
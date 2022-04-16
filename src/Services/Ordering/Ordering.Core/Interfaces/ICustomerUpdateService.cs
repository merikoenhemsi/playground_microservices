using Ordering.Core.Orders.Models;

namespace Ordering.Core.Interfaces;

public interface ICustomerUpdateService
{
    void UpdateCustomerNameInOrders(UpdateCustomerModel request);
}
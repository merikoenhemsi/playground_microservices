using Ordering.Core.Orders.Models;

namespace Ordering.Core.Interfaces;

public interface ICustomerUpdateService
{
    Task UpdateCustomerNameInOrders(UpdateCustomerModel request);
}
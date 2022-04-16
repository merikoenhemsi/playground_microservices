using Ardalis.Specification;
using Ordering.Core.Entities;

namespace Ordering.Core.Specification;

public class OrdersByCustIdSpec:Specification<Order>
{
    public OrdersByCustIdSpec(int customerId)
    {
        Query.Where(o => o.CustomerId == customerId).Include(o => o.OrderItems);
    }
}
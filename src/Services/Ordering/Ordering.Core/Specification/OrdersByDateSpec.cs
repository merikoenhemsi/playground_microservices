using Ardalis.Specification;
using Ordering.Core.Entities;

namespace Ordering.Core.Specification;

public class OrdersByDateSpec:Specification<Order>
{
    public OrdersByDateSpec(DateTime startDate, DateTime endDate)
    {
        Query.Where(ord => ord.CreatedDate > startDate && ord.CreatedDate < endDate).Include(ord => ord.OrderItems);
    }
}
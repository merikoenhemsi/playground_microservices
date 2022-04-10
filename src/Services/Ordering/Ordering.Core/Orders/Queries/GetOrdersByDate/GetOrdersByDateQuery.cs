using MediatR;
using Ordering.Core.Entities;

namespace Ordering.Core.Orders.Queries.GetOrdersByDate;

public class GetOrdersByDateQuery : IRequest<List<Order>>
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; }

    public GetOrdersByDateQuery(DateTime? startDate, DateTime? endDate)
    {
        StartDate = startDate ?? throw new ArgumentNullException(nameof(startDate));
        EndDate = endDate ?? throw new ArgumentNullException(nameof(endDate));
    }
}
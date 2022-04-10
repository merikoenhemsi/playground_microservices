using Ordering.Core.Enums;

namespace Ordering.Core.Entities;

public class Order:BaseEntity
{
    public int CustomerId { get; set; }
    public string CustomerName { get; set; }
    public  List<OrderItem> OrderItems;
    public OrderStatus OrderStatus { get; set; }

    public void SetCancelledStatus()
    {
        if (OrderStatus == OrderStatus.Paid ||
            OrderStatus == OrderStatus.Shipped)
        {
            throw new Exception("You can't cancel the order");
        }

        OrderStatus = OrderStatus.Cancelled;
    }


}
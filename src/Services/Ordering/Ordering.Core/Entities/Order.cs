using Ordering.Core.Enums;

namespace Ordering.Core.Entities;

public class Order:BaseEntity
{
    public int? CustomerId { get; set; }
    public string CustomerName { get; set; }
    public  List<OrderItem> OrderItems { get; set; }
    public OrderStatus OrderStatus { get; set; }

    private Order()
    {
        OrderStatus = OrderStatus.Created;
    }

    public Order(int? customerId, string customerName, List<OrderItem> orderItems)
    {
        CustomerId = customerId;
        CustomerName = customerName;
        OrderItems = orderItems;
        OrderStatus = OrderStatus.Created;
    }

    public Order(int? customerId, string customerName, List<OrderItem> orderItems, OrderStatus orderStatus)
    {
        CustomerId = customerId;
        CustomerName = customerName;
        OrderItems = orderItems;
        OrderStatus = orderStatus;
    }

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
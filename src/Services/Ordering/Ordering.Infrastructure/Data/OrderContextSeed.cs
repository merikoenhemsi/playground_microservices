using Microsoft.EntityFrameworkCore;
using Ordering.Core.Entities;
using Ordering.Core.Enums;

namespace Ordering.Infrastructure.Data;

public class OrderContextSeed
{
    public static async Task SeedAsync(OrderContext orderContext)
    {
        if (!await orderContext.Orders.AnyAsync())
        {
            await orderContext.Orders.AddRangeAsync(GetPreconfiguredOrders());
            await orderContext.SaveChangesAsync();
        }
    }

    private static IEnumerable<Order> GetPreconfiguredOrders()
    {
        return new List<Order>
        {
            new Order(customerId: 123456, customerName: "meri", orderItems: new()
            {
                new OrderItem(productName: "mug", price: 50, count: 1, productId: 1)
            }, orderStatus: OrderStatus.Created)
        };
    }
}
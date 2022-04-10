namespace Ordering.Core.Entities;

public class OrderItem:BaseEntity
{
    public int MasterId { get; set; }
    public string ProductName { get; set;}
    public decimal Price { get; set; }
    public int Count { get; set; }
    public int ProductId { get;  set; }

    private OrderItem()
    {
        
    }

    public OrderItem( string productName, decimal price, int count, int productId)
    {
        ProductName = productName;
        Price = price;
        Count = count;
        ProductId = productId;
    }

}
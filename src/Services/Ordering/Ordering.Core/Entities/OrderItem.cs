namespace Ordering.Core.Entities;

public class OrderItem
{
    private string _productName;
    private string _pictureUrl;
    private decimal _price;
    private int _count;

    public int ProductId { get; private set; }
}
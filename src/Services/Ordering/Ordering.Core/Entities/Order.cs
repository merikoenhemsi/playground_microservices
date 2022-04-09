namespace Ordering.Core.Entities;

public class Order:BaseEntity
{

    private DateTime _orderDate;

    public int? GetBuyerId => _buyerId;
    private int? _buyerId;

    //public OrderStatus OrderStatus { get; private set; }
    //private int _orderStatusId;

    private string _description;

}
namespace Ordering.Core.Orders.Queries;

public class OrderVm
{
    public string CustomerName { get; set; }
    public List<string> ProductName { get; set; }
    public DateTime OrderDate { get; set; }
}
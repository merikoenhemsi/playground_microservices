using System.ComponentModel.DataAnnotations;


namespace Ordering.Api.Models;

public class CreateOrderModel
{
    public int? CustomerId { get; set; }
    public string CustomerName { get; set; }

    [Required]
    public List<OrderItemModel> OrderItems { get; set; }
}
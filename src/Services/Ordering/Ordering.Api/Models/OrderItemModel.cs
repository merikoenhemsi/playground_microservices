using System.ComponentModel.DataAnnotations;

namespace Ordering.Api.Models;

public class OrderItemModel
{
    [Required]
    public string ProductName { get; set; }
    [Required]
    public decimal Price { get; set; }
    [Required]
    public int Count { get; set; }
    [Required]
    public int ProductId { get; set; }

}
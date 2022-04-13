using System.ComponentModel.DataAnnotations;

namespace Customer.API.Models;

public class UpdateCustomerModel
{
    [Required]
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public string Gender { get; set; }
}
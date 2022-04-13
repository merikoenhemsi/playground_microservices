using System.ComponentModel.DataAnnotations;

namespace Customer.API.Models;

public class CreateCustomerModel
{
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public string Gender { get; set; }
}
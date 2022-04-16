using Microsoft.EntityFrameworkCore;

namespace Customer.API.Data;

public class CustomerContextSeed
{
    public static async Task SeedAsync(CustomerContext customerContext)
    {
        if (!await customerContext.Customers.AnyAsync())
        {
            await customerContext.Customers.AddRangeAsync(GetPreconfiguredCustomers());
            await customerContext.SaveChangesAsync();
        }
    }

    private static IEnumerable<Entities.Customer> GetPreconfiguredCustomers()
    {
        return new List<Entities.Customer>()
        {
            new ()
            {
                FirstName = "FirstName",
                LastName = "LastName",
                Email = "Email",
                Address = "Address",
                Gender = "Gender"
            }

        };
    }
}
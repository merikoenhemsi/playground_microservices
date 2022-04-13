namespace Customer.API.Repositories;

public class CustomerRepository:ICustomerRepository
{
    public Task<IReadOnlyList<Entities.Customer>> GetCustomersAsync()
    {
        var customers = new List<Entities.Customer>()
        {
            new Entities.Customer
            {
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                FirstName = "meri",
                LastName = "kh",
                Email = "a@gmail.com",
                Address = "istanbul",
                Gender = "female"
            }
        };
        return Task.FromResult <IReadOnlyList<Entities.Customer>> (customers);
    }


    public Task<Entities.Customer> GetCustomerAsync(int id)
    {
        var customer = new Entities.Customer
        {
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now,
            FirstName = "meri",
            LastName = "kh",
            Email = "a@gmail.com",
            Address = "istanbul",
            Gender = "female"
        };
        return Task.FromResult<Entities.Customer>(customer);
    }

    public Task<Entities.Customer> CreateCustomerAsync(Entities.Customer customer)
    {
        return Task.FromResult<Entities.Customer>(customer);
    }

    public Task<bool> UpdateCustomerAsync(Entities.Customer customer)
    {
        return Task.FromResult(true);
    }

    public Task<bool> DeleteCustomerAsync(int id)
    {
        return Task.FromResult(true);
    }
}
namespace Customer.API.Repositories;

public interface ICustomerRepository
{
    Task<IReadOnlyList<Entities.Customer>> GetCustomersAsync();
    Task<Entities.Customer> GetCustomerAsync(int id);
    Task<Entities.Customer> CreateCustomerAsync(Entities.Customer customer);
    Task<bool> UpdateCustomerAsync(Entities.Customer customer);
    Task<bool> DeleteCustomerAsync(int id);
}
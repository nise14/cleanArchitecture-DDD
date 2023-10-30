namespace Domain.Customers;

public interface ICustomerRepository
{
    ValueTask<Customer?> GetByIdAsync(CustomerId id);
    Task<bool> ExistsAsync(CustomerId id);
    ValueTask Add(Customer customer);
    void Update(Customer customer);
    void Delete(Customer customer);
    ValueTask<List<Customer>> GetAll();
}
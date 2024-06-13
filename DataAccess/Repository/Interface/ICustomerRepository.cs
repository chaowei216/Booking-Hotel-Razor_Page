using BusinessObject.Models;

namespace DataAccess.Repository.Interface
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAllCustomers();
        Task<Customer?> GetCustomerById(int id);
        Task<Customer> AddNewCustomer(Customer customer);
        bool DeleteCustomer(int id);
        Customer UpdateCustomer(Customer customer);
        Task<Customer?> GetCustomerByEmail(string email);
        Task<Customer?> GetCustomerByPhone(string phone);
    }
}

using BusinessObject.Models;

namespace BusinessLogic.Interfaces
{
    public interface ICustomerService
    {
        Task<Customer> CreateNewCustomer(Customer customer);
        Customer UpdateCustomer(Customer customer);
        bool DeleteCustomer(int id);
        Task<IEnumerable<Customer>> GetAllCustomers();
        Task<Customer?> GetCustomerByEmail(string email);
        Task<Customer?> GetCustomerByPhone(string phone);
    }
}

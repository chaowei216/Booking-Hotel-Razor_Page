using BusinessObject.Models;
using DataAccess.DAO;
using DataAccess.Repository.Interface;

namespace DataAccess.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        public async Task<Customer> AddNewCustomer(Customer customer)
        {
            return await CustomerDAO.Instance.AddCustomer(customer);
        }

        public bool DeleteCustomer(int id)
        {
            return CustomerDAO.Instance.DeleteCustomer(id);
        }

        public async Task<IEnumerable<Customer>> GetAllCustomers()
        {
            return await CustomerDAO.Instance.GetAll();
        }

        public async Task<Customer?> GetCustomerByEmail(string email)
        {
            return (await CustomerDAO.Instance.GetAll()).FirstOrDefault(p => p.EmailAddress == email);
        }

        public async Task<Customer?> GetCustomerById(int id)
        {
            return await CustomerDAO.Instance.GetCustomer(id);
        }

        public async Task<Customer?> GetCustomerByPhone(string phone)
        {
            return (await CustomerDAO.Instance.GetAll()).FirstOrDefault(p => p.Telephone == phone);
        }

        public Customer UpdateCustomer(Customer customer)
        {
            return CustomerDAO.Instance.UpdateCustomer(customer);
        }

        
    }
}

using BusinessLogic.Interfaces;
using BusinessObject.Models;
using DataAccess.Repository.Interface;

namespace BusinessLogic.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Customer> CreateNewCustomer(Customer customer)
        {
            return await _customerRepository.AddNewCustomer(customer);
        }

        public bool DeleteCustomer(int id)
        {
            return _customerRepository.DeleteCustomer(id);
        }

        public async Task<IEnumerable<Customer>> GetAllCustomers()
        {
            return await _customerRepository.GetAllCustomers();
        }

        public async Task<Customer?> GetCustomerByEmail(string email)
        {
            return await _customerRepository.GetCustomerByEmail(email);
        }

        public async Task<Customer?> GetCustomerByPhone(string phone)
        {
            return await _customerRepository.GetCustomerByPhone(phone);
        }

        public Customer UpdateCustomer(Customer customer)
        {
            return _customerRepository.UpdateCustomer(customer);
        }
    }
}

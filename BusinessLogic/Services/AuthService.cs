using AutoMapper;
using BusinessLogic.Interfaces;
using BusinessObject.Models;
using Common.DTO;
using Microsoft.Extensions.Configuration;

namespace BusinessLogic.Services
{
    public class AuthService : IAuthService
    {
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;

        public AuthService(ICustomerService customerService, IMapper mapper)
        {
            _customerService = customerService;
            _mapper = mapper;
        }

        public async Task<Customer?> CheckLogin(string email, string password)
        {
            var customer = await _customerService.GetCustomerByEmail(email);

            if (customer != null)
            {
                if (customer.Password == password)
                {
                    return customer;
                }

                return null;
            }

            return null;
        }

        public async Task<Customer?> Register(RegisterRequestDTO request)
        {
            var existingEmail = await _customerService.GetCustomerByEmail(request.EmailAddress);
            var existingPhone = await _customerService.GetCustomerByPhone(request.Telephone!);

            if (existingEmail != null ||  existingPhone != null)
            {
                return null;
            }

            var customer = _mapper.Map<Customer>(request);
            customer.CustomerStatus = 1;

            var registeredCustomer = await _customerService.CreateNewCustomer(customer);

            if (registeredCustomer != null)
            {
                return registeredCustomer;
            }

            return null;
        }
    }
}

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
        private readonly IConfiguration _config;

        public AuthService(ICustomerService customerService, IMapper mapper, IConfiguration config)
        {
            _customerService = customerService;
            _mapper = mapper;
            _config = config;
        }

        public async Task<Customer?> CheckLogin(string email, string password)
        {
            if (CheckAdminAccount(email, password) != null)
            {
                return CheckAdminAccount(email, password);
            } else
            {
                var customer = await _customerService.GetCustomerByEmail(email);

                if (customer != null)
                {
                    if (customer.Password == password)
                    {
                        return customer;
                    }
                }

                return null;
            }
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

        private Customer? CheckAdminAccount(string email, string password)
        {
            var adminEmail = _config["AdminAccount:Email"];
            var adminPass = _config["AdminAccount:Password"];

            if (adminEmail != null || adminPass != null)
            {
                if (adminEmail == email && adminPass == password)
                {
                    return new Customer()
                    {
                        EmailAddress = email,
                        Password = password,
                        CustomerFullName = "ADMIN",
                    };
                }
            }

            return null;
        } 
    }
}

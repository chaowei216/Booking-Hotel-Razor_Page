using BusinessObject.Models;
using Common.DTO;

namespace BusinessLogic.Interfaces
{
    public interface IAuthService
    {
        Task<Customer?> CheckLogin(string email, string password);
        Task<Customer?> Register(RegisterRequestDTO request);
    }
}

using INMAR.Service.Models;

namespace INMAR.Service.Interfaces
{
    public interface IAuthenticationService
    {
        Task<AuthResponse> Authenticate(string email, string phone);
    }
}

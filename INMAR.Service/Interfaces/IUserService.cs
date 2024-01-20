using INMAR.Service.Models;

namespace INMAR.Service.Interfaces
{
    public interface IUserService
    {
        Task<bool> InsertOrUpdateUser(Users users);
        Task<bool> DeleteUser(long userId);
        Task<IQueryable<Users>> GetAllUsers();
        Task<Users> GetUser(long userId);
    }
}

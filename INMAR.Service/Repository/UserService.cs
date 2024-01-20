using INMAR.Service.DdContextConfiguration;
using INMAR.Service.Interfaces;
using INMAR.Service.Models;
using Microsoft.EntityFrameworkCore;

namespace INMAR.Service.Repository
{
    public class UserService : IUserService
    {
        private readonly ApplicationDBContext dbContext;
        public UserService(ApplicationDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<bool> DeleteUser(long userId)
        {
            if (userId > 0)
            {
                var user = await dbContext.users.FindAsync(userId);
                if (user != null)
                {
                    user.IsActive = false;
                    var responce = await dbContext.SaveChangesAsync();
                    return responce == 1 ? true : false;
                }
            }
            return false;
        }

        public async Task<IQueryable<Users>> GetAllUsers()
        {
            return dbContext.users.AsNoTracking().AsQueryable();
        }

        public async Task<Users> GetUser(long userId)
        {
            return await dbContext.users.FindAsync(userId);
        }

        public async Task<bool> InsertOrUpdateUser(Users user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User cannot be null.");
            }

            if (user.UserId > 0)
            {
                var existingUser = await dbContext.users.FindAsync(user.UserId);
                if (existingUser != null)
                {
                    existingUser.Name = user.Name;
                    existingUser.ModifiedOn = DateTimeOffset.Now;
                    existingUser.ModifiedBy = -1;
                    var responce = await dbContext.SaveChangesAsync();
                    return responce == 1 ? true : false;
                }
                else
                {
                    throw new InvalidOperationException($"User with ID {user.UserId} not found.");
                }
            }
            else
            {
                await dbContext.users.AddAsync(user);
                var responce = await dbContext.SaveChangesAsync();
                return responce == 1 ? true : false;
            }
        }

    }
}

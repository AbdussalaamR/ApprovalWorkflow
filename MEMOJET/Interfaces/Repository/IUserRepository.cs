using System.Collections.Generic;
using System.Threading.Tasks;
using MEMOJET.Identity;

namespace MEMOJET.Interfaces.Repository
{
    public interface IUserRepository
    {
        Task<User> AddUser(User user);
        Task<User> UpdateUser (User user);
        Task<bool> DeleteUser (User user);
        Task<User> GetUser (int id);
        Task<User> GetUser (string username);
        Task<User> GetUserByEmail (string username);
        Task<IList<UserRole>> GetUserByRole (string roleName);
        Task<IList<string>> GetUserRole(User user);
        Task<IList<User>> GetUsers();
        Task<bool> EmailExist(string email);
        Task<User> GetLoogedInUser(string email, string password);
    }
}
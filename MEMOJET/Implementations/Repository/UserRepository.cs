using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MEMOJET.Context;
using MEMOJET.Identity;
using MEMOJET.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace MEMOJET.Implementations.Repository
{
    public class UserRepository:IUserRepository
    {
        private readonly ApplicationContext _context;

        public UserRepository(ApplicationContext context)
        {
            _context = context;
        }
        
        public async Task<User> AddUser(User user)
        {
           await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateUser(User user)
        {
             _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> DeleteUser(User user)
        {
           _context.Users.Remove(user);
           await _context.SaveChangesAsync();
            return true;
        }

        public async Task<User> GetUser(int id)
        {
            var user = await  _context.Users.Include(x => x.UserRoles).ThenInclude(y=>y.Role)
                .Where(x => x.Id == id && x.IsDeleted == false).SingleOrDefaultAsync();
            return user;
        }

        public async Task<User> GetUserByEmail(string username)
        {
            var user =  await _context.Users.Include(x => x.UserRoles).ThenInclude(y=>y.Role)
                .Where(x => x.Email == username).SingleOrDefaultAsync();
            return user;
        }

        public async Task<IList<UserRole>> GetUserByRole(string roleName)
        {
            var users =await _context.UserRoles.Include(x => x.User)
                .Include(x => x.Role).Where(x => x.Role.Name == roleName).ToListAsync();
            return users;
        }

        public async Task<IList<string>> GetUserRole(User user)
        {
            var userRoles = await _context.UserRoles.Include(u => u.Role)
                .Where(u => u.User == user).Select(r => r.Role.Name).ToListAsync();
            return userRoles;
        }

        public async Task<IList<User>> GetUsers()
        {
            return await _context.Users.Include(x=>x.UserRoles).ThenInclude(y=>y.Role).ToListAsync();
        }
        

        public async Task<bool> EmailExist(string email)
        {
            return await _context.Users.AnyAsync(x => x.Email == email);
        }

        public async Task<User> GetLoogedInUser(string email, string password)
        {
            var user = await _context.Users.Include(x => x.UserRoles).ThenInclude(y=>y.Role).
                Where(x => x.Email == email && x.Password == password).FirstOrDefaultAsync();
            return user;
        }

        public async Task<User> GetUser(string username)
        {
            var user =  await _context.Users.Include(x => x.UserRoles).ThenInclude(y=>y.Role).Where(x => x.UserName == username).SingleOrDefaultAsync();
            return user;
        }
        
    }
}
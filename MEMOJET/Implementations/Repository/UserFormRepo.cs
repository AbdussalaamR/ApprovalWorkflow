using System.Threading.Tasks;
using MEMOJET.Context;
using MEMOJET.Entities;
using MEMOJET.Interfaces.Repository;

namespace MEMOJET.Implementations.Repository
{
    public class UserFormRepo:IUserformRepo
    {
        private readonly ApplicationContext _context;

        public UserFormRepo(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<UserForm> UpdateUserForm(UserForm userForm)
        { 
            _context.UserForms.Update(userForm);
            await _context.SaveChangesAsync();
            return userForm;
        }
    }
}
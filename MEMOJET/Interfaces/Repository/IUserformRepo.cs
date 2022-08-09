using System.Threading.Tasks;
using MEMOJET.Entities;

namespace MEMOJET.Interfaces.Repository
{
    public interface IUserformRepo
    {
        public Task<UserForm> UpdateUserForm(UserForm userForm);
    }
}
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MEMOJET.Entities;

namespace MEMOJET.Interfaces.Repository
{
    public interface IFormRepo
    {
        Task<Form> CreateForm(Form form);
        Task<UserForm> GetApprovedForms(int id);
        Task<Form> UpdateForm(Form form);
        Task<bool> DeleteForm(Form form);
        Task<bool> FormExists(string formName);
        Task<Form> Getform(int id);
        
        Task<IList<Form>> GetForms();
        Task<IList<UserForm>> GetFormsByUser(int userId);
        public Task<UserForm> GetUserform(int id);
        Task<IList<UserForm>> GetFormsByApproval(IList<int> Ids);
        Task<IList<UserForm>> GetAllFormsByApproval(int userId);
    }
}
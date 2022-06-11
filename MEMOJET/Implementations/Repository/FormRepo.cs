using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MEMOJET.Context;
using MEMOJET.Entities;
using MEMOJET.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace MEMOJET.Implementations.Repository
{
    public class FormRepo:IFormRepo
    {
        private readonly ApplicationContext _context;

        public FormRepo(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<Form> CreateForm(Form form)
        {
            await _context.Forms.AddAsync(form);
            await _context.SaveChangesAsync();
            return form;
        }

        public async Task<UserForm> GetUserForm(int id)
        {
            var form = await _context.UserForms.FirstOrDefaultAsync(x =>x.IsDeleted == false && x.Id == id);
            return form;
        }

        public async Task<Form> UpdateForm(Form form)
        {
            _context.Forms.Update(form);
            await _context.SaveChangesAsync();
            return form;
        }

        public async Task<bool> DeleteForm(Form form)
        {
            _context.Forms.Remove(form);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> FormExists(string formName)
        {
            return await _context.Forms.AnyAsync(x => x.Title == formName);
        }
        public async Task<Form> Getform(int id)
        {
            var form = await _context.Forms.FirstOrDefaultAsync(x =>x.IsDeleted == false && x.Id == id);
            return form;
        }
            
        public async Task<UserForm> GetUserform(int id)
        {
            var form = await _context.UserForms.FirstOrDefaultAsync(x =>x.IsDeleted == false && x.Id == id);
            return form;
        }
        public async Task<IList<Form>> GetForms()
        {
            var forms = await _context.Forms.ToListAsync();
            return forms;
        }

        public async Task<IList<UserForm>> GetFormsByUser(int userId)
        {
            var forms = await _context.UserForms.Where(x => x.UserId == userId).ToListAsync();
            return forms;
        }
    }
}
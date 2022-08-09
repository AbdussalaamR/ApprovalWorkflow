using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MEMOJET.Context;
using MEMOJET.Entities;
using MEMOJET.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace MEMOJET.Implementations.Repository
{
    public class CommentRepo:ICommentRepo
    {
        private readonly ApplicationContext _context;

        public CommentRepo(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Comment> CreateComment(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<Comment> UpdateComment(Comment comment)
        {
            _context.Comments.Update(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<Comment> GetComment(int id)
        {
            var comment = await _context.Comments.Include(x => x.UserForm).FirstOrDefaultAsync(x =>x.IsDeleted == false && x.Id == id);
            return comment;
        }

        public async Task<IList<Comment>> GetComments()
        {
            var comments = await _context.Comments.ToListAsync();
            return comments;
        }
    }
}
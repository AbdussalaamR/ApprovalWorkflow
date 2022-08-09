using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using MEMOJET.Context;
using MEMOJET.Entities;

namespace MEMOJET.Interfaces.Repository
{
    public interface ICommentRepo
    {
        public Task<Comment> CreateComment(Comment comment);
        public Task<Comment> UpdateComment(Comment comment);
        public Task<Comment> GetComment(int id);
        public Task<IList<Comment>> GetComments();

    }
}
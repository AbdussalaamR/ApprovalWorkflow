using System.Collections.Generic;
using System.Threading.Tasks;
using Ubiety.Dns.Core;

namespace MEMOJET.Interfaces.Repository
{
    public interface IQuestionRepository
    {
        Task<Question> CreateQuestion(Question question);
        Task<Question> UpdateQuestion(Question question);
        Task<bool> DeleteQuestion(Question question);
        Task<Question> GetQuestion(int id);
        Task<IList<Question>> GetQuestions();
    }
}
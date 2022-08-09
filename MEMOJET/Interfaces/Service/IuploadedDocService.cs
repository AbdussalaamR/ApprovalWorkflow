using System.Threading.Tasks;
using MEMOJET.DTOs;

namespace MEMOJET.Interfaces.Service
{
    public interface IuploadedDocService
    {
        public Task<UploadedDocResponseModel> GetDoc(int Id);
    }
}
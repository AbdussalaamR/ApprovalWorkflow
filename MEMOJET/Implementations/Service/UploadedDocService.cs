using System.Threading.Tasks;
using MEMOJET.DTOs;
using MEMOJET.Interfaces.Repository;
using MEMOJET.Interfaces.Service;

namespace MEMOJET.Implementations.Service
{
    public class UploadedDocService : IuploadedDocService
    {
        private readonly IUploadedDocRepo _uploadedDocRepo;

        public UploadedDocService(IUploadedDocRepo uploadedDocRepo)
        {
            _uploadedDocRepo = uploadedDocRepo;
        }

        public async Task<UploadedDocResponseModel> GetDoc(int Id)
        {
            var g = await _uploadedDocRepo.GetDocument(Id);
            if (g == null)
            {
                return new UploadedDocResponseModel
                {
                    Message = $"Document NOT found",
                    Status = false,
                };
            }

            return new UploadedDocResponseModel
            {
                Data = new UploadedDocDto
                {
                    Name = g.Name,
                    Id = g.Id,
                    Extension = g.Extension,
                    FileType = g.FileType,
                    Description = g.Description,
                    UploadedBy = g.UploadedBy,
                    UserFormId = g.UserFormId,
                    Data = g.Data
                },
                Message = $"Document retrieved successfully",
                Status = true
            };
        }
    }
}
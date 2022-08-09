using System.Collections.Generic;
using System.Threading.Tasks;
using MEMOJET.Entities;

namespace MEMOJET.Interfaces.Repository
{
    public interface IUploadedDocRepo
    {
        public Task<UploadedDoc> CreateDoc(UploadedDoc doc);
        public Task<UploadedDoc> UpdateDoc(UploadedDoc doc);
        public Task<UploadedDoc> GetDocument(int id);
        public Task<IList<UploadedDoc>> GetDocs();
    }
}
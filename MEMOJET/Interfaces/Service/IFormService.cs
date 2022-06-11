using System;
using System.Threading.Tasks;
using MEMOJET.DTOs;

namespace MEMOJET.Interfaces.Service
{
    public interface IFormService
    {
        public Task<FormResponseModel> CreateForm(CreateFormRequestModel model);
        public Task<FormResponseModel> FillForm(FillFormRequestModel model);
        public Task<FormResponseModel> ApproveForm(ApproveFormRequestModel model);
        public Task<FormResponseModel> UpdateForm(CreateFormRequestModel model, int id);
        public Task<FormResponseModel> GetForm(int id);
        public Task<FormsResponseModel> GetAllForms();
        public Task<FormsResponseModel> GetAllFormsByUser(int userId);
        public Task<string> DeleteForm(int id);
        public Task<String> GetFormStatus(int id);
        public Task<String> GetFormLocation(int id);
    }
}
using System;
using System.Linq;
using System.Threading.Tasks;
using MEMOJET.DTOs;
using MEMOJET.Entities;
using MEMOJET.Interfaces.Repository;
using MEMOJET.Interfaces.Service;

namespace MEMOJET.Implementations.Service
{
    public class FormService:IFormService
    {
        private readonly IFormRepo _formRepo;
        private readonly IUserRepository _userRepo;
        private readonly IApprovalRepo _approvalRepo;

        public FormService(IFormRepo formRepo, IUserRepository userRepo, IApprovalRepo approvalRepo)
        {
            _formRepo = formRepo;
            _userRepo = userRepo;
            _approvalRepo = approvalRepo;
        }

        public async Task<FormResponseModel> CreateForm(CreateFormRequestModel model)
        {
            var formExists = await _formRepo.FormExists(model.Name);
            if (formExists)
            {
                return new FormResponseModel
                {
                    Message = $"Form with title {model.Name} already exists",
                };
            }

            var form = new Form
            {
                Description = model.Description,
                Title = model.Name,
                Data = model.Data,
            };
            var newForm = await _formRepo.CreateForm(form);
            if (newForm == null)
            {
                return new FormResponseModel
                {
                    Message = $"Unable to create form",
                };
            }
            return new FormResponseModel
            {
                Message = $"{model.Name.ToUpper()} created successfully",
                Status = true,
                Data = new FormDto
                {
                    Id = newForm.Id,
                    Description = newForm.Description,
                    Data = newForm.Data,
                    Name = newForm.Title,
                    RespoCentreId = newForm.RespoCentreId
                }
            };
        }

        public async Task<FormResponseModel> FillForm(FillFormRequestModel model)
        {//Creates userform with users inputs in form filled
            var form = await _formRepo.Getform(model.FormId);
            var user = await _userRepo.GetUser(model.UserId);
            var userForm = new UserForm
            {
                Form = form,
                User = user,
                UserId = user.Id,
                FormId = form.Id,
                Data = model.Data,
                FormType = form.Title,
                ResponsibilityCentreId = form.RespoCentreId,
                ApprovalStatus = ApprovalStatus.InProgress,
            }; 
            var formApprovals = await _approvalRepo
                .GetApprovalsInCentre(form.RespoCentreId);
            var orderedApprovals = formApprovals.OrderBy(x => x.Sequence).ToList();
            userForm.ApprovalId = orderedApprovals[0].Id;
            user.UserForms.Add(userForm);
            return new FormResponseModel
            {
                Message = "Successfully submitted",
                Status = true,
                Data = new FormDto
                {
                    Description = form.Description,
                    Id = userForm.Id,
                    Name = userForm.FormType,
                    Data = userForm.Data,
                    RespoCentreId = userForm.ResponsibilityCentreId
                }
            };
        }

        public async Task<FormResponseModel> ApproveForm(ApproveFormRequestModel model)
        {
            // var form = await _formRepo.Getform(model.FormId); 
            // var userform = form.UserForms
            //     .Where(x => x.FormId == form.Id).FirstOrDefault();

            var userform = await _formRepo.GetUserform(model.FormId);
            userform.ApprovalAction = model.Action;
            var comment = new Comment
            {
                ApprovalComment = model.ApprovalComment,
                UserFormId = userform.Id,
                ApprovalId = userform.ApprovalId,
                UserForm = userform
            };
            userform.Comments.Add(comment);
            var formApprovals = await _approvalRepo
                .GetApprovalsInCentre(userform.ResponsibilityCentreId);
            var orderedApprovals = formApprovals.OrderBy(x => x.Sequence).ToList();
            var lenght = orderedApprovals.Count;
            var currentApproval = orderedApprovals.Where(x => x.Id == userform.ApprovalId).FirstOrDefault();
            var index = orderedApprovals.IndexOf(currentApproval);
            if (userform.ApprovalId == orderedApprovals[lenght - 1].Id)
            {
                if (userform.ApprovalAction == ApprovalAction.Approved)
                {
                    userform.ApprovalStatus = ApprovalStatus.Approved;
                }
                else if (userform.ApprovalAction == ApprovalAction.Rejected)
                {
                    userform.ApprovalStatus = ApprovalStatus.Rejected;
                }
            }
            if (userform.ApprovalAction == ApprovalAction.Approved)
            {
                userform.ApprovalId = orderedApprovals[index + 1].Id;
            } 
            else if (userform.ApprovalAction == ApprovalAction.Rejected)
            {
                userform.ApprovalStatus = ApprovalStatus.Rejected;
            }
            else if (userform.ApprovalAction == ApprovalAction.Revise)
            {
                userform.ApprovalId = orderedApprovals[index - 1].Id;;
            }

            return new FormResponseModel
            {
                Message = "Success",
                Status = true,
                Data = new FormDto
                {
                    Id = userform.Id,
                    Name = userform.FormType,
                    Data = userform.Data,
                    RespoCentreId = userform.ResponsibilityCentreId
                }
            };
        }

        public async Task<FormResponseModel> UpdateForm(CreateFormRequestModel model, int id)
        {
            var form = await _formRepo.Getform(id);
            if (form == null)
            {
                return new FormResponseModel
                {
                    Message = "Form not found"
                };
            }

            form.Title = model.Name;
            form.Description = model.Description;
            form.Data = model.Data;
            form.RespoCentreId = model.RespoCentreId;
            var updatedForm = await _formRepo.UpdateForm(form);
            if (updatedForm == null)
            {
                return new FormResponseModel
                {
                    Message = "Unable to update form"
                };
            }

            return new FormResponseModel
            {
                Message = "Successfully updated",
                Status = true,
                Data = new FormDto
                {
                    Id = updatedForm.Id,
                    Description = updatedForm.Description,
                    Data = updatedForm.Data,
                    Name = updatedForm.Title,
                    RespoCentreId = updatedForm.RespoCentreId
                }
            };
        }

        public async Task<FormResponseModel> GetForm(int id)
        {
            var form = await _formRepo.Getform(id);
            if (form == null)
            {
                return new FormResponseModel
                {
                    Message = "Form not found",
                    Status = true
                };
            }

            return new FormResponseModel
            {
                Data = new FormDto
                {
                    Id = form.Id,
                    Name = form.Title,
                    Description = form.Description,
                    Data = form.Data
                },
                Message = "Retrieved successfully",
                Status = true
            };
        }

        public async Task<FormsResponseModel> GetAllForms()
        {
            var forms = await _formRepo.GetForms();
            var formsDto = forms.Select(g => new FormDto
            {
                Name = g.Title,
                Description = g.Description,
                Data = g.Data,
                Id = g.Id 
            }).ToList();
            return new FormsResponseModel
            {
                Data = formsDto,
                Message = "Retrieved",
                Status = true,
            };
        }

        public async Task<FormsResponseModel> GetAllFormsByUser(int userId)
        {
            var forms = await _formRepo.GetFormsByUser(userId);
            var formsDto = forms.Select(g => new FormDto
            {
                Name = g.Form.Title,
                Description = g.Form.Description,
                Data = g.Data,
                RespoCentreId = g.ResponsibilityCentreId,
                Id = g.Id 
            }).ToList();
            return new FormsResponseModel
            {
                Data = formsDto,
                Message = "Retrieved",
                Status = true,
            };
        }

        public async Task<string> DeleteForm(int id)
        {
            var form = await _formRepo.Getform(id);
            if (form==null)
            {
                return "Form not found";
            }

            form.IsDeleted = true;
            await _formRepo.UpdateForm(form);
            return "Form deleted successfully";
        }

        public async Task<string> GetFormStatus(int id)
        {
            var form = await _formRepo.GetUserform(id);
            if (form==null)
            {
                return "Form not found";
            }

            return $"Status: {form.ApprovalStatus}";
        }

        public async Task<string> GetFormLocation(int id)
        {
            var form = await _formRepo.GetUserform(id);
            if (form == null)
            {
                return "Form not found";
            }
 
            var approval = await _approvalRepo.GetApproval(form.ApprovalId);
            return $"Form location: {approval.ApprovalRole}";
        }
    }
}
using System.Linq;
using System.Threading.Tasks;
using MEMOJET.DTOs;
using MEMOJET.Entities;
using MEMOJET.Interfaces.Repository;
using MEMOJET.Interfaces.Service;

namespace MEMOJET.Implementations.Service
{
    public class RespoCentreService:IRespoCentreService
    {
        private readonly IResponsibilityCentreRepo _centreRepo;
        private readonly IApprovalRepo _approvalRepo;

        public RespoCentreService(IResponsibilityCentreRepo centreRepo, IApprovalRepo approvalRepo)
        {
            _centreRepo = centreRepo;
            _approvalRepo = approvalRepo;
        }

        public async Task<RespoCentreResponse> CreateCentre(CreateRespoCentreDto model)
        {
            var centreExists = await _centreRepo.CentreExist(model.Name);

            if (centreExists)
            {
                return new RespoCentreResponse
                {
                    Message = $"Centre named {model.Name} already exists",
                    Status = false
                };
            }

            var respoCentre = new ResponsibilityCentre
            {
                Description = model.Description,
                Name = model.Name,
            };
            var addNew = await _centreRepo.CreateRespoCentre(respoCentre);
            if (addNew == null)
            {
                return new RespoCentreResponse
                {
                    Message = "Unable to create responsibility centre",
                    Status = false
                };
            }
            return new RespoCentreResponse
            {
                Message = $"Centre {model.Name} created successfully",
                Status = true,
                Data = new RespoCentreDto
                {
                    Name = addNew.Name,
                    Description = addNew.Description,
                    id = addNew.Id
                }
            }; 
        }

        public async Task<RespoCentreResponse> UpdateCentre(int id, CreateRespoCentreDto model)
        {
            var centre = await _centreRepo.GetCentre(id);
            if (centre == null)
            {
                return new RespoCentreResponse
                {
                    Message = $"Centre with {id} does not exist",
                    Status = false

                };
            }

            centre.Name = model.Name;
            centre.Description = model.Description;
            var UpdatedCent = await _centreRepo.UpdateRespoCentre(centre);
            if (UpdatedCent == null)
            {
                return new RespoCentreResponse
                {
                    Message = "Unable to updateate responsibility centre",
                    Status = false
                };
            }
            return new RespoCentreResponse
            {
                Message = $"Centre {model.Name} created successfully",
                Status = true,
                Data = new RespoCentreDto
                {
                    Name = UpdatedCent.Name,
                    Description = UpdatedCent.Description
                }
            };
        }

        public async Task<RespoCentreResponse> DeleteApprovalFromCentre(int approvalId, int respoCentreId)
        {
            var respoCentre = await _centreRepo.GetCentre(respoCentreId);
            if (respoCentre == null)
            {
                return new RespoCentreResponse
                {
                    Message = "Unable to retrieve responsibility centre",
                    Status = false
                };
            }

            var approval = await _approvalRepo.GetApproval(approvalId);
            if (approval == null)
            {
                return new RespoCentreResponse
                {
                    Message = $"Unable to retrieve approval with id {approvalId}",
                    Status = false
                }; 
            }
            var approvals = await _approvalRepo.GetApprovalWithCentres(respoCentreId);

            foreach (var approv in approvals)
            {
                if (approv.Approval == approval)
                {
                    respoCentre.ApprovalResponsibilityCentres.Remove(approv);
                    await _centreRepo.UpdateRespoCentre(respoCentre);
                    
                }
            }
            return new RespoCentreResponse
            {
                Message = $"Approval with id {approvalId} successfully removed from {respoCentre.Name}",
                Status = true,
                Data = new RespoCentreDto
                {
                    id = respoCentreId,
                    Name = respoCentre.Name,
                    Description = respoCentre.Description
                }
            };
        }

        public async Task<RespoCentreResponse> GetCentre(int id)
        {
            var centre = await _centreRepo.GetCentre(id);
            if (centre == null)
            {
                return new RespoCentreResponse
                {
                    Message = "Not found",
                    Status = false
                };
            }

            return new RespoCentreResponse
            {
                Message = "Successfully retrieved",
                Status = false,
                Data = new RespoCentreDto
                {
                    ApprovalResponsibilityCentres = centre.ApprovalResponsibilityCentres,
                    Description = centre.Description,
                    id = centre.Id,
                    Name = centre.Name
                }
            };
        }
        public async Task<RespoCentreResponse> GetCentreByName(string name)
        {
            var centre = await _centreRepo.GetCentreByName(name);
            if (centre == null)
            {
                return new RespoCentreResponse
                {
                    Message = "Not found",
                    Status = false
                };
            }

            return new RespoCentreResponse
            {
                Message = "Successfully retrieved",
                Status = false,
                Data = new RespoCentreDto
                {
                    ApprovalResponsibilityCentres = centre.ApprovalResponsibilityCentres,
                    Description = centre.Description,
                    id = centre.Id,
                    Name = centre.Name
                }
            };
        }

        public async Task<RespoCentresResponse> GetCentres()
        {
            var centres = await _centreRepo.GetCentres();
            var centreDto = centres.Select(g => new RespoCentreDto
            {
                id = g.Id,
                Description = g.Description,
                Name = g.Name,
                ApprovalResponsibilityCentres = g.ApprovalResponsibilityCentres
            }).ToList();

            return new RespoCentresResponse
            {
                Message = "Succssful",
                Status = true,
                Data = centreDto
            };
        }

        public async Task<bool> DeleteCentre(int id)
        {
            var centre = await _centreRepo.GetCentre(id);
            if (centre == null)
            {
                return false;
            }

            centre.IsDeleted = true;
            await _centreRepo.UpdateRespoCentre(centre);
            return true;
        }
        
    }
}
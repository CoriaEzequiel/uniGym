using Application.Models.Request;
using Application.Models.Response;
using DomainEntity = Domain.Entities;

namespace Application.Mappings
{
    public static class SuperAdmProfile
    {
        public static DomainEntity.SuperAdm ToSuperAdminEntity(SuperAdmRequest request)
        {
            return new DomainEntity.SuperAdm()
            {
                UserName = request.UserName,
                Password = request.Password,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Dni = request.Dni,
                Email = request.Email,

            };
        }



        public static SuperAdmResponse ToSuperAdminResponse(DomainEntity.SuperAdm superAdmin)
        {
            return new SuperAdmResponse()
            {
                UserName = superAdmin.UserName,
                Id = superAdmin.Id,
                FirstName = superAdmin.FirstName,
                LastName = superAdmin.LastName,
                Dni = superAdmin.Dni,
                Email = superAdmin.Email
            };
        }

        public static List<SuperAdmResponse> ToSuperAdminResponse(List<DomainEntity.SuperAdm> superAdmins)
        {
            return superAdmins.Select(c => new SuperAdmResponse
            {
                UserName = c.UserName,
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Dni = c.Dni,
                Email = c.Email

            }).ToList();
        }
    }
}
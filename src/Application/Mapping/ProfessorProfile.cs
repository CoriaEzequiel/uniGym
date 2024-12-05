using Application.Models.Request;
using Application.Models.Response;
using Domain.Enum;
using DomainEntity = Domain.Entities;

namespace Application.Mappings
{
    public static class ProfessorProfile
    {

        public static DomainEntity.Professor ToProfessorEntity(ProfessorRequest request)
        {
            return new DomainEntity.Professor()
            {
                UserName = request.UserName,
                Password = request.Password,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Dni = request.Dni,
                Email = request.Email,
                ProfessorClass = (ProfessorClass)request.ProfessorClass,
                Phone = request.Phone,
                Address = request.Address
            };


        }



        public static ProfessorResponse ToProfessorResponse(DomainEntity.Professor professor)
        {
            return new ProfessorResponse()
            {
                UserName = professor.UserName,
                Password = professor.Password,
                Id = professor.Id,
                FirstName = professor.FirstName,
                LastName = professor.LastName,
                ProfessorClass = professor.ProfessorClass,
                Dni = professor.Dni,
                Email = professor.Email,
                Phone = professor.Phone,
                Address = professor.Address

            };
        }

        public static List<ProfessorResponse> ToProfessionalResponse(List<DomainEntity.Professor> professional)
        {
            return professional.Select(p => new ProfessorResponse
            {
                UserName = p.UserName,
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Dni = p.Dni,                
                ProfessorClass = p.ProfessorClass,
                Email = p.Email,
                Phone = p.Phone,
                Address = p.Address
            }).ToList();
        }
    }

}
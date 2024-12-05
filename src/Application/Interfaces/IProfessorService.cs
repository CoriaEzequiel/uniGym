using Domain.Entities;
using Domain.Enum;
using Application.Models.Response;
using Application.Models.Request;

namespace Application.Interfaces
{
    public interface IProfessorService
    {

        List<Professor> GetAllProfessors();
        ProfessorResponse? GetProfessorById(int id);
        List<ProfessorResponse> GetProfessorByClass(ProfessorClass professorClass);
        void CreateProfessor(ProfessorRequest entity);
        bool UpdateProfessor(int id, ProfessorRequest professor);
        bool DeleteProfessor(int id);

    }
}

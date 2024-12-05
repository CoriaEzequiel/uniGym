using Domain.Entities;
using Domain.Enum;

namespace Domain.Interfaces
{
    public interface IProfessorRepository
    {
        List<Professor> GetAllProfessors();
        Professor? GetProfessorById(int id);
        List<Professor> GetProfessorByClass(ProfessorClass professorclass);
        void AddProfessor(Professor entity);
        void UpdateProfessor(Professor entity);
        void DeleteProfessor(Professor entity);
    }
}
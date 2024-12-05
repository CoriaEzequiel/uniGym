using Application.Interfaces;
using Application.Mappings;
using Application.Models.Request;
using Application.Models.Response;
using Domain.Entities;
using Domain.Enum;
using Domain.Interfaces;

namespace Application.Services
{
    public class ProfessorService : IProfessorService
    {
        private readonly IProfessorRepository _professorRepository;
        private readonly IMeetingService _meetingService;

        public ProfessorService(IProfessorRepository professorRepository, IMeetingService meetingService)
        {
            _professorRepository = professorRepository;
            _meetingService = meetingService;
        }

        public List<Professor> GetAllProfessors()
        {
            return _professorRepository.GetAllProfessors();
        }

        public ProfessorResponse? GetProfessorById(int id)
        {
            var professor = _professorRepository.GetProfessorById(id);
            return professor == null ? null : ProfessorProfile.ToProfessorResponse(professor);
        }

        public List<ProfessorResponse> GetProfessorByClass(ProfessorClass professorClass)
        {
            var professors = _professorRepository.GetProfessorByClass(professorClass);
            return professors.Select(p => ProfessorProfile.ToProfessorResponse(p)).ToList();
        }

        public void CreateProfessor(ProfessorRequest professor)
        {
            // Validamos.
            var professorEntity = ProfessorProfile.ToProfessorEntity(professor);
            _professorRepository.AddProfessor(professorEntity);
        }

        public bool UpdateProfessor(int id, ProfessorRequest professor)
        {
            // Validamos.
            var professorEntity = _professorRepository.GetProfessorById(id);
            if (professorEntity == null)
            {
                return false; 
            }

            // Actualizar las propiedades del profesor
            professorEntity.FirstName = professor.FirstName;
            

            _professorRepository.UpdateProfessor(professorEntity);
            return true;
        }

        public bool DeleteProfessor(int id)
        {
            var professor = _professorRepository.GetProfessorById(id);

            if (professor == null)
            {
                return false; // Profesor no encontrado
            }

            // Eliminar reuniones asociadas al profesor
            _meetingService.DeleteMeetingsByProfessor(professor.Id);

            // Eliminar el profesor
            _professorRepository.DeleteProfessor(professor);

            return true;
        }
    }
}
using Domain.Entities;
using Domain.Enum;
using Domain.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ProfessorRepository : IProfessorRepository
    {
        private readonly uniContext _context;

        public ProfessorRepository(uniContext context)
        {
            _context = context;
        }

        // Obtener todos los profesores
        public List<Professor> GetAllProfessors()
        {
            return _context.Professors.ToList();
        }

        // Obtener un profesor por su ID
        public Professor? GetProfessorById(int id)
        {
            return _context.Professors.FirstOrDefault(x => x.Id == id);
        }

        // Obtener profesores por la clase a la que pertenecen
        public List<Professor> GetProfessorByClass(ProfessorClass professorClass)
        {
            return _context.Professors.Where(professor => professor.ProfessorClass == professorClass).ToList();
        }

        // Agregar un nuevo profesor
        public void AddProfessor(Professor entity)
        {
            _context.Professors.Add(entity);
            _context.SaveChanges();
        }

        // Actualizar la información de un profesor
        public void UpdateProfessor(Professor entity)
        {
            _context.Professors.Update(entity);
            _context.SaveChanges();
        }

        // Eliminar un profesor
        public void DeleteProfessor(Professor professor)
        {
            _context.Professors.Remove(professor);
            _context.SaveChanges();
        }
    }
}

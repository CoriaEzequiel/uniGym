using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Context;


namespace Infrastructure.Data;

public class SuperAdmRepository : ISuperAdmRepository
{
    private readonly uniContext _context;

    public SuperAdmRepository(uniContext context)
    {
        _context = context;
    }

    public List<SuperAdm> GetAllSuperAdms()
    {
        return _context.SuperAdms.ToList();
    }

    public SuperAdm? GetSuperAdmById(int id)
    {
        return _context.SuperAdms.FirstOrDefault(x => x.Id.Equals(id));
    }

    public void AddSuperAdm(SuperAdm entity)
    {
        _context.SuperAdms.Add(entity);
        _context.SaveChanges();
    }

    public void UpdateSuperAdm(SuperAdm entity)
    {
        _context.SuperAdms.Update(entity);
        _context.SaveChanges();
    }

    public void DeleteSuperAdm(SuperAdm entity)
    {
        _context.SuperAdms.Remove(entity);
        _context.SaveChanges();
    }
}
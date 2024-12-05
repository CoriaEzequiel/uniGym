using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Context;


namespace Infrastructure.Data;

public class VipClientRepository : IVipClientRepository
{
    private readonly uniContext _context;

    public VipClientRepository(uniContext context)
    {
        _context = context;
    }

    public List<VipClient> GetAllVipClients()
    {
        return _context.VipClients.ToList();
    }

    public VipClient? GetVipClientById(int id)
    {
        return _context.VipClients.FirstOrDefault(x => x.Id.Equals(id));
    }

    public void AddVipClient(VipClient entity)
    {
        _context.VipClients.Add(entity);
        _context.SaveChanges();
    }

    public void UpdateVipClient(VipClient entity)
    {
        _context.VipClients.Update(entity);
        _context.SaveChanges();
    }

    public void DeleteVipClient(VipClient entity)
    {
        _context.VipClients.Remove(entity);
        _context.SaveChanges();
    }
}
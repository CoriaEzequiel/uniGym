using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ISuperAdmRepository
    {
        List<SuperAdm> GetAllSuperAdms();
        SuperAdm? GetSuperAdmById(int id);
        void AddSuperAdm(SuperAdm entity);
        void UpdateSuperAdm(SuperAdm entity);
        void DeleteSuperAdm(SuperAdm entity);
    }
}
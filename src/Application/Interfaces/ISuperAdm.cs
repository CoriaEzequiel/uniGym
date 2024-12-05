using Application.Models.Request;
using Application.Models.Response;
using Domain.Entities;


namespace Application.Interfaces
{
    public interface ISuperAdm
    {
        //List<SuperAdm> GetAllSuperAdmins();
        
        SuperAdmResponse? GetSuperAdminById(int id);
        void CreateSuperAdm(SuperAdmRequest entity);
        bool UpdateSuperAdm(int id, SuperAdmRequest superAdmin);
        bool DeleteSuperAdm(int id);
    }
}

using Domain.Entities;


namespace Domain.Interfaces
{
    public interface IVipClientRepository
    {
        List<VipClient> GetAllVipClients();
        VipClient? GetVipClientById(int id);
        void AddVipClient(VipClient entity);
        void UpdateVipClient(VipClient entity);
        void DeleteVipClient(VipClient entity);
    }
}

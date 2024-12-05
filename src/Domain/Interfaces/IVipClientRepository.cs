using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    internal interface IVipClientRepository
    {
        List<VipClient> GetAllVipClients();
        VipClient? GetVipClientById(int id);
        void AddVipClient(VipClient entity);
        void UpdateVipClient(VipClient entity);
        void DeleteVipClient(VipClient entity);
    }
}

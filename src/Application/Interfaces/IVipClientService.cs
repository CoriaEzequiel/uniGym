using Application.Models.Request;
using Application.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IVipClientService
    {
        List<VipClientResponse> GetAllVipClient();
        VipClientResponse? GetVipClientById(int id);
        void CreateVipClient(VipClientRequest entity);
        bool UpdateVipClient(int id, VipClientRequest vipclient);
        bool DeleteVipClient(int id);

    }
}

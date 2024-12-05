using Application.Models.Request;
using Application.Models.Response;
using DomainEntity = Domain.Entities;

namespace Application.Mappings
{
    public static class VipClientProfile
    {
        public static DomainEntity.VipClient ToVipClientEntity(VipClientRequest request)
        {
            return new DomainEntity.VipClient()
            {
                UserName = request.UserName,
                Password = request.Password,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Dni = request.Dni,
                Email = request.Email,
                TypeUser = "VipClient",
                Phone = request.Phone,
                Address = request.Address,
            };
        }


        public static VipClientResponse ToVipClientResponse(DomainEntity.VipClient vipclient)
        {
            if (vipclient == null)
            {
                throw new ArgumentNullException(nameof(vipclient), "El cliente no puede ser nulo.");
            }

            return new VipClientResponse()
            {
                UserName = vipclient.UserName,
                Password = vipclient.Password,
                Id = vipclient.Id,
                FirstName = vipclient.FirstName,
                LastName = vipclient.LastName,
                Dni = vipclient.Dni,
                Email = vipclient.Email,
                Phone = vipclient.Phone,
                Address = vipclient.Address
            };
        }

        public static List<VipClientResponse> ToVipClientResponseList(List<DomainEntity.VipClient> vipclients)
        {
            return vipclients.Select(vipclient => new VipClientResponse
            {
                Id = vipclient.Id,
                FirstName = vipclient.FirstName,
                LastName = vipclient.LastName,
                Dni = vipclient.Dni,
                Email = vipclient.Email,
                Phone = vipclient.Phone,
                Address = vipclient.Address

            }).ToList();
        }
    }
}
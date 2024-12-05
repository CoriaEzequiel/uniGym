using Application.Interfaces;
using Application.Mappings;
using Application.Models.Request;
using Application.Models.Response;
using Domain.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class VipClientService : IVipClientService
    {
        private readonly IVipClientRepository _vipclientRepository;
        private readonly IMeetingService _meetingService;

        public VipClientService(IVipClientRepository vipClientRepository, IMeetingService meetingService)
        {
            _vipclientRepository = vipClientRepository;
            _meetingService = meetingService;
        }

        public List<VipClientResponse> GetAllVipClient()
        {
            try
            {
                var vipclients = _vipclientRepository.GetAllVipClients();

                if (vipclients == null)
                {
                    Console.WriteLine("No se encontraron clientes vip.");
                    return new List<VipClientResponse>();
                }

                return VipClientProfile.ToVipClientResponseList(vipclients);
            }
            catch (Exception e)
            {
                Console.WriteLine("Hay un error en la clase: " + e.Message);
                throw;
            }
        }

        public VipClientResponse? GetVipClientById(int id)
        {
            var vipclient = _vipclientRepository.GetVipClientById(id);

            if (vipclient != null)
            {
                return VipClientProfile.ToVipClientResponse(vipclient);
            }

            return null;
        }

        public void CreateVipClient(VipClientRequest entity)
        {
            var vipclientEntity = VipClientProfile.ToVipClientEntity(entity);
            _vipclientRepository.AddVipClient(vipclientEntity);
        }


        public bool UpdateVipClient(int id, VipClientRequest vipclient)
        {
            var vipclientEntity = _vipclientRepository.GetVipClientById(id);

            if (vipclientEntity != null)
            {

                if (!string.IsNullOrEmpty(vipclient.UserName) && vipclient.UserName != "string")
                {
                    vipclientEntity.UserName = vipclient.UserName;
                }

                if (!string.IsNullOrEmpty(vipclient.Password) && vipclient.Password != "string")
                {
                    vipclientEntity.Password = vipclient.Password;
                }

                if (!string.IsNullOrEmpty(vipclient.FirstName) && vipclient.FirstName != "string")
                {
                    vipclientEntity.FirstName = vipclient.FirstName;
                }

                if (!string.IsNullOrEmpty(vipclient.LastName) && vipclient.LastName != "string")
                {
                    vipclientEntity.LastName = vipclient.LastName;
                }

                if (vipclient.Dni != 0)
                {
                   vipclientEntity.Dni = vipclient.Dni;
                }

                if (!string.IsNullOrEmpty(vipclient.Email) && vipclient.Email != "string")
                {
                    vipclientEntity.Email = vipclient.Email;
                }


                _vipclientRepository.UpdateVipClient(vipclientEntity);
                return true;
            }

            return false;
        }


        public bool DeleteVipClient(int id)
        {
            var vipclient = _vipclientRepository.GetVipClientById(id);

            if (vipclient != null)
            {

                //var meetings = _meetingService.GetMeetingsByVipClient(id);
                //foreach (var meeting in meetings)
                //{
                //    _meetingService.DeleteMeeting(meeting.Id);
                //}


                _vipclientRepository.DeleteVipClient(vipclient);

                return true;
            }

            return false;
        }
    }
}
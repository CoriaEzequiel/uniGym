using Application.Interfaces;
using Application.Models.Request;
using Application.Models.Response;
using Application.Mappings;
using Domain.Interfaces;

namespace Application.Services
{
    public class MeetingService : IMeetingService
    {
        private readonly IMeetingRepository _meetingRepository;
        private readonly IVipClientRepository _vipclientRepository;
        private readonly IProfessorRepository _professorRepository;

        public MeetingService(IMeetingRepository meetingRepository,
                              IVipClientRepository vipclientRepository,
                              IProfessorRepository professorRepository)
        {
            _meetingRepository = meetingRepository;
            _vipclientRepository = vipclientRepository;
            _professorRepository = professorRepository;
        }

        public List<MeetingResponse> GetAllMeetings()
        {
            try
            {
                var meetings = _meetingRepository.GetMeetings();
                return MeetingProfile.ToMeetingResponse(meetings);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al obtener todas las reuniones: {e.Message}");
                throw new Exception(e.Message);
            }
        }

        public MeetingResponse? GetMeetingById(int id)
        {
            var meeting = _meetingRepository.GetMeetingById(id);
            if (meeting != null)
            {
                return MeetingProfile.ToMeetingResponse(meeting);
            }
            return null;
        }

        public List<MeetingResponse> GetMeetingsByProfessor(int professorId)
        {
            var meetings = _meetingRepository.GetMeetingsByProfessor(professorId);
            return MeetingProfile.ToMeetingResponse(meetings);
        }

        public List<MeetingResponse> GetMeetingsByVipClient(int vipclientId)
        {
            var meetings = _meetingRepository.GetMeetingsByVipClient(vipclientId);
            return MeetingProfile.ToMeetingResponse(meetings);
        }

        public void CreateMeeting(MeetingRequest meetingRequest)
        {
            var vipclient = _vipclientRepository.GetVipClientById(meetingRequest.VipClientId);
            var professor = _professorRepository.GetProfessorById(meetingRequest.ProfessorId);


            if (vipclient == null)
            {
                throw new Exception($" VipCLient with ID {meetingRequest.VipClientId} not found.");
            }

            if (professor == null)
            {
                throw new Exception($"Professor with ID {meetingRequest.ProfessorId} not found.");
            }


            var meetingEntity = MeetingProfile.ToMeetingEntity(meetingRequest, vipclient, professor);


            _meetingRepository.AddMeeting(meetingEntity);
        }

        public bool UpdateMeeting(int id, MeetingRequest meeting)
        {
            var meetingEntity = _meetingRepository.GetMeetingById(id);

            if (meetingEntity != null)
            {
                var vipclientRepository = _vipclientRepository;
                var professorRepository = _professorRepository;

                MeetingProfile.ToMeetingEntityUpdate(meetingEntity, meeting, vipclientRepository, professorRepository);

                _meetingRepository.UpdateMeeting(meetingEntity);
                return true;
            }

            return false;
        }

        public bool DeleteMeetingsByProfessor(int professorId)
        {
            // Obtengo todas las reuniones del profe
            var meetings = _meetingRepository.GetMeetingsByProfessor(professorId);

            // Pra eliminar cada una y no dejar registros.
            foreach (var meeting in meetings)
            {
                _meetingRepository.DeleteMeeting(meeting);
            }

            return true; // Asumiendo que todas las eliminaciones fueron exitosas
        }

        public bool DeleteMeeting(int id)
        {
            var meetingToDelete = _meetingRepository.GetMeetingById(id);
            if (meetingToDelete == null)
            {
                return false; 
            }

            _meetingRepository.DeleteMeeting(meetingToDelete);
            return true; 
        }
    }
}


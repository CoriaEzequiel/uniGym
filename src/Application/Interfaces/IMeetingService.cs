using Application.Models.Request;
using Application.Models.Response;

namespace Application.Interfaces
{
    public interface IMeetingService
    {
        List<MeetingResponse> GetAllMeetings();
        MeetingResponse? GetMeetingById(int id);
        List<MeetingResponse> GetMeetingsByProfessor(int professionalId);
        List<MeetingResponse> GetMeetingsByVipClient(int customerId);
        void CreateMeeting(MeetingRequest meeting);
        bool UpdateMeeting(int id, MeetingRequest meeting);
        bool DeleteMeetingsByProfessor(int id);
        bool DeleteMeeting(int id);
    }
}
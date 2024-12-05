using Application.Models.Request;
using Application.Models.Response;
using Domain.Entities; // Asegúrate de que este using está presente
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Mappings
{
    public static class MeetingProfile
    {
        public static MeetingResponse ToMeetingResponse(Meeting meeting)
        {
            return new MeetingResponse
            {
                Id = meeting.Id,
                Date = meeting.Date,
                VipClientId = meeting.VipClient?.Id ?? 0,
                ProfessorId = meeting.Professor?.Id ?? 0 
            };
        }

        public static List<MeetingResponse> ToMeetingResponse(List<Meeting> meetings)
        {
            return meetings.Select(ToMeetingResponse).ToList();
        }

        public static Meeting ToMeetingEntity(MeetingRequest meetingRequest, Domain.Entities.VipClient vipClient, Professor professor) 
        {
            if (vipClient == null) throw new Exception("VipClient not found.");
            if (professor == null) throw new Exception("Professor not found.");

            return new Meeting
            {
                Date = meetingRequest.Date,
                VipClient = vipClient,
                Professor = professor
            };
        }

        public static void ToMeetingEntityUpdate(
            Meeting meetingEntity,
            MeetingRequest meetingRequest,
            IVipClientRepository vipClientRepository,
            IProfessorRepository professorRepository)
        {
            meetingEntity.Date = meetingRequest.Date;

            var vipClient = vipClientRepository.GetVipClientById(meetingRequest.VipClientId);
            if (vipClient == null) throw new Exception($"VipClient with ID {meetingRequest.VipClientId} not found.");
            meetingEntity.VipClient = vipClient;

            var professor = professorRepository.GetProfessorById(meetingRequest.ProfessorId);
            if (professor == null) throw new Exception($"Professor with ID {meetingRequest.ProfessorId} not found.");
            meetingEntity.Professor = professor;
        }
    }
}

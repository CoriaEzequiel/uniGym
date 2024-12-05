using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class MeetingRepository : IMeetingRepository
{
    private readonly uniContext _context;

    public MeetingRepository(uniContext context)
    {
        _context = context;
    }

    public List<Meeting> GetMeetings()
    {
        return _context.Meetings
            .Include(meeting => meeting.VipClient)
            .Include(meeting => meeting.Professor)
            .ToList();
    }


    public Meeting? GetMeetingById(int id)
    {
        return _context.Meetings
            .Include(x => x.VipClient)
            .Include(x => x.Professor)
            .FirstOrDefault(x => x.Id == id);
    }

    public List<Meeting> GetMeetingsByProfessor(int professorId)
    {
        return _context.Meetings
            .Include(meeting => meeting.VipClient)
            .Include(meeting => meeting.Professor)
            .Where(meeting => meeting.Professor.Id == professorId)
            .ToList();
    }

    public List<Meeting> GetMeetingsByVipClient(int vipclientId)
    {
        return _context.Meetings
            .Include(meeting => meeting.VipClient)
            .Include(meeting => meeting.Professor)
            .Where(meeting => meeting.VipClient.Id == vipclientId)
            .ToList();
    }

    public void AddMeeting(Meeting entity)
    {
        _context.Meetings.Add(entity);
        _context.SaveChanges();
    }

    public void UpdateMeeting(Meeting entity)
    {
        _context.Meetings.Update(entity);
        _context.SaveChanges();
    }

    public void DeleteMeeting(Meeting meeting)
    {
        _context.Meetings.Remove(meeting);
        _context.SaveChanges();
    }
}
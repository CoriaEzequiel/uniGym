namespace Domain.Entities;

public class Meeting
{
    public int Id { get; set; }

    public DateTime Date { get; set; }

    public VipClient VipClient { get; set; } = new();

    public Professor PRofessor { get; set; } = new();
}
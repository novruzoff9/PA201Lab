namespace TicketSystem.Models;

public class Event
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public decimal Price { get; set; }
    public List<Ticket>? Tickets { get; set; }
    public override string ToString()
    {
        return $"{Id}: {Name} on {Date.ToShortDateString()} - Price: {Price:C}";
    }
}

namespace TicketSystem.Services;

public interface ITicketService
{
    Task CreateTicketAsync(int eventId);
    Task UseTicketAsync(int ticketId);
}

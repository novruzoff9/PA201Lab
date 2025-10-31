using Microsoft.EntityFrameworkCore;
using TicketSystem.Data;
using TicketSystem.Exceptions;
using TicketSystem.Models;

namespace TicketSystem.Services;

public class TicketService : ITicketService
{
    private readonly AppDbContext appDbContext = new();
    private readonly IEventService eventService = new EventService();
    public async Task CreateTicketAsync(int eventId)
    {
        var @event = await eventService.GetByIdAsync(eventId);
        if(@event.Date < DateTime.Now)
        {
            throw new EventExpiredException("Cannot create ticket for expired event.");
        }
        var ticket = new Ticket
        {
            EventId = eventId,
            Price = @event.Price,
        };
        await appDbContext.Tickets.AddAsync(ticket);
        await appDbContext.SaveChangesAsync();
    }

    public async Task UseTicketAsync(int ticketId)
    {
        var ticket = await appDbContext.Tickets.SingleOrDefaultAsync(t => t.Id == ticketId);
        if(ticket == null)
            throw new ArgumentException("Ticket not found.");
        if (ticket.IsUsed)
            throw new TicketAlreadyUsedException("Ticket has already been used.");
        ticket.IsUsed = true;
        appDbContext.Tickets.Update(ticket);
        await appDbContext.SaveChangesAsync();
    }
}

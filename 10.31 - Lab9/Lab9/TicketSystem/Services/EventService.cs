using Microsoft.EntityFrameworkCore;
using TicketSystem.Data;
using TicketSystem.Models;

namespace TicketSystem.Services;

public class EventService : IEventService
{
    private readonly AppDbContext appDbContext = new();
    public async Task CreateAsync(Event @event)
    {
        await appDbContext.Events.AddAsync(@event);
        await appDbContext.SaveChangesAsync();
    }

    public async Task<List<Event>> GetAllAsync()
    {
        return await appDbContext.Events.ToListAsync();
    }

    public async Task<Event> GetByIdAsync(int id)
    {
        var @event = await appDbContext.Events.SingleOrDefaultAsync(e => e.Id == id);
        if (@event == null)
            throw new Exception($"Event with id {id} not found.");
        return @event;
    }

    public async Task RemoveAsync(int id)
    {
        var @event = await GetByIdAsync(id);
        appDbContext.Events.Remove(@event);
        await appDbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Event @event)
    {
        appDbContext.Events.Update(@event);
        await appDbContext.SaveChangesAsync();
    }
}

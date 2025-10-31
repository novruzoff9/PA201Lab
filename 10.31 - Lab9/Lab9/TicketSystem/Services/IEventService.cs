using TicketSystem.Models;

namespace TicketSystem.Services;
public interface IEventService
{
    Task<List<Event>> GetAllAsync();
    Task CreateAsync(Event @event);
    Task<Event> GetByIdAsync(int id);
    Task UpdateAsync(Event @event);
    Task RemoveAsync(int id);
}

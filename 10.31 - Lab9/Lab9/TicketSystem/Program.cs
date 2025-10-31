using Microsoft.EntityFrameworkCore;
using TicketSystem.Data;
using TicketSystem.Models;
using TicketSystem.Services;

IEventService eventService = new EventService();
ITicketService ticketService = new TicketService();
AppDbContext appDbContext = new();

//Event @event = new()
//{
//    Name = "Concert A",
//    Date = new DateTime(2024, 12, 15, 12, 0, 0),
//    Price = 50.00m
//};
//await eventService.CreateAsync(@event);

//var events = await eventService.GetAllAsync();
//foreach (var ev in events)
//{
//    Console.WriteLine(ev);
//}
//Event updatedEvent = await eventService.GetByIdAsync(1);
//updatedEvent.Name = "Updated Concert A";
//await eventService.UpdateAsync(updatedEvent);

//try
//{
//    //await ticketService.CreateTicketAsync(8);
//    await ticketService.UseTicketAsync(4);
//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex.Message);
//}
var events = await appDbContext.Events
    .Include(e => e.Tickets)
    .OrderByDescending(e => e.Tickets.Count())
    .Select(e => new
    {
        e.Name,
        TicketCount = e.Tickets.Count()
    })
    .ToListAsync();
foreach (var ev in events)
{
    Console.WriteLine(ev);
}
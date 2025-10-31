using Microsoft.EntityFrameworkCore;
using TicketSystem.Models;

namespace TicketSystem.Data;

public class AppDbContext : DbContext
{
    public DbSet<Event> Events { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("server=DESKTOP-93814R2;database=PA201TicketSystem;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=True");
    }
}

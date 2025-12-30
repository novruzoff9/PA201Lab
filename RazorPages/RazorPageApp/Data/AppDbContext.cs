using Microsoft.EntityFrameworkCore;
using RazorPageApp.Models;

namespace RazorPageApp.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
}

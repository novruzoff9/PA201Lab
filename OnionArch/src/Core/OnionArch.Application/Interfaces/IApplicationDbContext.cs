using Microsoft.EntityFrameworkCore;
using OnionArch.Domain.Entities;

namespace OnionArch.Application.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Category> Categories { get; set; }
    Task<int> SaveChangesAsync();
}

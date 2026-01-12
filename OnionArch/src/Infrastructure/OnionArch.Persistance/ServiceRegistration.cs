using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnionArch.Application.Interfaces;
using OnionArch.Application.Services.Concretes;
using OnionArch.Application.Services.Interfaces;
using OnionArch.Persistance.Data;

namespace OnionArch.Persistance;

public static class ServiceRegistration
{
    extension(IServiceCollection services)
    {
        public void AddPersistenceServices(IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
        }
    }
}

using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnionArch.Application.Profiles;
using OnionArch.Application.Services.Concretes;
using OnionArch.Application.Services.Interfaces;

namespace OnionArch.Application;

public static class ServiceRegistration
{
    extension(IServiceCollection services)
    {
        public void AddApplicationServices()
        {
            services.AddAutoMapper(cfg => { cfg.AddProfile<MapperProfile>(); });
            services.AddValidatorsFromAssembly(typeof(ServiceRegistration).Assembly);
            services.AddScoped<ICategoryService, CategoryService>();
        }
    }
}

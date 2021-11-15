using BuiltCode.Domain.Abstractions;
using BuiltCode.Domain.Services;
using BuiltCode.Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace BuiltCode.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IParceiroService, ParceiroService>();
            services.AddScoped<IMedicoService, MedicoService>();
            services.AddScoped<IPacienteService, PacienteService>();

            services.AddAutoMapper(typeof(MappingProfiles));

            return services;
        }
    }
}

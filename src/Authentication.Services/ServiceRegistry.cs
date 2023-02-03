namespace Authentication.Services
{
    using AutoMapper;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Authentication.Abstractions.Services;

    public static class ServiceRegistry
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient(typeof(ITokenService), typeof(TokenService));
            services.AddAutoMapper(typeof(ServiceRegistry));

            return services;
        }
    }
}


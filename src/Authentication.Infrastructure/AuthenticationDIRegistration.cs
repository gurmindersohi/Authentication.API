namespace Authentication.Infrastructure
{
    using Authentication.Services;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class AuthenticationDIRegistration
    {
        public static IServiceCollection AddAuthenticationServices(this IServiceCollection services)
        {
            services.AddServices();

            return services;
        }
    }
}


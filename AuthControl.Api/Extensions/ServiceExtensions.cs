using AuthControl.Application.Interfaces;
using AuthControl.Application.Services;
using AuthControl.Infrastructure.Data;
using AuthControl.Infrastructure.Repositories;

namespace AuthControl.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<DatabaseSeeder>();
            return services;
        }
    }
}

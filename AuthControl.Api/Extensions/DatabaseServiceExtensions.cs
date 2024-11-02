using AuthControl.Application.Interfaces;
using AuthControl.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;

namespace AuthControl.Api.Extensions
{
    public static class DatabaseServiceExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, string connectionString)
        {
            services.AddSingleton<IDapperContext>(new DapperContext(connectionString));
            return services;
        }
    }
}

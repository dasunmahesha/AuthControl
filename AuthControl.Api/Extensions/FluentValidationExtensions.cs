using FluentValidation;
using FluentValidation.AspNetCore;

namespace AuthControl.Api.Extensions
{
    public static class FluentValidationExtensions
    {
        public static IServiceCollection AddCustomFluentValidation(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<AuthControl.Application.Validators.LoginRequestValidator>();
            services.AddValidatorsFromAssemblyContaining<AuthControl.Application.Validators.UserRegistrationValidator>();
            return services;
        }
    }
}

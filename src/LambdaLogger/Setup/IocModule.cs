using Microsoft.Extensions.DependencyInjection;

namespace LambdaLogger.Setup
{
    public static class IocModule
    {
        public static IServiceCollection AddLoggingService(this IServiceCollection services)
        {
            services.AddScoped<ILogger, Logger>();
            return services;
        }
    }
}

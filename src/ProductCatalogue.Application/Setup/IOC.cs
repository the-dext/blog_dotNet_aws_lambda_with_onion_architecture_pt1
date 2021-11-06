using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ProductCatalogue.Domain.Repositories;
using ProductCatalogue.Persistence.Setup;

namespace ProductCatalogue.Application.Setup
{
    public static class Ioc
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection serviceCollection)
        {
            ConfigureServices(serviceCollection);
            return serviceCollection;
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            // TODO: Will autofac work instead?

            // add mediatr types from this assembly
            services.AddMediatR(Assembly.GetExecutingAssembly())
                .AddAutoMapper(Assembly.GetExecutingAssembly())
                .AddPersistenceServices();

            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}

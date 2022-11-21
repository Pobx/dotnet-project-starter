using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.EFCore.PostgreSQL;
public static class PostgreSQLInfrastructureRegistration
{
    public static IServiceCollection AddPostgreSQLInfrastructureRegistration(this IServiceCollection services, IConfiguration configuration)
    {
        var migrationsAssembly = configuration["ProjectName"]?.ToString() ?? throw new ArgumentNullException("Project name not setting");

        services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(
            configuration.GetConnectionString("DefaultConnectionPostgreSQL"),
            b => b.MigrationsAssembly(migrationsAssembly))
        );

        return services;
    }
}

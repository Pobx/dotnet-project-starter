using Domain.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.EFCore.SqlServer;
public static class SqlServerInfrastructureRegistration
{
    public static IServiceCollection AddSqlServerInfrastructureRegistration(this IServiceCollection services, IConfiguration configuration)
    {
        var migrationsAssembly = configuration["ProjectName"]?.ToString() ?? throw new ArgumentNullException("Project name not setting");
        
        services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(
            configuration.GetConnectionString(DatabaseNames.DefaultConnectionSqlServer),
            b => b.MigrationsAssembly(migrationsAssembly))
        );

        return services;
    }
}

using DataAccess.EFCore.PostgreSQL.Repositories;
using Domain.Interfaces;
using Domain.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.EFCore.PostgreSQL;
public static class PostgreSQLInfrastructureRegistration
{
    public static IServiceCollection AddPostgreSQLInfrastructureRegistration(this IServiceCollection services, IConfiguration configuration)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        var migrationsAssembly = configuration["ProjectName"]?.ToString() ?? throw new ArgumentNullException("Project name not setting");
        
        services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(
            configuration.GetConnectionString(DatabaseNames.DefaultConnectionPostgreSQL),
            b => b.MigrationsAssembly(migrationsAssembly))
        );

        #region Repositories
        services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddTransient<IExampleRepository, ExampleRepository>();
        #endregion

        services.AddTransient<IUnitOfWork, UnitOfWork>();

        return services;
    }
}

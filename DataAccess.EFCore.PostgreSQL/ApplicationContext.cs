using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.EFCore.PostgreSQL;
public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
    }

    public DbSet<Example> Examples { get; set; }
}


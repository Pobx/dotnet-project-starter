using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.EFCore.SqlServer
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }
        public DbSet<Example> Examples => Set<Example>();
    }
}


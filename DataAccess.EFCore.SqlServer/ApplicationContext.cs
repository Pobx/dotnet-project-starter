using System;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.EFCore.SqlServer
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public DbSet<Example> Examples { get; set; }
    }
}


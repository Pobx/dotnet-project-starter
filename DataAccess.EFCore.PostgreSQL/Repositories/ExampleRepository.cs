using System;
using Domain.Entities;
using Domain.Interfaces;

namespace DataAccess.EFCore.PostgreSQL.Repositories
{
    public class ExampleRepository : GenericRepository<Example>, IExampleRepository
    {
        public ExampleRepository(ApplicationContext context) : base(context)
        {
        }
    }
}


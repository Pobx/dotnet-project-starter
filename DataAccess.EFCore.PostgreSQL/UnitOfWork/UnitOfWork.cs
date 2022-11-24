using System;
using DataAccess.EFCore.PostgreSQL.Repositories;
using Domain.Interfaces;

namespace DataAccess.EFCore.PostgreSQL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;
        public UnitOfWork(ApplicationContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            Examples = new ExampleRepository(_context);
        }

        public IExampleRepository Examples { get; private set; }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}


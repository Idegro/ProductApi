using ProductApi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductApi.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;
        private IProductRepository _products;

        public UnitOfWork(ApplicationContext context)
        {
            _context = context;
        }

        public IProductRepository productRepository { get { return _products = _products ?? new ProductRepository(_context); } }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void RollBack()
        {
            _context.Dispose();
        }
    }
}

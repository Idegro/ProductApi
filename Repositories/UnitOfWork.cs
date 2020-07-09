using ProductApi.Data;
using ProductApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductApi.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;
        private IBaseRepository<Product> _products;

        public UnitOfWork(ApplicationContext context)
        {
            _context = context;
        }

        public IBaseRepository<Product> productRepository { get { return _products = _products ?? new BaseRepository<Product>(_context, _context.Product); } }

        public bool Commit()
        {
            _context.SaveChanges();
            return true;
        }

        public bool RollBack()
        {
            _context.Dispose();
            return false;
        }

        public void RemoveFromContext(object o)
        {
            _context.Remove(o);
        }
    }
}

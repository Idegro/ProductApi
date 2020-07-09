using ProductApi.Data;
using ProductApi.Models;

namespace ProductApi.Repositories
{
    public interface IUnitOfWork
    {
        IBaseRepository<Product> productRepository { get; }

        bool Commit();
        bool RollBack();
        void RemoveFromContext(object o);
    }
}
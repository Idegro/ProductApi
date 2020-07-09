using ProductApi.Data;

namespace ProductApi.Repositories
{
    public interface IUnitOfWork
    {
        IProductRepository productRepository { get; }

        void Commit();
        void RollBack();
    }
}
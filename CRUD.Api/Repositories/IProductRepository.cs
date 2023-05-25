using CRUD.Api.Domain;

namespace CRUD.Api.Repositories
{
    public interface IProductRepository
    {
        ValueTask InsertProduct(Product product);
        ValueTask DeleteProduct(int id);
        ValueTask<List<Product>> GetProducts();
        ValueTask<Product> GetProduct(int id);
        ValueTask UpdateProduct(int id, Product product);
    }
}

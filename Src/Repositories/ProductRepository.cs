using Store.Domain;
using Microsoft.EntityFrameworkCore;
using Infrastructure;

namespace Store.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext _context;

        public ProductRepository(StoreContext context) => _context = context;

        public async ValueTask DeleteProduct(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(product => product.Id == id);
            if (product is null)
                throw new ArgumentNullException("Product Not Founded!");
            _context.Products.Remove(product!);
            await _context.SaveChangesAsync();
        }

        public async ValueTask<Product> GetProduct(int id) =>
            await _context.Products.FirstOrDefaultAsync(product => product.Id == id);

        public async ValueTask<List<Product>> GetProducts() =>
            await _context.Products.ToListAsync();

        public async ValueTask InsertProduct(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async ValueTask UpdateProduct(int id, Product product)
        {
            var updateProduct = await _context.Products.FirstOrDefaultAsync(
                product => product.Id == id
            );
            updateProduct!.SetName(product.Name);
            updateProduct.SetDescription(product.Description);
            updateProduct.SetPrice(product.Price);
            updateProduct.GiveRate((int)product.Rate);
            updateProduct.UpdateAttachments(product.Attachments);
            _context.Entry(updateProduct).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}

using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDbExample.Models;

namespace MongoDbExample.Services
{
    public class ProductService
    {
        private readonly IMongoCollection<Product> _products;

        public ProductService(IOptions<MongoDbSettings> settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.Value.DatabaseName);
            _products = database.GetCollection<Product>(settings.Value.ProductsCollectionName);
        }

        public async Task<List<Product>> GetAllAsync() =>
            await _products.Find(_ => true).ToListAsync();    // _ => true  means no filter adds

        public async Task<Product?> GetByIdAsync(string id) =>
            await _products.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Product product) =>
            await _products.InsertOneAsync(product);

        public async Task UpdateAsync(string id, Product product) =>
            await _products.ReplaceOneAsync(x => x.Id == id, product);

        public async Task DeleteAsync(string id) =>
            await _products.DeleteOneAsync(x => x.Id == id);

        public async Task<List<Product>> GetByCategoryAsync(string category) =>
            await _products.Find(x => x.Category == category).ToListAsync();

        public async Task<List<Product>> GetInStockAsync() =>
            await _products.Find(x => x.InStock == true).ToListAsync();
    }
}

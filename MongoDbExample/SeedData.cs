using MongoDbExample.Models;
using MongoDbExample.Services;

namespace MongoDbExample
{
    public static class SeedData
    {
        public static async Task SeedAsync(ProductService productService)
        {
            var products = await productService.GetAllAsync();
            if (products.Count == 0)
            {
                var sampleProducts = new List<Product>
                {
                    new Product
                    {
                        Name = "Laptop",
                        Price = 999.99m,
                        Category = "Electronics",
                        InStock = true
                    },
                    new Product
                    {
                        Name = "Coffee Mug",
                        Price = 12.50m,
                        Category = "Kitchenware",
                        InStock = true
                    },
                    new Product
                    {
                        Name = "Desk Chair",
                        Price = 199.00m,
                        Category = "Furniture",
                        InStock = false
                    }
                };

                foreach (var product in sampleProducts)
                {
                    await productService.CreateAsync(product);
                }
            }
        }
    }
}

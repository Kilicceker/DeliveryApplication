using DeliveryApp.Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DeliveryApp.Data.EntityFramework.Context
{
    public static class AppContextSeed
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            if (!context.ProductBrands.Any())
            {
                var brandsData =
                    File.ReadAllText("../DeliveryApp.Data/SeedData/brands.json");

                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                foreach (var item in brands)
                {
                    context.ProductBrands.Add(item);
                }

                await context.SaveChangesAsync();
            }
            if (!context.ProductTypes.Any())
            {
                var typesData =
                    File.ReadAllText("../DeliveryApp.Data/SeedData/types.json");

                var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                foreach (var item in types)
                {
                    context.ProductTypes.Add(item);
                }

                await context.SaveChangesAsync();
            }

            if (!context.Products.Any())
            {
                var productsData =
                    File.ReadAllText("../DeliveryApp.Data/SeedData/products.json");

                var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                foreach (var item in products)
                {
                    context.Products.Add(item);
                }

                await context.SaveChangesAsync();
            }
            if (!context.Comments.Any())
            {
                var commentsData =
                    File.ReadAllText("../DeliveryApp.Data/SeedData/comments.json");

                var comments = JsonSerializer.Deserialize<List<Comment>>(commentsData);

                foreach (var item in comments)
                {
                    context.Comments.Add(item);
                }

                await context.SaveChangesAsync();
            }
        }
    }
}

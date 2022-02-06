using DeliveryApp.Core.Entities.Concrete;
using DeliveryApp.Core.Repositories.Abstract;
using DeliveryApp.Data.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Data.Repositories
{
    public class ProductRepository:RepositoryBase<Product>,IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {

        }
    }
}

using DeliveryApp.Core.Entities.Concrete;
using DeliveryApp.Core.Repositories.Abstract;
using DeliveryApp.Data.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Data.Repositories
{
    class ProductTypeRepository: RepositoryBase<ProductType>, IProductTypeRepository
    {
        public ProductTypeRepository(AppDbContext context) :base(context)
        {
           
        }
    }
}

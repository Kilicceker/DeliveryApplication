using DeliveryApp.Core.Repositories.Abstract;
using DeliveryApp.Core.UnitOfWorks;
using DeliveryApp.Data.EntityFramework.Context;
using DeliveryApp.Data.Repositories;
using System.Threading.Tasks;

namespace DeliveryApp.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private ProductRepository _productRepository;
        private ProductBrandRepository _brandRepository;
        private ProductTypeRepository _productTypeRepository;
        private CommentRepository _commentRepository;
        private OrderRepository _orderRepository;
        private AddressRepository _addressRepository;
        public IProductRepository Products => _productRepository ??= new ProductRepository(_context);

        public IProductBrandRepository Brands => _brandRepository ??= new ProductBrandRepository(_context);

        public IProductTypeRepository Type => _productTypeRepository ??= new ProductTypeRepository(_context);

        public IProductBrandRepository Brand => _brandRepository ??= new ProductBrandRepository(_context);
        public ICommentRepository Comment => _commentRepository ??= new CommentRepository(_context);

        public IOrderRepository Order => _orderRepository ??= new OrderRepository(_context);

        public IAddressRepository Address => _addressRepository ??= new AddressRepository(_context);

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

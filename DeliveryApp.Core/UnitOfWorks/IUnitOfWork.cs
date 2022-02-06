using DeliveryApp.Core.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryApp.Core.UnitOfWorks
{
    public interface IUnitOfWork
    {
        IProductRepository Products { get; }
        IProductBrandRepository Brand { get; }
        IProductTypeRepository Type { get; }
        ICommentRepository Comment { get; }
        IOrderRepository Order { get; }
        IAddressRepository Address { get; }
        Task CommitAsync();
        void Commit();

    }
}

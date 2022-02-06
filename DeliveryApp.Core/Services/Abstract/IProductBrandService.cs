using DeliveryApp.Core.Dtos;
using DeliveryApp.Shared.Result;
using DeliveryApp.Shared.Result.Abstract;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeliveryApp.Core.Services.Abstract
{
    public interface IProductBrandService
    {
        Task<IDataResult<ProductBrandDto>> GetAsync(int brandId);
        Task<IDataResult<IList<ProductBrandDto>>> GetAllAsync();
        Task<IResult> AddAsync(ProductBrandAddDto brandAddDto);
        Task<IResult> AddRangeAsync(IList<ProductBrandAddDto> brands);
        Task<IResult> UpdateAsync(ProductBrandUpdateDto brandUpdateDto);
        Task<IResult> DeleteAsync(int id);
        Task<IDataResult<ProductBrandWithProductsDto>> GetWithProducts(int id);
    }
}

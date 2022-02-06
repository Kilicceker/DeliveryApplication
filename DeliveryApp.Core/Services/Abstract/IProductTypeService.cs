using DeliveryApp.Core.Dtos;
using DeliveryApp.Shared.Result;
using DeliveryApp.Shared.Result.Abstract;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeliveryApp.Core.Services.Abstract
{
    public interface IProductTypeService
    {
        Task<IDataResult<ProductTypeDto>> GetAsync(int typeId);
        Task<IDataResult<IList<ProductTypeDto>>> GetAllAsync();
        Task<IResult> AddAsync(ProductTypeAddDto typeAddDto);
        Task<IResult> AddRangeAsync(IList<ProductTypeAddDto> types);
        Task<IResult> UpdateAsync(ProductTypeUpdateDto updateTypeDto);
        Task<IResult> DeleteAsync(int id);
        Task<IDataResult<ProductTypeWithProductsDto>> GetWithProducts(int id);
    }
}

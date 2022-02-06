using DeliveryApp.Core.Dtos;
using DeliveryApp.Shared.Result.Abstract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeliveryApp.Core.Services.Abstract
{
    public interface IProductService
    {
        Task<IDataResult<ProductDto>> GetAsync(int productId);
        Task<IDataResult<IList<ProductDto>>> GetAllAsync();
        Task<IDataResult<IList<ProductDto>>> GetAllWithPagesAsync(int? productTypeId,int? productBrandId,int currentPage,int pageSize=5,bool isAscending=false);
        Task<IDataResult<ProductDto>> GetProductWithComments(int productId);
        Task<IResult> AddAsync(ProductAddDto productAddDto);
        Task<IResult> AddRangeAsync(IList<ProductAddDto> products);
        Task<IResult> UpdateAsync(ProductUpdateDto updateDto);
        Task<IResult> UpdateRatingAsync(int productId,int rating);
        Task<IResult> DeleteAsync(int id);
        Task<IDataResult<ProductListDto>> SearchAsync(string keyword, int currentPage, int pageSize = 5, bool isAscending = false);
    }
}

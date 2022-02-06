using AutoMapper;
using DeliveryApp.Core.Dtos;
using DeliveryApp.Core.Entities.Concrete;
using DeliveryApp.Core.Services.Abstract;
using DeliveryApp.Core.UnitOfWorks;
using DeliveryApp.Shared.Result;
using DeliveryApp.Shared.Result.Abstract;
using DeliveryApp.Shared.Result.ComplexTypes;
using DeliveryApp.Shared.Result.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeliveryApp.Services.Concrete
{
    public class ProductBrandService : IProductBrandService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductBrandService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IResult> AddAsync(ProductBrandAddDto brandAddDto)
        {
            var brand = _mapper.Map<ProductBrand>(brandAddDto);
            await _unitOfWork.Brand.AddAsync(brand);
            await _unitOfWork.CommitAsync();
            return new Result(ResultStatus.Succes,$"{brand.Name} has been added successfully");
        }

        
        public async Task<IDataResult<ProductBrandDto>> GetAsync(int brandId)
        {
            var brand = await _unitOfWork.Brand.GetAsync(x => x.Id == brandId); 
            if (brand == null)
                return new DataResult<ProductBrandDto>(ResultStatus.Error, "No brands found with specified criteria",null);
            var brandToReturnDto = _mapper.Map<ProductBrandDto>(brand);                                                            
            return new DataResult<ProductBrandDto>(ResultStatus.Succes, brandToReturnDto);
        }

        public async Task<IDataResult<IList<ProductBrandDto>>> GetAllAsync()
        {
            var brand = await _unitOfWork.Brand.GetAllAsync();
            if (brand == null)
                return new DataResult<IList<ProductBrandDto>>(ResultStatus.Error, "No brands found with specified criteria", null);
            var brandToList = _mapper.Map<IList<ProductBrandDto>>(brand);
            return new DataResult<IList<ProductBrandDto>>(ResultStatus.Succes,brandToList);
        }

        public async Task<IResult> UpdateAsync(ProductBrandUpdateDto brandUpdateDto)
        {
            var brand = _mapper.Map<ProductBrand>(brandUpdateDto);
            await _unitOfWork.Brand.UpdateAsync(brand);
            await _unitOfWork.CommitAsync();
            return new Result(ResultStatus.Succes,$"{brand.Name} has been updated successfully");
        }
        public async Task<IResult> DeleteAsync(int id)
        {
            var brand = await _unitOfWork.Brand.GetAsync(x => x.Id == id);
            if (brand == null)
                return new Result(ResultStatus.Error, "No brands found with specified criteria");
            await _unitOfWork.Brand.DeleteAsync(brand);
            await _unitOfWork.CommitAsync();
            return new Result(ResultStatus.Succes, $"{brand.Name} has been deleted successfully");
        }

        public async Task<IDataResult<ProductBrandWithProductsDto>> GetWithProducts(int id)
        {
            var types = await _unitOfWork.Brand.GetAsync(x => x.Id == id, x => x.Products);
            if (types == null)
                return new DataResult<ProductBrandWithProductsDto>(ResultStatus.Error, "No brands found with specified criteria", null);
            var typesToList = _mapper.Map<ProductBrandWithProductsDto>(types);
            return new DataResult<ProductBrandWithProductsDto>(ResultStatus.Succes, typesToList);
        }

        public async Task<IResult> AddRangeAsync(IList<ProductBrandAddDto> brands)
        {
            var brandsToAdd = _mapper.Map<IList<ProductBrand>>(brands);
            await _unitOfWork.Brand.AddRangeAsync(brandsToAdd);
            await _unitOfWork.CommitAsync();
            return new Result(ResultStatus.Succes, "brands has been added successfully");
        }
    }
}

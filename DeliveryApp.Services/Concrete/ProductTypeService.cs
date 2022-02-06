using AutoMapper;
using DeliveryApp.Core.Dtos;
using DeliveryApp.Core.Entities.Concrete;
using DeliveryApp.Core.Services.Abstract;
using DeliveryApp.Core.UnitOfWorks;
using DeliveryApp.Shared.Result.Abstract;
using DeliveryApp.Shared.Result.ComplexTypes;
using DeliveryApp.Shared.Result.Concrete;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Services.Concrete
{
    public class ProductTypeService : IProductTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductTypeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IResult> AddAsync(ProductTypeAddDto typeAddDto)
        {
            var type = _mapper.Map<ProductType>(typeAddDto);
            await _unitOfWork.Type.AddAsync(type);
            await _unitOfWork.CommitAsync();
            return new Result(ResultStatus.Succes,$"{type.Name} has been added successfully");
        }

        public async Task<IDataResult<ProductTypeDto>> GetAsync(int typeId)
        {
            var type = await _unitOfWork.Type.GetAsync(x => x.Id == typeId); 
            if (type == null)
                return new DataResult<ProductTypeDto>(ResultStatus.Error, "No types found with specified criteria",null);
            var typeToReturnDto = _mapper.Map<ProductTypeDto>(type);                                                            
            return new DataResult<ProductTypeDto>(ResultStatus.Succes, typeToReturnDto);
        }

        public async Task<IDataResult<IList<ProductTypeDto>>> GetAllAsync()
        {
            var types = await _unitOfWork.Type.GetAllAsync();
            if (types.Count == 0)
                return new DataResult<IList<ProductTypeDto>>(ResultStatus.Error, "No types found with specified criteria", null);
            var typesToList = _mapper.Map<IList<ProductTypeDto>>(types);
            return new DataResult<IList<ProductTypeDto>>(ResultStatus.Succes, typesToList);
        }

        public async Task<IResult> UpdateAsync(ProductTypeUpdateDto updateTypeDto)
        {
            var type = _mapper.Map<ProductType>(updateTypeDto);
            await _unitOfWork.Type.UpdateAsync(type);
            await _unitOfWork.CommitAsync();
            return new Result(ResultStatus.Succes,$"{type.Name} has been updated successfully");
        }
        public async Task<IResult> DeleteAsync(int id)
        {
            var type = await _unitOfWork.Type.GetAsync(x => x.Id == id);
            if (type == null)
                return new Result(ResultStatus.Error, "No types found with specified criteria");
            await _unitOfWork.Type.DeleteAsync(type);
            await _unitOfWork.CommitAsync();
            return new Result(ResultStatus.Succes, $"{type.Name} has been deleted successfully");
        }

        public async Task<IDataResult<ProductTypeWithProductsDto>> GetWithProducts(int id)
        {
            var types = await _unitOfWork.Type.GetAsync(x => x.Id == id, x => x.Products);
            types.Products.OrderBy(x => x.Rating);
            if(types==null)
                return new DataResult<ProductTypeWithProductsDto>(ResultStatus.Error, "No types found with specified criteria", null);
            var typesToList = _mapper.Map<ProductTypeWithProductsDto>(types);
            return new DataResult<ProductTypeWithProductsDto>(ResultStatus.Succes, typesToList);
        }

        public async Task<IResult> AddRangeAsync(IList<ProductTypeAddDto> types)
        {
            var typeToAdd = _mapper.Map<IList<ProductType>>(types);
            await _unitOfWork.Type.AddRangeAsync(typeToAdd);
            await _unitOfWork.CommitAsync();
            return new Result(ResultStatus.Succes, "types has been added successfully");
        }
    }
}

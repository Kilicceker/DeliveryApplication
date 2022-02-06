using AutoMapper;
using DeliveryApp.Core.Dtos;
using DeliveryApp.Core.Entities.Concrete;
using DeliveryApp.Core.Services.Abstract;
using DeliveryApp.Core.UnitOfWorks;
using DeliveryApp.Shared.Result.Abstract;
using DeliveryApp.Shared.Result.ComplexTypes;
using DeliveryApp.Shared.Result.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DeliveryApp.Services.Concrete
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }

        public async Task<IResult> AddAsync(ProductAddDto productAddDto)
        {
            var product = _mapper.Map<Product>(productAddDto);
            await _unitOfWork.Products.AddAsync(product);
            await _unitOfWork.CommitAsync();
            return new Result(ResultStatus.Succes,$"{product.Name} has been added successfully");
        }

        public async Task<IDataResult<ProductDto>> GetAsync(int productId)
        {
            var product = await _unitOfWork.Products.GetAsync(x => x.Id == productId, x => x.ProductBrand, x => x.ProductType);
            if (product == null)
                return new DataResult<ProductDto>(ResultStatus.Error, "No products found with specified criteria",null);
            var productToReturnDto = _mapper.Map<ProductDto>(product);
            return new DataResult<ProductDto>(ResultStatus.Succes, productToReturnDto);
        }

        public async Task<IDataResult<IList<ProductDto>>> GetAllAsync()
        {
            var products = await _unitOfWork.Products.GetAllAsync(null, x => x.ProductBrand, x => x.ProductType);
            if (products == null)
                return new DataResult<IList<ProductDto>>(ResultStatus.Error, "No products found with specified criteria", null);
            var productsToList = _mapper.Map<IList<ProductDto>>(products);
            return new DataResult<IList<ProductDto>>(ResultStatus.Succes,productsToList);
        }

        public async Task<IResult> UpdateAsync(ProductUpdateDto updateDto)
        {
            var product = _mapper.Map<Product>(updateDto);
            await _unitOfWork.Products.UpdateAsync(product);
            await _unitOfWork.CommitAsync();
            return new Result(ResultStatus.Succes,$"{product.Name} has been updated successfully");
        }
        public async Task<IResult> DeleteAsync(int id)
        {
            var product = await _unitOfWork.Products.GetAsync(x => x.Id == id, x => x.ProductBrand, x => x.ProductType);
            if (product == null)
                return new Result(ResultStatus.Error, "No products found with specified criteria");
            await _unitOfWork.Products.DeleteAsync(product);
            await _unitOfWork.CommitAsync();
            return new Result(ResultStatus.Succes, $"{product.Name} has been deleted successfully");
        }

        public async Task<IDataResult<IList<ProductDto>>> GetAllWithPagesAsync(int? productTypeId, int? productBrandId, int currentPage, int pageSize = 5, bool isAscending = false)
        {
            IList<Product> products = new List<Product>();
            pageSize = pageSize > 20 ? 20 : pageSize;
            if (productTypeId ==null && productBrandId==null)
            {
                products = await _unitOfWork.Products.GetAllAsync(null, x => x.ProductBrand, x => x.ProductType);
            }
            else if (productTypeId != null && productBrandId == null)
            {
                products = await _unitOfWork.Products.GetAllAsync(x => x.ProductTypeId == productTypeId, x => x.ProductBrand, x => x.ProductType);
            }
            else if (productTypeId == null && productBrandId != null)
            {
                products = await _unitOfWork.Products.GetAllAsync(x => x.ProductBrandId == productBrandId, x => x.ProductBrand, x => x.ProductType);
            }
            else if (productTypeId != null && productBrandId != null)
            {
                products = await _unitOfWork.Products.GetAllAsync(x => x.ProductTypeId == productTypeId && x.ProductBrandId==productBrandId, x => x.ProductBrand, x => x.ProductType);
            }
            var sortedProducts = isAscending ? products.OrderBy(x => x.Rating).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList() :
                products.OrderByDescending(x => x.Rating).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
            var productsToReturn = _mapper.Map<IList<ProductDto>>(sortedProducts);
            return new DataResult<IList<ProductDto>>(ResultStatus.Succes, productsToReturn);
        }

        public async Task<IDataResult<ProductListDto>> SearchAsync(string keyword, int currentPage, int pageSize = 5, bool isAscending = false)
        {
            IList<Product> products = new List<Product>();
            pageSize = pageSize > 20 ? 20 : pageSize;
            if (string.IsNullOrWhiteSpace(keyword))
            {
                    products = await _unitOfWork.Products.GetAllAsync(null, x => x.ProductBrand, x => x.ProductType);
                var sortedProducts = isAscending ? products.OrderBy(x => x.Price).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList() :
                    products.OrderByDescending(x => x.Rating).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
                var productsToReturn = _mapper.Map<IList<ProductDto>>(sortedProducts);
                return new DataResult<ProductListDto>(ResultStatus.Succes, new ProductListDto
                {
                    Products = productsToReturn,
                    PageSize = pageSize,
                    IsAscending = isAscending,
                    TotalCount = productsToReturn.Count,
                    CurrentPage = currentPage
                });
            }
            else 
            {
                var searchedArticles = await _unitOfWork.Products.SearchAsync(new List<Expression<Func<Product, bool>>>
            {
                (p) => p.Name.Contains(keyword),
                (p) => p.Description.Contains(keyword),
                (p) => p.ProductBrand.Name.Contains(keyword),
                (p) => p.ProductType.Name.Contains(keyword)
            }, p => p.ProductType, p => p.ProductBrand);

                var sortedAndSearchedProducts = isAscending ? searchedArticles.OrderBy(x => x.Price).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList() :
                    searchedArticles.OrderByDescending(x => x.Price).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
                var productsToReturn = _mapper.Map<IList<ProductDto>>(sortedAndSearchedProducts);
                return new DataResult<ProductListDto>(ResultStatus.Succes, new ProductListDto
                {
                    Products = productsToReturn,
                    PageSize = pageSize,
                    IsAscending = isAscending,
                    TotalCount = productsToReturn.Count,
                    CurrentPage = currentPage
                });
            }

        }

        public async Task<IResult> AddRangeAsync(IList<ProductAddDto> products)
        {
            var product = _mapper.Map<IList<Product>>(products);
            await _unitOfWork.Products.AddRangeAsync(product);
            await _unitOfWork.CommitAsync();
            return new Result(ResultStatus.Succes, "products has been added successfully");
        }

        public async Task<IDataResult<ProductDto>> GetProductWithComments(int productId)
        {
            var product = await _unitOfWork.Products.GetAsync(x => x.Id == productId, x => x.ProductBrand, x => x.ProductType);
            var comments = await _unitOfWork.Comment.GetAllAsync(x => x.IsPublished == true&&x.ProductId==productId);
            if(product==null)
                return new DataResult<ProductDto>(ResultStatus.Error, "No products found with specified criteria", null);
            var productToReturnDto = _mapper.Map<ProductDto>(product);
            productToReturnDto.Comments = _mapper.Map<IList<CommentReturnDto>>(comments);
            return new DataResult<ProductDto>(ResultStatus.Succes, productToReturnDto);
        }

        public async Task<IResult> UpdateRatingAsync(int productId, int rating)
        {
            var product = await _unitOfWork.Products.GetAsync(x => x.Id == productId);
            product.RatingCount += 1;
            product.Rating = (product.Rating + rating) / product.RatingCount;
            await _unitOfWork.Products.UpdateAsync(product);
            await _unitOfWork.CommitAsync();
            return new Result(ResultStatus.Succes, "");
        }
    }
}

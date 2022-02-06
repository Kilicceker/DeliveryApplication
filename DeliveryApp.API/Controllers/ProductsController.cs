using DeliveryApp.Core.Dtos;
using DeliveryApp.Core.Services.Abstract;
using DeliveryApp.Shared.Result.ComplexTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeliveryApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _iproductService;

        public ProductsController(IProductService iproductService)
        {
            _iproductService = iproductService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Product(int id)
        {
            var product = await _iproductService.GetProductWithComments(id);
            return Ok(product);
        }
        [HttpGet]
        public async Task<IActionResult> Product(int? productTypeId, int? productBrandId, int currentPage, int pageSize, bool isAscending)
        {
            var products = await _iproductService.GetAllWithPagesAsync(productTypeId, productBrandId, currentPage, pageSize,isAscending);
            return Ok(products);
        }
        [HttpGet("All")]
        public async Task<IActionResult> GetAll()
        {
            var products = await _iproductService.GetAllAsync();
            return Ok(products);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Save(ProductAddDto productAddDto)
        {
            var product = await _iproductService.AddAsync(productAddDto);
            return Created(string.Empty, product);
        }
        [HttpPost("products")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Save(IList<ProductAddDto> productAddDtos)
        {
            var products = await _iproductService.AddRangeAsync(productAddDtos);
            return Created(string.Empty, products);
        }
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(ProductUpdateDto productUpdateDto)
        {
            var response = await _iproductService.UpdateAsync(productUpdateDto);
            if (response.ResultStatus == ResultStatus.Succes)
                return NoContent();
            return BadRequest(response);
        }
        [HttpDelete("{id}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Remove(int id)
        {
            var response = await _iproductService.DeleteAsync(id);
            if (response.ResultStatus == ResultStatus.Succes)
                return NoContent();
            return BadRequest(response);
        }
        [HttpGet("search")]
        public async Task<IActionResult> Search(string keyword, int currentPage, int pageSize = 5, bool isAscending = false)
        {
            var products = await _iproductService.SearchAsync(keyword,currentPage,pageSize,isAscending);
            return Ok(products);
        }
        [HttpPut("rating")]
        public async Task<IActionResult> UpdateRating(Rating rating)
        {
            var response = await _iproductService.UpdateRatingAsync(rating.ProductId,rating.RatingValue);
            if (response.ResultStatus == ResultStatus.Succes)
                return NoContent();
            return BadRequest(response);
        }
    }
}

using ETicaret.Application.Abstractions;
using ETicaret.Application.Repositories;
using ETicaret.Application.RequestParameters;
using ETicaret.Application.Services;
using ETicaret.Application.ViewModels.Products;
using ETicaret.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ETicaret.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IProductReadRepository _productReadRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IFileService _fileService;
       
        public ProductsController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository,
            IWebHostEnvironment webHostEnvironment,IFileService fileService)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
            _webHostEnvironment = webHostEnvironment;
            _fileService = fileService;
        }

        [HttpGet]
        public  async Task<IActionResult> GetProducts([FromQuery]Pagination pagination) 
        {
            var totalCount=_productReadRepository.GetAll(false).Count();
            var products= _productReadRepository.GetAll(false).Skip(pagination.Page * pagination.Size).Take(pagination.Size).Select(p => new
            {
                p.Id,
                p.Name,
                p.Stock,
                p.Price,
                p.CreatedDate,
                p.UpdatedDate

            });
            return Ok(new
            {
                totalCount, products
            });
        }

        [HttpPost]
        public async Task<IActionResult> Post(VMCreateProduct vmCreateProduct) 
        {
            await _productWriteRepository.AddAsync(new() 
            { 
                Name=vmCreateProduct.Name,
                Price=vmCreateProduct.Price,
                Stock=vmCreateProduct.Stock
            });
            await _productWriteRepository.SaveAsync();

            return Ok((int)HttpStatusCode.Created);
        
        }

        [HttpPut]
        public async Task<IActionResult> Put(VMUpdateProduct vmUpdateProduct)
        {

            Product product = await _productReadRepository.GetByIdAsync(vmUpdateProduct.Id);
            product.Stock=vmUpdateProduct.Stock;
            product.Name=vmUpdateProduct.Name;
            product.Price=vmUpdateProduct.Price;
            await _productWriteRepository.SaveAsync();

            return Ok((int)HttpStatusCode.Created);

        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _productWriteRepository.RemoveAsync(id);
            await _productWriteRepository.SaveAsync();
            return Ok();
        }

        [HttpPost("[action]")]
        public  async Task<IActionResult> Upload()
        {
            await _fileService.UploadAsync("product-images",Request.Form.Files);
            return Ok();
        }
    }
}

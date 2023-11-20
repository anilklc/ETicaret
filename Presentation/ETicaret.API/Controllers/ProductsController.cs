using ETicaret.Application.Abstractions;
using ETicaret.Application.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETicaret.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IProductReadRepository _productReadRepository;
       
        public ProductsController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
        }

        [HttpGet]
        public async Task GetProducts() 
        {
           /* await _productWriteRepository.AddRangeAsync(new() 
            {  new() {Name="Test",Price=100,CreatedDate=DateTime.Now,Stock=10},
               new() {Name="Test 2",Price=150,CreatedDate=DateTime.Now,Stock=15}
            });

            var count=await _productWriteRepository.SaveAsync();*/



        }
    }
}

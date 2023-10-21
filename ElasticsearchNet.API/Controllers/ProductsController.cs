using ElasticsearchNet.API.Dtos;
using ElasticsearchNet.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElasticsearchNet.API.Controllers
{
    public class ProductsController : BaseController
    {
        private readonly ProductService _productService;

        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddProductDto product)
        {
            return AddActionResult(await _productService.Add(product));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return AddActionResult(await _productService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            return AddActionResult(await _productService.GetById(id));
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateProductDto updateProduct)
        {
            return AddActionResult(await _productService.Update(updateProduct));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            return AddActionResult(await _productService.Delete(id));
        }
    }
}

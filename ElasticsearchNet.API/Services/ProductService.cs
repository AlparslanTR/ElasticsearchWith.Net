using ElasticsearchNet.API.Dtos;
using ElasticsearchNet.API.Models;
using ElasticsearchNet.API.Repositories.ProductRepo;
using System.Collections.Immutable;
using System.Net;

namespace ElasticsearchNet.API.Services
{
    public class ProductService
    {
        private readonly ProductRepo _repo;

        public ProductService(ProductRepo repo)
        {
           _repo = repo;
        }


        public async Task<ResponseDto<ProductDto>> Add(AddProductDto product)
        {
            var response = await _repo.Add(product.AddProduct());

            if (response is null)
            {
                return ResponseDto<ProductDto>.Fail(new List<string> {"Ekleme İşlemi Başarısız"},HttpStatusCode.InternalServerError);
            }

            return ResponseDto<ProductDto>.Success(response.CreateDto(), HttpStatusCode.Created);
        }

        public async Task<ResponseDto<List<ProductDto>>> GetAll()
        {
            var products = await _repo.GetAll();
            var productListDto = new List<ProductDto>();    

            foreach (var x in products)
            {
                if (x.ProductFeature is null)
                {
                    productListDto.Add(new ProductDto(x.Id, x.Name, x.Price, x.Stock, null));
                }

                else
                {
                    productListDto.Add(new ProductDto(x.Id, x.Name, x.Price, x.Stock, new ProductFeatureDto(x.ProductFeature.Width, x.ProductFeature.Height, x.ProductFeature.Color)));
                }
                
            }
            return ResponseDto<List<ProductDto>>.Success(productListDto, HttpStatusCode.OK);

        }

        public async Task<ResponseDto<ProductDto?>> GetById(string id)
        {
            var product = await _repo.GetById(id);

            if (product == null)
            {
                return ResponseDto<ProductDto?>.Fail(new List<string> { "Ürün Bulunamadı" }, HttpStatusCode.NotFound);
            }
            return ResponseDto<ProductDto?>.Success(product.CreateDto(),HttpStatusCode.OK);


        }

        public async Task<ResponseDto<bool>> Update(UpdateProductDto updateProduct)
        {
            var isProduct = await _repo.Update(updateProduct);

            if (!isProduct)
            {
                return ResponseDto<bool>.Fail(new List<string> { "Ürün Bulunamadı" }, HttpStatusCode.NotFound);
            }
            return ResponseDto<bool>.Success(true, HttpStatusCode.NoContent);
        }

        public async Task<ResponseDto<bool>> Delete(string id)
        {
            var isProduct = await _repo.Delete(id);

            if (!isProduct)
            {
                return ResponseDto<bool>.Fail(new List<string> {"Ürün Bulunamadı"}, HttpStatusCode.NotFound);
            }
            return ResponseDto<bool>.Success(true,HttpStatusCode.NoContent);
        }
    }
}

using ElasticsearchNet.API.Models;

namespace ElasticsearchNet.API.Dtos
{
    public record AddProductDto(string Name,decimal Price, int Stock,ProductFeatureDto ProductFeature)
    {
        public Product AddProduct()
        {
            return new Product { Name = Name, Price = Price, Stock = Stock, ProductFeature = new ProductFeature() { Width = ProductFeature.Width, Color = ProductFeature.Color, Height = ProductFeature.Height } };
        }
    }
}

using ElasticsearchNet.API.Dtos;
using Nest;

namespace ElasticsearchNet.API.Models
{
    public class Product
    {
        [PropertyName("_id")]
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public ProductFeature? ProductFeature { get; set; }

        public ProductDto CreateDto() 
        {
            if (ProductFeature is null) return new ProductDto(Id, Name, Price, Stock, null);

            return new ProductDto(Id, Name, Price, Stock, new ProductFeatureDto(ProductFeature.Width, ProductFeature.Height, ProductFeature.Color));
        }
    }
}

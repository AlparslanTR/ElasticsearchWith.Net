using ElasticsearchNet.API.Models;
using Nest;

namespace ElasticsearchNet.API.Dtos
{
    public record ProductDto(string Id, string Name, decimal Price, int Stock, ProductFeatureDto? ProductFeature)
    {
    }
}

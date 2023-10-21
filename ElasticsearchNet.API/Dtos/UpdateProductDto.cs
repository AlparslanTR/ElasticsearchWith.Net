namespace ElasticsearchNet.API.Dtos
{
    public record UpdateProductDto(string Id, string Name, decimal Price, int Stock, ProductFeatureDto ProductFeature)
    {
    }
}

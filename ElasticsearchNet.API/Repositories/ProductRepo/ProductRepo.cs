using Elastic.Clients.Elasticsearch;
using ElasticsearchNet.API.Dtos;
using ElasticsearchNet.API.Models;
using Nest;

namespace ElasticsearchNet.API.Repositories.ProductRepo
{
    public class ProductRepo 
    {
        //private readonly ElasticClient _client;
        private readonly ElasticsearchClient _client;
        private const string indexName = "products";

        public ProductRepo(ElasticsearchClient client)
        {
            _client = client;
        }

        public async Task<Product?> Add(Product product)
        {
            product.CreatedDate = DateTime.Now;

            var response = await _client.IndexAsync(product,x=>x.Index(indexName).Id(Guid.NewGuid().ToString()));

            if(!response.IsSuccess()) return null;
            product.Id = response.Id;
            return product;
        }

        public async Task<IReadOnlyCollection<Product>> GetAll()
        {
            var result = await _client.SearchAsync<Product>(s => s.Index(indexName).Query(q => q.MatchAll()));

            foreach (var hit in result.Hits) hit.Source.Id = hit.Id;

            return result.Documents;
        }

        public async Task<Product?> GetById(string id)
        {
            var response= await _client.GetAsync<Product>(id, x=>x.Index(indexName));

            if (!response.IsSuccess())
            {
                return null;
            }
            response.Source.Id = response.Id;
            return response.Source;
        }

        public async Task<bool> Update(UpdateProductDto updateProduct)
        {
            var response = await _client.UpdateAsync<Product,UpdateProductDto>(indexName,updateProduct.Id,x=>x.Doc(updateProduct));
            return response.IsSuccess();
        }

        public async Task<bool> Delete(string id)
        {
            var response = await _client.DeleteAsync<Product>(id, x => x.Index(indexName));
            return response.IsSuccess();
        }
    }
}

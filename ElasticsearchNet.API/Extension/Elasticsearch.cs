using Elasticsearch.Net;
using Nest;

namespace ElasticsearchNet.API.Extension
{
    public static class Elasticsearch
    {
        public static void AddElastic(this IServiceCollection services, IConfiguration configuration)
        {
            var pool = new SingleNodeConnectionPool(new Uri(configuration.GetSection("Elastic")["Url"]!));
            var settings = new ConnectionSettings(pool);
            settings.BasicAuthentication(configuration.GetSection("Elastic")["Username"],configuration.GetSection("Elastic")["Password"]);
            var client = new ElasticClient(settings);
            services.AddSingleton(client);
        }
    }
}

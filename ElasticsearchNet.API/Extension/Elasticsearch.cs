using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using Elasticsearch.Net;
using Nest;

namespace ElasticsearchNet.API.Extension
{
    public static class Elasticsearch
    {
        public static void AddElastic(this IServiceCollection services, IConfiguration configuration)
        {
            /* Nest Kütüphanesi İçin */

            //var pool = new SingleNodeConnectionPool(new Uri(configuration.GetSection("Elastic")["Url"]!));
            //var settings = new ConnectionSettings(pool);
            //settings.BasicAuthentication(configuration.GetSection("Elastic")["Username"],configuration.GetSection("Elastic")["Password"]);
            //var client = new ElasticClient(settings);

            /* Elastic Clients Kütüphanesi İçin */

            var UserName = configuration.GetSection("Elastic")["Username"];
            var Password = configuration.GetSection("Elastic")["Password"];

            var settings = new ElasticsearchClientSettings(new Uri(configuration.GetSection("Elastic")["Url"]!)).Authentication(new BasicAuthentication(UserName!,Password!));
            var client = new ElasticsearchClient(settings);

            services.AddSingleton(client);
        }
    }
}

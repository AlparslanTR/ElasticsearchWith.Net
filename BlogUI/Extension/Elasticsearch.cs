using Elastic.Clients.Elasticsearch;
using Elastic.Transport;

namespace BlogUI.Extension
{
    public static class Elasticsearch
    {
        public static void AddElastic(this IServiceCollection services, IConfiguration configuration)
        {
            var UserName = configuration.GetSection("Elastic")["Username"];
            var Password = configuration.GetSection("Elastic")["Password"];

            var settings = new ElasticsearchClientSettings(new Uri(configuration.GetSection("Elastic")["Url"]!)).Authentication(new BasicAuthentication(UserName!,Password!));
            var client = new ElasticsearchClient(settings);

            services.AddSingleton(client);
        }
    }
}

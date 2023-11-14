using BlogUI.Models;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using System.Reflection.Metadata.Ecma335;

namespace BlogUI.Repository.BlogRepo
{
    public class BlogRepo
    {
        private readonly ElasticsearchClient _client;
        private const string indexName = "blog";
        private const int Size = 100; 

        public BlogRepo(ElasticsearchClient client)
        {
            _client = client;
        }

        public async Task<Blog?> Save(Blog blog)
        {
            blog.Created = DateTime.Now;
            var response = await _client.IndexAsync(blog,indexName);
            if (!response.IsValidResponse) return null;
            blog.Id = response.Id;
            return blog;
        }

        public async Task<List<Blog>> Search(string text)
        {
            List<Action<QueryDescriptor<Blog>>> ListQuery = new();
            // En altta uzun sorgular yazmak yerine delege ile birşey dönmeden sorguları bir değişkene atayabilir ve direk sorgu kısmında bu değişkenleri kullanabiliriz.
            Action<QueryDescriptor<Blog>> MatchAll = (q)=>q.MatchAll(); // Bütün dataları çek.
            // İçerikleri yazdığım kelimelere göre getir.
            Action<QueryDescriptor<Blog>> MatchContent = (q) => q.Match(m => m
                                                                    .Field(f => f.Content)
                                                                    .Query(text));

            // Başlıkları yazdığım kelimelere göre getir.
            Action<QueryDescriptor<Blog>> MatchTitle = (q) => q.MatchBoolPrefix(m => m
                                                                    .Field(f => f.Title)
                                                                    .Query(text));

            if (string.IsNullOrEmpty(text))
            {
                ListQuery.Add(MatchAll);
            }

            else
            {
                ListQuery.Add(MatchContent);
                ListQuery.Add(MatchTitle);
            }


            var result = await _client.SearchAsync<Blog>(s => s
            .Index(indexName)
            .Size(Size)
            .Query(q => q
            .Bool(b => b
            .Should(
               ListQuery.ToArray()
                )))
            );
            foreach (var hit in result.Hits) hit.Source!.Id = hit.Id;
            return result.Documents.ToList();
        }
    }
}

using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using ElasticsearchNet.API.Models.ECommerce;
using System.Collections.Immutable;

namespace ElasticsearchNet.API.Repositories.ECommerceRepo
{
    public class ECommerceRepo
    {
        private readonly ElasticsearchClient _client;
        private const string indexName = "kibana_sample_data_ecommerce";

        public ECommerceRepo(ElasticsearchClient client)
        {
            _client = client;
        }

        // Keyword Sorguları
        public async Task<ImmutableList<ECommerce>> TermQuery(string customerFirstName)
        {
            // Tip güvenli olmadan string tipinde de bir field verebiliriz
            //var result = await _client.SearchAsync<ECommerce>(i => i.Index(indexName).Query(q => q.Term(t => t.Field("customer_first_name.keyword").Value(customerFirstName))));

            // Farklı bir yol
            //var termQuery = new TermQuery("customer_first_name.keyword") {Value=customerFirstName, CaseInsensitive=true };
            //var result = await _client.SearchAsync<ECommerce>(i => i.Index(indexName).Query(termQuery));

            // Tip Güvenli 
            var result = await _client.SearchAsync<ECommerce>(i => i
            .Index(indexName)
            .Query(q => q
            .Term(t => t.CustomerFirstName
            .Suffix("keyword"), customerFirstName)));

            foreach (var hit in result.Hits) hit.Source.Id = hit.Id;
            return result.Documents.ToImmutableList();
        }
        public async Task<ImmutableList<ECommerce>> TermsQuery(List<string> customerFirstNameList)
        {
            List<FieldValue> terms = new List<FieldValue>();
            customerFirstNameList.ForEach(x =>
            {
                terms.Add(x);
            });

            //1. Yol
            //var termsQuery = new TermsQuery()
            //{
            //    Field = "customer_first_name.keyword",
            //    Terms = new TermsQueryField(terms.AsReadOnly())
            //};
            //var result = await _client.SearchAsync<ECommerce>(q => q.Index(indexName).Query(termsQuery));


            // 2. Yol

            var result = await _client.SearchAsync<ECommerce>(s => s.Index(indexName)
            .Size(100)
            .Query(q => q
            .Terms(t => t
            .Field(f => f.CustomerFirstName
            .Suffix("keyword"))
            .Terms(new TermsQueryField(terms.AsReadOnly())))));

            return result.Documents.ToImmutableList();
        }
        public async Task<ImmutableList<ECommerce>> PrefixQuery(string customerFullName)
        {
            var result = await _client.SearchAsync<ECommerce>(s => s
            .Index(indexName)
            .Size(100)
            .Query(q => q
            .Prefix(p => p
            .Field(f => f.CustomerFullName
            .Suffix("keyword"))
            .Value(customerFullName))));

            return result.Documents.ToImmutableList();
        }
        public async Task<ImmutableList<ECommerce>> RangeQuery(double FromPrice, double ToPrice)
        {
            var result = await _client.SearchAsync<ECommerce>(s => s
            .Index(indexName)
            .Size(100)
            .Query(q => q
            .Range(r => r
            .NumberRange(nr => nr
            .Field(f => f.TaxFulTotalPrice)
            .Gte(FromPrice)
            .Lte(ToPrice)))));

            return result.Documents.ToImmutableList();
        }
        public async Task<ImmutableList<ECommerce>> MatchAll()
        {
            var result = await _client.SearchAsync<ECommerce>(s => s
            .Index(indexName)
            .Size(100)
            .Query(q => q
            .MatchAll()));

            return result.Documents.ToImmutableList();
        }
        public async Task<ImmutableList<ECommerce>> MatchAllWithPagination(int page, int pageSize)
        {
            var pageFrom = (page - 1) * pageSize;
            var result = await _client.SearchAsync<ECommerce>(s => s
            .Index(indexName)
            .Size(pageSize)
            .From(pageFrom)
            .Query(q => q
            .MatchAll()));

            return result.Documents.ToImmutableList();
        }
        public async Task<ImmutableList<ECommerce>> Wildcard(string customerFirstName)
        {
            var result = await _client.SearchAsync<ECommerce>(s => s
            .Index(indexName)
            .Query(q => q
            .Wildcard(w => w
            .Field(f => f.CustomerFirstName
            .Suffix("keyword"))
            .Wildcard(customerFirstName))));

            return result.Documents.ToImmutableList();
        }
        public async Task<ImmutableList<ECommerce>> FuzzyQuery(string customerFullName)
        {
            var result = await _client.SearchAsync<ECommerce>(s => s
            .Index(indexName)
            .Size(100)
            .Query(q => q
            .Fuzzy(f => f
            .Field(fi => fi.CustomerFullName
            .Suffix("keyword"))
            .Value(customerFullName)
            .Fuzziness(new Fuzziness(2)))));

            return result.Documents.ToImmutableList();
        }

        // Text Sorguları
        public async Task<ImmutableList<ECommerce>> MatchQueryWithFullText(string categoryName)
        {
            var result = await _client.SearchAsync<ECommerce>(s => s
            .Index(indexName)
            .Size(100)
            .Query(q => q
            .Match(m => m
            .Field(f => f.Category)
            .Query(categoryName)
            .Operator(Operator.And))));

            return result.Documents.ToImmutableList();
        }
        public async Task<ImmutableList<ECommerce>> MatchBoolPrefixWithFullText(string customerFullName)
        {
            var result = await _client.SearchAsync<ECommerce>(s => s
            .Index(indexName)
            .Size(100)
            .Query(q => q
            .MatchBoolPrefix(m => m
            .Field(f => f.CustomerFullName)
            .Query(customerFullName))));

            return result.Documents.ToImmutableList();
        }
        public async Task<ImmutableList<ECommerce>> MatchPhraseWithFullText(string customerFullName)
        {
            var result = await _client.SearchAsync<ECommerce>(s => s
            .Index(indexName)
            .Size(100)
            .Query(q => q
            .MatchPhrase(m => m
            .Field(f => f.CustomerFullName)
            .Query(customerFullName))));

            return result.Documents.ToImmutableList();
        }
        public async Task<ImmutableList<ECommerce>> CompoundQueryFullText(string cityName, double taxfulTotalPrice, string categoryName, string menufacturer)
        {
            var result = await _client.SearchAsync<ECommerce>(s => s
                    .Index(indexName)
                    .Size(1000)
                    .Query(q => q
                    .Bool(b => b
                    .Must(m => m
                    .Term(t => t
                    .Field("geoip.city_name").Value(cityName)))
                    .MustNot(mn => mn
                    .Range(r => r
                    .NumberRange(nr => nr
                    .Field(f => f.TaxFulTotalPrice)
                    .Lte(taxfulTotalPrice))))
                    .Should(s => s
                    .Term(t => t
                    .Field(f => f.Category!.Suffix("keyword")).Value(categoryName)))
                    .Filter(f => f
                    .Term(t => t
                    .Field("manufacturer.keyword")
                    .Value(menufacturer))))
                ));

            return result.Documents.ToImmutableList();
        }
        public async Task<ImmutableList<ECommerce>> MultiMatchQuery(string customer)
        {
            var result = await _client.SearchAsync<ECommerce>(s => s
           .Index(indexName)
           .Size(100)
           .Query(q => q
           .MultiMatch(m => m
           .Fields(new Field("customer_first_name")
           .And("customer_last_name")
           .And("customer_full_name"))
           .Query(customer))));

            return result.Documents.ToImmutableList();
        }
    }
}

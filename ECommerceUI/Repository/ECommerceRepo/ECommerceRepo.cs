using ECommerceUI.Dtos;
using ECommerceUI.Models.entities;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using System.Reflection;

namespace ECommerceUI.Repository.ECommerceRepo
{
    public class ECommerceRepo
    {
        private readonly ElasticsearchClient _client;
        private const string indexName = "kibana_sample_data_ecommerce";
        private const int Size = 10;

        public ECommerceRepo(ElasticsearchClient client)
        {
            _client = client;
        }

        public async Task<(List<ECommerce>list,long count)> Search(ECommerceSearch search, int page, int pageSize)
        {
            List<Action<QueryDescriptor<ECommerce>>> ListQuery = new();



            Action<QueryDescriptor<ECommerce>> matchAll = q => q.MatchAll();
            Action<QueryDescriptor<ECommerce>> searchCategory = q => q.Match(m => m
                                                                      .Field(f => f.Category)
                                                                      .Query(search.Category));
            Action<QueryDescriptor<ECommerce>> searchCustomer = q => q.Match(m => m
                                                                      .Field(f => f.CustomerFullName)
                                                                      .Query(search.CustomerName));
            Action<QueryDescriptor<ECommerce>> searchOrderDateStart = q => q.Range(r => r
                                                                      .DateRange(dr => dr
                                                                      .Field(f => f.OrderDate).Gte(search.OrderDateStart.Value)));
            Action<QueryDescriptor<ECommerce>> searchOrderDateEnd = q => q.Range(r => r
                                                                      .DateRange(dr => dr
                                                                      .Field(f => f.OrderDate).Lte(search.OrderDateEnd.Value)));
            Action<QueryDescriptor<ECommerce>> searchGender = q => q.Term(m => m
                                                                     .Field(f => f.Gender).Value(search.Gender).CaseInsensitive());
            if (search is null)
            {
                ListQuery.Add(matchAll);
                return await PageCount(page, pageSize, ListQuery);
            }


            if (!string.IsNullOrEmpty(search.Category))
            {
                ListQuery.Add(searchCategory);
            }

            if (!string.IsNullOrEmpty(search.CustomerName))
            {
                ListQuery.Add(searchCustomer);
            }

            if (search.OrderDateStart.HasValue)
            {
                ListQuery.Add(searchOrderDateStart);
            }

            if (search.OrderDateEnd.HasValue)
            {
                ListQuery.Add(searchOrderDateEnd);
            }

            if (!string.IsNullOrEmpty(search.Gender))
            {
                ListQuery.Add(searchGender);
            }

            if (!ListQuery.Any())
            {
                ListQuery.Add(matchAll);
            }

            else
            {
                ListQuery.Add(matchAll);
            }

            return await PageCount(page, pageSize, ListQuery);
        }

        private async Task<(List<ECommerce> list, long count)> PageCount(int page, int pageSize, List<Action<QueryDescriptor<ECommerce>>> ListQuery)
        {
            var pageFrom = (page - 1) * pageSize;
            var result = await _client.SearchAsync<ECommerce>(s => s.Index(indexName)
            .Size(Size)
            .From(pageFrom)
            .Query(q => q
            .Bool(b => b
            .Must(ListQuery.ToArray())))
            );

            foreach (var hit in result.Hits) hit.Source!.Id = hit.Id;

            return (list: result.Documents.ToList(), result.Total);
        }
    }
}

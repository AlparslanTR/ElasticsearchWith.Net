using ECommerceUI.Dtos;
using ECommerceUI.Repository.ECommerceRepo;

namespace ECommerceUI.Services.ECommerceService
{
    public class ECommerceService
    {
        private readonly ECommerceRepo _repo;

        public ECommerceService(ECommerceRepo repo)
        {
            _repo = repo;
        }

        public async Task<(List<ECommerceList>,long totalCount, long pageLinkCount)> Search(ECommerceSearch search, int page, int pageSize)
        {
            var (list, totalCount) = await _repo.Search(search, page, pageSize);
            var pageLinkCountCalculator = totalCount%pageSize;
            long pageLinkCount = 0;

            if (pageLinkCountCalculator == 0)
            {
                pageLinkCount = totalCount / pageSize;
            }
            else
            {
                pageLinkCount = (totalCount / pageSize)+1;
            }

            var eCommerceList = list.Select(x => new ECommerceList()
            {
                Category = string.Join(",", x.Category),
                CustomerFullName = x.CustomerFullName,
                CustomerFirstName = x.CustomerFirstName,
                CustomerLastName = x.CustomerLastName,
                OrderDate = x.OrderDate.ToShortDateString(),
                Gender = x.Gender.ToLower(),
                Id = x.Id,
                OrderId = x.OrderId,
                CityName = x.CityName,
                TaxFulTotalPrice = x.TaxFulTotalPrice
            }).ToList();

            return (eCommerceList, totalCount, pageLinkCount);
        }
    }
}

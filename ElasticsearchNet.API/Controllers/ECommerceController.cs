using ElasticsearchNet.API.Repositories.ECommerceRepo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElasticsearchNet.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ECommerceController : ControllerBase
    {
        private readonly ECommerceRepo _repo;

        public ECommerceController(ECommerceRepo repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task <IActionResult> GetCustomerFirstName(string customerFirstName)
        {
            return Ok(await _repo.TermQuery(customerFirstName));
        }

        [HttpPost]
        public async Task<IActionResult> GetCustomersFirstName(List<string> customersFirstName)
        {
            return Ok(await _repo.TermsQuery(customersFirstName));
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomerFullName(string customersFullName)
        {
            return Ok(await _repo.PrefixQuery(customersFullName));
        }

        [HttpGet]
        public async Task<IActionResult> GetTaxTotalPrice(double fromPrice, double ToPrice)
        {
            return Ok(await _repo.RangeQuery(fromPrice,ToPrice));
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomersFirstNameWithAllData()
        {
            return Ok(await _repo.MatchAll());
        }
        
        [HttpGet]
        public async Task<IActionResult> GetCustomersFirstNameWithAllDataWithPagination(int page, int pageSize)
        {
            return Ok(await _repo.MatchAllWithPagination(page,pageSize));
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomerFirstNameWithWildcard(string customerFirstName)
        {
            return Ok(await _repo.Wildcard(customerFirstName));
        }
        [HttpGet]
        public async Task<IActionResult> GetCustomerFullNameWithFuzzy(string customerFullName)
        {
            return Ok(await _repo.FuzzyQuery(customerFullName));
        }
        [HttpGet]
        public async Task<IActionResult> GetCategoryNameWithMacth(string categoryName)
        {
            return Ok(await _repo.MatchQueryWithFullText(categoryName));
        }
        [HttpGet]
        public async Task<IActionResult> GetCustomerFullNameWithMacth(string customerFullName)
        {
            return Ok(await _repo.MatchBoolPrefixWithFullText(customerFullName));
        }
        [HttpGet]
        public async Task<IActionResult> GetCustomerFullNameWithMacthPhrase(string customerFullName)
        {
            return Ok(await _repo.MatchPhraseWithFullText(customerFullName));
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomerFullNameWithCompoundQuery(string cityName, double taxfulTotalPrice, string categoryName, string menufacturer)
        {
            return Ok(await _repo.CompoundQueryFullText(cityName,taxfulTotalPrice,categoryName,menufacturer));
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomerWithMultiMatchQuery(string customer)
        {
            return Ok(await _repo.MultiMatchQuery(customer));
        }

    }
}
